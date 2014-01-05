using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace vMixControler
{
    public class vMixScheduler
    {
        Semaphore LocalListLock;
        List<vMixMicroEvent> LocalList;
        BlockingCollection<vMixMicroEvent> Workload;

        Thread Worker;
        bool StopWorking;

        public int Intervall;
        public int MediaPreload;
        public int MediaLinger;

        public bool Working { get { return Worker.IsAlive; } }

        public vMixScheduler(int intervall, int mediapreload, int medialinger, BlockingCollection <vMixMicroEvent> workload)
        {
            LocalListLock = new Semaphore(1, 1);
            LocalList = new List<vMixMicroEvent>();
            Workload = workload;

            Intervall = intervall;
            MediaPreload = mediapreload;
            MediaLinger = medialinger;

            ThreadStart workstart = new ThreadStart(ClockWorker);
            Worker = new Thread(workstart);
            Worker.Start();
        }

        public void Abort()
        {
            StopWorking = true;
            Worker.Join();
        }

        void ClearEventList()
        {
            LocalListLock.WaitOne();
            LocalList.Clear();
            LocalListLock.Release();
        }

        public bool AddMicroEvents(vMixEvent evnt)
        {
            DateTime now = DateTime.Now;
            if (evnt.EventEnd < now + new TimeSpan(0, 0, MediaPreload+1))
                return false;     // too late to add!
            
            List<vMixMicroEvent> vmes = new List<vMixMicroEvent>();

            // prepare, transition, remove
            if (!evnt.HasDuration)
            {
                vmes.Add(new vMixMicroEvent(evnt.EventStart - new TimeSpan(0, 0, MediaPreload), vmMicroEventType.prepare, evnt));
                if (evnt.EventType ==  vmEventType.photos )
                    vmes.Add(new vMixMicroEvent(evnt.EventStart - new TimeSpan(0, 0, 2), vmMicroEventType.setup, evnt));
                vmes.Add(new vMixMicroEvent(evnt.EventStart, vmMicroEventType.transition, evnt));
            }
            else if (evnt.EventStart >= now + new TimeSpan(0,0,MediaPreload))
            {
                vmes.Add(new vMixMicroEvent(evnt.EventStart - new TimeSpan(0, 0, MediaPreload), vmMicroEventType.prepare, evnt));
                if (evnt.EventInPoint >= new TimeSpan(0) )
                    vmes.Add(new vMixMicroEvent(evnt.EventStart - new TimeSpan(0, 0, 2), vmMicroEventType.fastforward, evnt));
                vmes.Add(new vMixMicroEvent(evnt.EventStart, vmMicroEventType.transition, evnt));
            }
            else // has already started!!
            {
                evnt.EventTransition = vmTransitionType.fade;
                evnt.EventTransitionTime = 300;
                vmes.Add(new vMixMicroEvent(now, vmMicroEventType.prepare, evnt));
                vmes.Add(new vMixMicroEvent(now + new TimeSpan(0, 0, 0, 1, 0), vmMicroEventType.fastforward, evnt));
                vmes.Add(new vMixMicroEvent(now + new TimeSpan(0, 0, 0, 2, 0), vmMicroEventType.transition, evnt));
            }
            
            vmes.Add(new vMixMicroEvent(evnt.EventEnd + new TimeSpan (0, 0, MediaLinger), vmMicroEventType.remove, evnt));

            LocalListLock.WaitOne();
            LocalList.AddRange(vmes);
            LocalList.Sort(delegate(vMixMicroEvent e1, vMixMicroEvent e2) { return e1.when.CompareTo(e2.when); });
            LocalListLock.Release();
            return true;
        }

        public void RemoveMicroEvents(vMixEvent evnt)
        {
            LocalListLock.WaitOne();
            for (int n = LocalList.Count - 1; n >= 0; n--)
                if (LocalList[n].with == evnt)
                    LocalList.RemoveAt(n);
            LocalListLock.Release();
        }
        public void RevokeMicroEvents(vMixEvent evnt, DateTime when)
        {
            LocalListLock.WaitOne();
            for (int n = LocalList.Count - 1; n >= 0; n--)
                if (LocalList[n].with == evnt)
                    LocalList.RemoveAt(n);

            LocalList.Add (new vMixMicroEvent(when + new TimeSpan(0,0,MediaLinger), vmMicroEventType.remove, evnt));
            LocalList.Sort(delegate(vMixMicroEvent e1, vMixMicroEvent e2) { return e1.when.CompareTo(e2.when); });
            LocalListLock.Release();
        }

        private void ClockWorker()
        {
            StopWorking = false;
            while (!StopWorking)
            {
                LocalListLock.WaitOne();
                DateTime now = DateTime.Now;
                if (LocalList.Count > 0)
                {
                    List<vMixMicroEvent> toremove = new List<vMixMicroEvent>();
                    for (int n = 0; n < LocalList.Count; n++)
                        if (LocalList[n].when < now)
                        {
                            Workload.Add(LocalList[n]);
                            toremove.Add(LocalList[n]);
                        }
                    foreach (vMixMicroEvent vme in toremove)
                        LocalList.Remove(vme);
                }
                LocalListLock.Release();
                Thread.Sleep(Intervall);
            }
        }
    }
}