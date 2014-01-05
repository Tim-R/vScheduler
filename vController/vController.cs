using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml;

namespace vMixControler
{
    public partial class vMixControler : Form
    {
        vMixPreferences settings;
        List<vMixEvent> EventList;
        Semaphore EventListLock;

        vMixScheduler MasterClock;
        BlockingCollection<vMixMicroEvent> Workload;
        
        Thread WorkloadThread;

        vMixWebClient WebClient;

        bool exitApp = false;

        FileSystemWatcher WatchDog;
        string ScheduleFolder;

        public vMixControler()
        {
            InitializeComponent();
        }

        private void vMaster_Load(object sender, EventArgs e)
        {
            settings = new vMixPreferences();

            EventListLock = new Semaphore(1, 1);
            EventList = new List<vMixEvent>();
            Workload = new BlockingCollection<vMixMicroEvent>();

            MasterClock = new vMixScheduler(100, settings.vMixPreload , settings.vMixLinger, Workload);
            WebClient = new vMixWebClient(settings.vMixURL);

            ThreadStart workstart = new ThreadStart(WorkloadFunc);
            WorkloadThread = new Thread(workstart);
            WorkloadThread.Start();

            ScheduleFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\vMixScheduler";
            if (!Directory.Exists (ScheduleFolder)) Directory.CreateDirectory (ScheduleFolder);
            WatchDog = new FileSystemWatcher(ScheduleFolder, "vMixSchedule.xml");
            WatchDog.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName;
            WatchDog.Changed += new FileSystemEventHandler(WatchDogBark);
            WatchDog.Created += new FileSystemEventHandler(WatchDogBark);
            WatchDog.Deleted += new FileSystemEventHandler(WatchDogBark);
            WatchDog.EnableRaisingEvents = true;

            if(settings.vMixAutoLoad) 
                ReloadSchedule();
        }

        private void WatchDogBark(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Deleted)
                ClearList();
            else
                ReloadSchedule();
        }

        private void ClearList()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(ClearList));
            else
            {
                lvEventList.SelectedIndices.Clear();
                EventListLock.WaitOne();
                for (int n = EventList.Count - 1; n >= 0; n--)
                {
                    MasterClock.RemoveMicroEvents(EventList[n]);
                    if (EventList[n].EventType != vmEventType.input)
                        WebClient.CloseInput(EventList[n].GUID);
                    EventList.RemoveAt(n);
                }
                EventListLock.Release();
                lvEventList.VirtualListSize = EventList.Count;
            }
        }

        private void ReloadSchedule()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(ReloadSchedule));
            else
            {
                this.Enabled = false;

                string schedulename = ScheduleFolder + "\\vMixSchedule.xml";
                if (File.Exists(schedulename))
                {
                    List<vMixEvent> vmes = new List<vMixEvent>();
                    XmlDocument d = new XmlDocument();
                    try
                    {
                        d.Load(schedulename);
                        lvEventList.SelectedIndices.Clear();
                        vmes.Sort(delegate(vMixEvent e1, vMixEvent e2) { return e1.EventStart.CompareTo(e2.EventStart); });
                        for (int n = EventList.Count - 1; n >= 0; n--)
                        {
                            if (!EventList[n].IsLoaded)
                            {
                                MasterClock.RemoveMicroEvents(EventList[n]);
                                EventList.RemoveAt(n);
                            }
                        }
                        
                        foreach (XmlNode n in d.SelectNodes("//vMixManager//Events//Event"))
                        {
                            vMixEvent ne = new vMixEvent (n);
                            if (ne.EventEnd > DateTime.Now )
                            {
                                bool found = false;
                                foreach (vMixEvent ve in EventList)
                                    if (ve.IsLike (ne))
                                        found = true;
                                if (!found)
                                    vmes.Add(new vMixEvent (n));
                            }
                        }

                        if (vmes.Count > 0)
                        {
                            DateTime revoke = vmes[0].EventStart > DateTime.Now ? vmes[0].EventStart : DateTime.Now;
                            foreach (vMixEvent ve in EventList)
                                MasterClock.RevokeMicroEvents (ve,revoke);
                            foreach (vMixEvent vme in vmes)
                                if (MasterClock.AddMicroEvents(vme))
                                    EventList.Add(vme);
                            EventList.Sort(delegate(vMixEvent e1, vMixEvent e2) { return e1.EventStart.CompareTo(e2.EventStart); });
                            lvEventList.VirtualListSize = EventList.Count;
                            lvEventList.RedrawItems(0, EventList.Count - 1, false);
                        }
                    }
                    catch {}
                }
                this.Enabled = true;
            }
        }

        private void bn_load_schedule_Click(object sender, EventArgs e)
        {
            ReloadSchedule();
        }

        private void lvEventList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < EventList.Count)
                e.Item = EventListItem(EventList[e.ItemIndex]);
        }

        public ListViewItem EventListItem(vMixEvent vmixevent)
        {
            string[] caption = { 
                                   vmixevent.Title,
                                   vmixevent.EventStart.ToString("MM-dd  HH:mm:ss"),
                                   vmixevent.EventDuration.ToString(@"hh\:mm\:ss"),
                                   vmixevent.EventTypeString()                               
                               };
            ListViewItem lvi = new ListViewItem(caption);
            return lvi;
        }

        private void WorkloadFunc()
        {
            vMixMicroEvent vme;

            while(!exitApp)
            {
                PaintTime();
                if (Workload.TryTake(out vme, 1000))
                {
                    vMixEvent evnt = vme.with;

                    if (vme.what.Equals(vmMicroEventType.exit))
                        break;

                    switch (vme.what)
                    {
                        case vmMicroEventType.prepare:
                            evnt.state = 1;
                            if (evnt.EventType == vmEventType.input)
                            {
                                    WebClient.GetGUID(evnt.Title, out evnt.GUID);
                            }
                            else if (evnt.EventType == vmEventType.black)
                            {
                                    WebClient.AddInput("Colour", "black", evnt.GUID);
                            }
                            else if (evnt.HasMedia)
                            {
                                    WebClient.AddInput(evnt.EventTypeString(), evnt.EventPath, evnt.GUID);
                            }
                            break;
                        case vmMicroEventType.setup:
                            if (evnt.EventType == vmEventType.photos)
                            {
                                    WebClient.SetupSlideshow (evnt.SlideshowInterval,evnt.SlideshowTypeString(),evnt.SlideshowTransitionTime, evnt.GUID);
                            }
                            break;
                        case  vmMicroEventType.fastforward :
                            if (evnt.HasDuration)
                            {
                                int position = (int)(DateTime.Now - evnt.EventStart + evnt.EventInPoint).TotalMilliseconds;
                                    WebClient.ForwardTo(evnt.GUID, position);
                            }
                            break;
                        case vmMicroEventType.transition:
                            evnt.state = 2;
                            if (evnt.EventType == vmEventType.input)
                            {
                                string type = evnt.TransitionTypeString();
                                int duration = evnt.EventTransitionTime;
                                    WebClient.Transition(evnt.GUID, type, duration);
                            }
                            else if (evnt.HasMedia)
                            {
                                string type = evnt.TransitionTypeString();
                                int duration = evnt.EventTransitionTime;
                                    WebClient.Transition(evnt.GUID, type, duration);
                            }
                            break;
                        case vmMicroEventType.remove:
                            if (evnt.EventType != vmEventType.input)
                                WebClient.CloseInput(evnt.GUID);
                            RemoveEvent(evnt);
                            break;                       
                    }
                }
            }
            CloseWindow();
        }

        private void RemoveEvent(vMixEvent evnt)
        {
            
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(delegate { RemoveEvent(evnt); }));
                else
                {
                    EventListLock.WaitOne();
                    EventList.Remove(evnt);
                    EventListLock.Release();
                    lvEventList.VirtualListSize = EventList.Count;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);                
            }
        }

        private void PaintTime()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(PaintTime));
            else
                lbl_clock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void CloseWindow()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(CloseWindow));
            else
                this.Close();
        }

        private void vMixControler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exitApp)
            {
                exitApp = true;
                Workload.Add(new vMixMicroEvent(vmMicroEventType.exit));
                MasterClock.Abort();
                ClearList();
                e.Cancel = true;
            }
        }

        private void bn_showpreferences_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
            WebClient.URL = settings.vMixURL;
            MasterClock.Intervall = 100;
            MasterClock.MediaLinger = settings.vMixLinger;
            MasterClock.MediaPreload = settings.vMixPreload;
        }
    }
}
