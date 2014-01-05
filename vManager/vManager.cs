using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using MediaInfoLib;

namespace vMixManager
{
    public partial class vMixManager : Form
    {
        List<vMixEvent> vMixEvents;
        vMixEvent ActiveEvent;
        MediaInfo FileInfo;
        bool donotredraw = false;

        public vMixManager()
        {
            InitializeComponent();
            vMixEvents = new List<vMixEvent>();
            FileInfo = new MediaInfo();
        }

        private void vMixManager_Load(object sender, EventArgs e)
        {
            dtp_timetable.Value = DateTime.Today + new TimeSpan(1, 0, 0, 0, 0);
            lb_transition.Text = "Fade";
            lb_slideshow_transition.Text = "Fade";
        }

        private void EventList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < vMixEvents.Count)
                e.Item = EventListItem(vMixEvents[e.ItemIndex]);
        }
        
        public ListViewItem EventListItem(vMixEvent vmixevent)
        {
            string[] caption = { vmixevent.Title,
                                   vmixevent.EventStart.ToString("MM-dd  HH:mm:ss"),
                                   vmixevent.EventDuration.ToString(@"hh\:mm\:ss"),
                                   vmixevent.EventTypeString(),
                                   vmixevent.EventPath };
            ListViewItem lvi = new ListViewItem(caption);
            lvi.ToolTipText = vmixevent.EventInfoText;
            return lvi;
        }

        private void UpdateDisplay()
        {
            if (donotredraw)
                return;
            donotredraw = true;
            if (ActiveEvent != null)
            {
                EventDetails.Enabled = true; 
                tb_title.Text = ActiveEvent.Title;
                dtp_start.Value = ActiveEvent.EventStart;
                dtp_duration.Text = ActiveEvent.EventDuration.ToString("c");
                dtp_inpoint.Text = ActiveEvent.EventInPoint.ToString("c");
                dtp_end.Value = ActiveEvent.EventEnd;
                if (ActiveEvent.EventLooping)
                    rb_looping.Checked = true;
                else
                    rb_toblack.Checked = true;
                if (ActiveEvent.HasDuration)
                {
                    cb_keep_duration.Checked = ActiveEvent.KeepDuration;
                    cb_keep_duration.Enabled = true;
                }
                else
                {
                    cb_keep_duration.Checked = false;
                    cb_keep_duration.Enabled = false;
                }
                dtp_duration.Enabled = !ActiveEvent.KeepDuration;
                dtp_inpoint.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_ip_zero.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_ip_50.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_ip_33.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_ip_25.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_dr_100.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_dr_50.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_dr_33.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                bn_dr_25.Enabled = ActiveEvent.HasDuration && !ActiveEvent.KeepDuration;
                lb_transition.Text = ActiveEvent.TransitionTypeString();
                ud_transition_time.Enabled = (ActiveEvent.EventTransition != vmTransitionType.cut);
                ud_transition_time.Value = ActiveEvent.EventTransitionTime;
                rtb_fileinfo.Text = ActiveEvent.EventInfoText;

                if (ActiveEvent.EventType == vmEventType.photos)
                {
                    pnl_slideshow.Visible = true;
                    ud_slideshow_interval.Value = ActiveEvent.SlideshowInterval;
                    lb_slideshow_transition.Text = ActiveEvent.SlideshowTypeString();
                    ud_slideshow_transition.Value = ActiveEvent.SlideshowTransitionTime;
                }
                else
                    pnl_slideshow.Visible = false;
            }
            else 
            {
                tb_title.Text = "";
                dtp_start.Text = "00:00:00";
                dtp_inpoint.Text = "00:00:00";
                dtp_duration.Text = "00:00:00";
                dtp_end.Text = "00:00:00";
                rb_toblack.Checked = false;
                rb_looping.Checked = false;
                cb_keep_duration.Checked = false;
                lb_transition.SelectedIndex = 0;
                ud_transition_time.Value = 1000;
                rtb_fileinfo.Text = "";
                pnl_slideshow.Visible = false;
                EventDetails.Enabled = false;
            }
            donotredraw = false;
        }

        private void EventList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveEvent = null;
            if (EventList.SelectedIndices.Count > 0)
            {
                ActiveEvent = vMixEvents[EventList.SelectedIndices[0]];
                bn_move_up.Enabled = (EventList.SelectedIndices[0] > 0);
                bn_move_down.Enabled = (EventList.SelectedIndices[0] < vMixEvents.Count - 1);
                bn_remove.Enabled = true;
            }
            else
            {
                bn_move_up.Enabled = false;
                bn_move_down.Enabled = false;
                bn_remove.Enabled = true;
            }
            UpdateDisplay();
        }

        private void tb_title_TextChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null)
            {
                ActiveEvent.Title = tb_title.Text;
                EventList.RedrawItems(EventList.SelectedIndices[0], EventList.SelectedIndices[0], false);
            }
        }

        private void lb_transition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null)
            {
                ActiveEvent.EventTransition = ActiveEvent.TransitionTypeFromString(lb_transition.Text);
                if (ActiveEvent.EventTransition == vmTransitionType.cut)
                {
                    ActiveEvent.EventTransitionTime = 0;
                    RebuildTimetable();
                }
                UpdateDisplay();
            }
        }

        private void cb_looping_CheckedChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null)
                ActiveEvent.EventLooping = rb_looping.Checked;
        }

        private void ud_transition_time_ValueChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null)
            {
                ActiveEvent.EventTransitionTime = (int)ud_transition_time.Value;
                RebuildTimetable();
                UpdateDisplay();
            }
        }

        private void cb_keep_duration_CheckedChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent == null) return;
            if (cb_keep_duration.Checked)
            {
                ActiveEvent.EventInPoint = new TimeSpan(0);
                ActiveEvent.EventDuration = ActiveEvent.MediaDuration;
                ActiveEvent.KeepDuration = true;
                dtp_duration.Enabled = false;
                dtp_inpoint.Enabled = false;
                //dtp_end.Enabled = false;
                rb_looping.Enabled = false;
                rb_toblack.Enabled = false;
                RebuildTimetable();
                UpdateDisplay();
            }
            else
            {
                ActiveEvent.KeepDuration = false;
                UpdateDisplay();
            }
        }

        private void RebuildTimetable()
        {
            if (vMixEvents.Count == 0) return;
            DateTime nextstart = dtp_timetable.Value;
            for (int i = 0; i < vMixEvents.Count; i++)
            {
                vMixEvent e = vMixEvents [i];
                if (i > 0)
                    nextstart -= new TimeSpan(0, 0, 0, 0, e.EventTransitionTime);
                if (e.EventStart != nextstart)
                    e.EventStart = nextstart;
                nextstart += e.EventDuration;
            }
            EventList.RedrawItems(0, vMixEvents .Count -1, false);
        }

        private void dtp_timetable_ValueChanged(object sender, EventArgs e)
        {
            RebuildTimetable();
            UpdateDisplay();
        }

        private void dtp_duration_ValueChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null)
            {
                TimeSpan dr = dtp_duration.Value.TimeOfDay;
                if (ActiveEvent.HasDuration && dr + ActiveEvent.EventInPoint > ActiveEvent.MediaDuration)
                    dr = ActiveEvent.MediaDuration - ActiveEvent.EventInPoint;
                if (dr > new TimeSpan(0, 0, 0))
                    ActiveEvent.EventDuration = dr;
                else
                    ActiveEvent.EventDuration = new TimeSpan(0);
                RebuildTimetable();
                UpdateDisplay();
            }
        }
        private void dtp_inpoint_ValueChanged(object sender, EventArgs e)
        {
            if (donotredraw) return;
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                TimeSpan ip = dtp_inpoint.Value.TimeOfDay;
                if (ip > ActiveEvent.MediaDuration )
                {
                    ip = ActiveEvent .MediaDuration ;
                    dtp_inpoint.Text = ip.ToString("c");
                }
                if (ip > new TimeSpan(0))
                    ActiveEvent.EventInPoint = ip;
                else
                    ActiveEvent.EventInPoint = new TimeSpan(0);
                if (ip + ActiveEvent.EventDuration > ActiveEvent.MediaDuration)
                    ActiveEvent.EventDuration = ActiveEvent.MediaDuration - ip;

                RebuildTimetable();
                UpdateDisplay();
            }
        }

        private void ud_slideshow_interval_ValueChanged(object sender, EventArgs e)
        {
            if (ActiveEvent != null)
                ActiveEvent.SlideshowInterval = (int)ud_slideshow_interval.Value;
        }
        private void lb_slideshow_transition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveEvent != null)
                ActiveEvent.SlideshowTransition = ActiveEvent.TransitionTypeFromString(lb_slideshow_transition.Text);
        }
        private void ud_slideshow_transition_ValueChanged(object sender, EventArgs e)
        {
            if (ActiveEvent != null)
                ActiveEvent.SlideshowTransitionTime = (int)ud_slideshow_transition.Value;
        }
        private void bn_move_up_Click(object sender, EventArgs e)
        {
            if (ActiveEvent == null) return;
            int position = vMixEvents.IndexOf(ActiveEvent);
            if (position > 0)
            {
                donotredraw = true;
                vMixEvents.RemoveAt(position);
                vMixEvents.Insert(position - 1, ActiveEvent);
                EventList.SelectedIndices.Clear();
                EventList.SelectedIndices.Add(position - 1);
                RebuildTimetable();
                donotredraw = false;
                UpdateDisplay();
            }
        }

        private void bn_move_down_Click(object sender, EventArgs e)
        {
            if (ActiveEvent == null) return;
            int position = vMixEvents.IndexOf(ActiveEvent);
            if (position < vMixEvents.Count - 1)
            {
                donotredraw = true;
                vMixEvents.RemoveAt(position);
                vMixEvents.Insert(position + 1, ActiveEvent);
                EventList.SelectedIndices.Clear();
                EventList.SelectedIndices.Add(position + 1);
                RebuildTimetable();
                donotredraw = false;
                UpdateDisplay();
            }
        }
        private void bn_clone_Click(object sender, EventArgs e)
        {
            if (ActiveEvent == null) return;
            int position = vMixEvents.IndexOf(ActiveEvent) + 1;
            donotredraw = true;
            vMixEvent copy = new vMixEvent(ActiveEvent);
            vMixEvents.Insert(position, copy);
            ActiveEvent = copy;
            EventList.VirtualListSize = vMixEvents.Count;
            EventList.SelectedIndices.Clear();
            EventList.SelectedIndices.Add(position);
            RebuildTimetable();
            donotredraw = false;
            UpdateDisplay();
        }
        private void bn_remove_Click(object sender, EventArgs e)
        {
            if (ActiveEvent == null) return;
            int position = vMixEvents.IndexOf(ActiveEvent);
            donotredraw = true;
            vMixEvents.Remove(ActiveEvent);
            ActiveEvent = null;
            EventList.VirtualListSize = vMixEvents.Count;
            EventList.SelectedIndices.Clear();
            if (position >= vMixEvents.Count) position--;
            if (position >= 0)
                EventList.SelectedIndices.Add(position);
            RebuildTimetable();
            donotredraw = false;
            UpdateDisplay();
        }

        private void bn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML-File|*.xml|all Files|*.*";
            sfd.FileName = dtp_timetable.Value.ToString("yyyy-MM-dd");
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XmlDocument d = new XmlDocument();
                XmlNode root = d.CreateElement("vMixManager");
                d.AppendChild(root);
                XmlNode events = d.CreateElement("Events");
                root.AppendChild(events);
                foreach (vMixEvent vme in vMixEvents)
                    events.AppendChild(vme.ToXMLNode (d));
                d.Save(sfd.FileName);
                MessageBox.Show(vMixEvents.Count.ToString() + " events saved to xml.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bn_clear_Click(object sender, EventArgs e)
        {
            EventList.SelectedIndices.Clear();
            donotredraw = true;
            ActiveEvent = null;
            vMixEvents.Clear();
            EventList.VirtualListSize = 0;
            UpdateDisplay();
            donotredraw = false;
        }

        private void bn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML-Files|*.xml|all Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<vMixEvent > vmes = new List<vMixEvent> ();
                XmlDocument d = new XmlDocument();
                d.Load(ofd.FileName);
                foreach(XmlNode n in d.SelectNodes ("//vMixManager//Events//Event"))
                    vmes.Add(new vMixEvent(n));
                if (vmes.Count > 0)
                {
                    vmes.Sort(delegate(vMixEvent e1, vMixEvent e2) {return e1.EventStart.CompareTo(e2.EventStart);});
                    donotredraw = true;
                    EventList.SelectedIndices.Clear();
                    ActiveEvent = null;
                    vMixEvents = vmes;
                    EventList.VirtualListSize = vmes.Count;
                    dtp_timetable.Value = vmes[0].EventStart;
                    RebuildTimetable();
                    UpdateDisplay();
                    donotredraw = false;
                    MessageBox.Show(vMixEvents.Count.ToString() + " events loaded from xml.", "Success!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        private void bn_append_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML-Files|*.xml|all Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<vMixEvent> vmes = new List<vMixEvent>();
                XmlDocument d = new XmlDocument();
                d.Load(ofd.FileName);
                foreach (XmlNode n in d.SelectNodes("//vMixManager//Events//Event"))
                    vmes.Add(new vMixEvent(n));
                if (vmes.Count > 0)
                {
                    vmes.Sort(delegate(vMixEvent e1, vMixEvent e2) { return e1.EventStart.CompareTo(e2.EventStart); });
                    donotredraw = true;
                    vMixEvents.AddRange(vmes);
                    EventList.VirtualListSize = vMixEvents .Count;
                    RebuildTimetable();
                    UpdateDisplay();
                    donotredraw = false;
                    MessageBox.Show(vmes.Count.ToString() + " events appended from xml.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void bn_add_input_Click(object sender, EventArgs e)
        {
            vMixEvent new_event = new vMixEvent(vmEventType.input, dtp_timetable.Value, new TimeSpan(0, 1, 0));
            if (new_event != null)
            {
                new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                new_event.EventTransitionTime = (int)ud_transition_time.Value;
                int position;
                if (ActiveEvent != null)
                    position = vMixEvents.IndexOf(ActiveEvent) + 1;
                else
                    position = vMixEvents.Count;
                vMixEvents.Insert(position, new_event);
                ActiveEvent = new_event;
                EventList.VirtualListSize = vMixEvents.Count;
                EventList.SelectedIndices.Clear();
                RebuildTimetable();
                EventList.SelectedIndices.Add(position);
                UpdateDisplay();
            }

        }

        private void bn_add_black_Click(object sender, EventArgs e)
        {
            vMixEvent new_event = new vMixEvent (vmEventType.black, dtp_timetable.Value,new TimeSpan (0,0,10));
            if (new_event != null)
            {
                new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                new_event.EventTransitionTime = (int)ud_transition_time.Value;
                int position;
                if (ActiveEvent != null)
                    position = vMixEvents.IndexOf(ActiveEvent) + 1;
                else
                    position = vMixEvents.Count;
                vMixEvents.Insert(position, new_event);
                ActiveEvent = new_event;
                EventList.VirtualListSize = vMixEvents.Count;
                EventList.SelectedIndices.Clear();
                RebuildTimetable();
                EventList.SelectedIndices.Add(position);
                UpdateDisplay();
            }
        }

        private void bn_add_manual_Click(object sender, EventArgs e)
        {
            vMixEvent new_event = new vMixEvent(vmEventType.manual, dtp_timetable.Value, new TimeSpan(1, 0, 0));
            if (new_event != null)
            {
                new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                new_event.EventTransitionTime = (int)ud_transition_time.Value;
                int position;
                if (ActiveEvent != null)
                    position = vMixEvents.IndexOf(ActiveEvent) + 1;
                else
                    position = vMixEvents.Count;
                vMixEvents.Insert(position, new_event);
                ActiveEvent = new_event;
                EventList.VirtualListSize = vMixEvents.Count;
                EventList.SelectedIndices.Clear();
                RebuildTimetable();
                EventList.SelectedIndices.Add(position);
                UpdateDisplay();
            }
        }

        private void bn_add_video_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    vMixEvent new_event = ParseVideoData(file);
                    if (new_event != null)
                    {
                        new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                        new_event.EventTransitionTime = (int)ud_transition_time.Value;
                        int position;
                        if (ActiveEvent != null)
                            position = vMixEvents.IndexOf(ActiveEvent) + 1;
                        else
                            position = vMixEvents.Count;
                        vMixEvents.Insert(position, new_event);
                        ActiveEvent = new_event;
                        EventList.VirtualListSize = vMixEvents.Count;
                        EventList.SelectedIndices.Clear();
                        RebuildTimetable();
                        EventList.SelectedIndices.Add(position);
                        UpdateDisplay();
                    }
                }
            }
        }

        private vMixEvent ParseVideoData(string path)
        {
            vMixEvent new_event = null;
            string infotext = path;
            FileInfo.Open(path);

            string result = FileInfo.Get(StreamKind.General, 0, "Video_Format_List");
            if (result != "")
            {
                infotext += "\r\nVideo: " + result;
                result = FileInfo.Get(StreamKind.General, 0, "Audio_Format_List");
                if (result != "") infotext += "\r\nAudio: " + result;

                double milliseconds = -1;
                TimeSpan duration = new TimeSpan(0);
                result = FileInfo.Get(StreamKind.General, 0, "Duration");
                CultureInfo cult = CultureInfo.CreateSpecificCulture("en-GB");
                if (result != "" && double.TryParse(result, NumberStyles.Float | NumberStyles.AllowDecimalPoint, cult,out milliseconds))
                {
                    duration = new TimeSpan(0, 0, 0, 0, (int) milliseconds);
                    infotext += "\r\nDuration: " + duration.ToString(@"hh\:mm\:ss");
                }
                else
                    MessageBox.Show("I can't decode this files duration!", "No Duration?", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                new_event = new vMixEvent(System.IO.Path.GetFileNameWithoutExtension(path),
                    path,
                    vmEventType.video,
                    dtp_timetable.Value,
                    new TimeSpan (0),
                    duration,
                    duration,
                    true,
                    vmTransitionType.cut,
                    1000,
                    true);
                new_event.EventInfoText = infotext;
            }
            else
                MessageBox.Show("I can't recognize the video format!", "No Video?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FileInfo.Close();
            return new_event;
        }

        private void bn_add_image_Click(object sender, EventArgs e)
        {
           OpenFileDialog ofd = new OpenFileDialog();
           ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    vMixEvent new_event = ParseImageData(file);
                    if (new_event != null)
                    {
                        new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                        new_event.EventTransitionTime = (int)ud_transition_time.Value;
                        int position;
                        if (ActiveEvent != null)
                            position = vMixEvents.IndexOf(ActiveEvent) + 1;
                        else
                            position = vMixEvents.Count;
                        vMixEvents.Insert(position, new_event);
                        ActiveEvent = new_event;
                        EventList.VirtualListSize = vMixEvents.Count;
                        EventList.SelectedIndices.Clear();
                        RebuildTimetable();
                        EventList.SelectedIndices.Add(position);
                        UpdateDisplay();
                    }
                }
            }
        }

        private vMixEvent ParseImageData(string path)
        {
            vMixEvent new_event = null;
            string infotext = path;
            FileInfo.Open(path);

            string result = FileInfo.Get(StreamKind.General, 0, "Video_Format_List");
            if (result != "JPEG")
                result = FileInfo.Get(StreamKind.General, 0, "Image_Format_List");
            if (result != "")
            {
                infotext += "\r\nImage: " + result;
                TimeSpan duration = new TimeSpan(0,0,10);
                new_event = new vMixEvent(System.IO.Path.GetFileNameWithoutExtension(path),
                    path,
                    vmEventType.image,
                    dtp_timetable.Value,
                    new TimeSpan(0),
                    duration,
                    duration,
                    false,
                    vmTransitionType.cut,
                    1000,
                    true);
                new_event.EventInfoText = infotext;
            }
            else
                MessageBox.Show("I can't recognize the image format!", "No Image?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FileInfo.Close();
            return new_event;
        }

        private void bn_add_audio_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    vMixEvent new_event = ParseAudioData(file);
                    if (new_event != null)
                    {
                        new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                        new_event.EventTransitionTime = (int)ud_transition_time.Value;
                        int position;
                        if (ActiveEvent != null)
                            position = vMixEvents.IndexOf(ActiveEvent) + 1;
                        else
                            position = vMixEvents.Count;
                        vMixEvents.Insert(position, new_event);
                        ActiveEvent = new_event;
                        EventList.VirtualListSize = vMixEvents.Count;
                        EventList.SelectedIndices.Clear();
                        RebuildTimetable();
                        EventList.SelectedIndices.Add(position);
                        UpdateDisplay();
                    }
                }
            }
        }

        private vMixEvent ParseAudioData(string path)
        {
            vMixEvent new_event = null;
            string infotext = path;
            FileInfo.Open(path);

            string result = FileInfo.Get(StreamKind.General, 0, "Audio_Format_List");
            if (result != "")
            {
                infotext += "\r\nAudio: " + result;

                double milliseconds = -1;
                TimeSpan duration = new TimeSpan(0);
                result = FileInfo.Get(StreamKind.General, 0, "Duration");
                CultureInfo cult = CultureInfo.CreateSpecificCulture("en-GB");
                if (result != "" && double.TryParse(result, NumberStyles.Float | NumberStyles.AllowDecimalPoint, cult, out milliseconds))
                {
                    duration = new TimeSpan(0, 0, 0, 0, (int)milliseconds);
                    infotext += "\r\nDuration: " + duration.ToString(@"hh\:mm\:ss");
                }
                else
                    MessageBox.Show("I can't decode this files duration!", "No Duration?", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                new_event = new vMixEvent(System.IO.Path.GetFileNameWithoutExtension(path),
                    path,
                    vmEventType.audio,
                    dtp_timetable.Value,
                    new TimeSpan(0),
                    duration,
                    duration,
                    true,
                    vmTransitionType.cut,
                    1000,
                    true);
                new_event.EventInfoText = infotext;
            }
            else
                MessageBox.Show("I can't recognize the audio format!", "No Audio?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FileInfo.Close();
            return new_event;
        }

        private void bn_add_photos_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                TimeSpan ts = new TimeSpan(0, 3, 0);
                vMixEvent new_event = new vMixEvent(System.IO.Path.GetFileName (path),
                    path,
                    vmEventType.photos,
                    dtp_timetable.Value,
                    new TimeSpan(0),
                    ts,
                    ts,
                    false,
                    vmTransitionType.fade,
                    500,
                    false);

                if (new_event != null)
                {
                    new_event.EventTransition = new_event.TransitionTypeFromString(lb_transition.Text);
                    new_event.EventTransitionTime = (int)ud_transition_time.Value;
                    new_event.SlideshowInterval = (int)ud_slideshow_interval.Value;
                    new_event.SlideshowTransition = new_event.TransitionTypeFromString(lb_slideshow_transition.Text);
                    new_event.SlideshowTransitionTime = (int)ud_slideshow_transition.Value;
                    new_event.EventInfoText = "slideshow";

                    int position;
                    if (ActiveEvent != null)
                        position = vMixEvents.IndexOf(ActiveEvent) + 1;
                    else
                        position = vMixEvents.Count;
                    vMixEvents.Insert(position, new_event);
                    ActiveEvent = new_event;
                    EventList.VirtualListSize = vMixEvents.Count;
                    EventList.SelectedIndices.Clear();
                    RebuildTimetable();
                    EventList.SelectedIndices.Add(position);
                    UpdateDisplay();
                }
            }
        }

        private void bn_schedule_Click(object sender, EventArgs e)
        {
            if (vMixEvents.Count == 0)
                return;

            DateTime start = vMixEvents[0].EventStart;
            DateTime end = vMixEvents[vMixEvents.Count-1].EventEnd;

            string datafolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)+"\\vMixScheduler";
            if (!System.IO.Directory.Exists(datafolder))
                System.IO.Directory.CreateDirectory(datafolder);

            string schedulename = datafolder + "\\vMixSchedule.xml";

            List<vMixEvent> vmes = new List<vMixEvent>();
            XmlDocument d = new XmlDocument();

            if (System.IO.File.Exists(schedulename))
            {
                d.Load(schedulename);
                foreach (XmlNode n in d.SelectNodes("//vMixManager//Events//Event"))
                {
                    vMixEvent vme = new vMixEvent(n);
                    if (vme.EventStart > DateTime.Now && (vme.EventStart < start || vme.EventStart > end))
                        vmes.Add(vme);
                }
                d = new XmlDocument();
            }
            vmes.AddRange(vMixEvents);
            vmes.Sort(delegate(vMixEvent e1, vMixEvent e2) { return e1.EventStart.CompareTo(e2.EventStart); });
                        
            XmlNode root = d.CreateElement("vMixManager");
            d.AppendChild(root);
            XmlNode events = d.CreateElement("Events");
            root.AppendChild(events);
            foreach (vMixEvent vme in vmes)
                events.AppendChild(vme.ToXMLNode(d));
            d.Save(schedulename);
            MessageBox.Show("Events scheduled.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bn_erase_schedule_Click(object sender, EventArgs e)
        {
            string datafolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\vMixScheduler";
            string schedulename = datafolder + "\\vMixSchedule.xml";
            if (MessageBox.Show("This will erase ALL currently scheduled Events,\r\nincluding the ones scheduled earlier;\r\nrunning events will be terminated.\r\n\r\nAre you sure?", "Beware!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                if (System.IO.File.Exists(schedulename))
                    System.IO.File.Delete(schedulename);
                MessageBox.Show("Schedule erased.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bn_view_text_Click(object sender, EventArgs e)
        {
            vMixTextviewer vmt = new vMixTextviewer();
            vmt.ViewText(vMixEvents);
        }

        private void bn_view_html_Click(object sender, EventArgs e)
        {
            vMixTextviewer vmt = new vMixTextviewer();
            vmt.ViewHTML(vMixEvents);
        }

        private void bn_view_bbcode_Click(object sender, EventArgs e)
        {
            vMixTextviewer vmt = new vMixTextviewer();
            vmt.ViewBBCode(vMixEvents);
        }

        private void bn_now_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = DateTime.Now + new TimeSpan(0, 0, 10);
        }

        private void bn_settime_0_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date;
        }

        private void bn_settime_4_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date + new TimeSpan(4,0,0);
        }

        private void bn_settime_8_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date + new TimeSpan(8, 0, 0);
        }

        private void bn_settime_12_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date + new TimeSpan(12, 0, 0);
        }

        private void bn_settime_16_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date + new TimeSpan(16, 0, 0);
        }

        private void bn_settime_20_Click(object sender, EventArgs e)
        {
            dtp_timetable.Value = dtp_timetable.Value.Date + new TimeSpan(20, 0, 0);
        }

        private void FixInPointAndDuration()
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                if (ActiveEvent.EventInPoint > ActiveEvent.MediaDuration)
                    ActiveEvent.EventInPoint = ActiveEvent.MediaDuration;
                if (ActiveEvent.EventInPoint + ActiveEvent.EventDuration > ActiveEvent.MediaDuration)
                    ActiveEvent.EventDuration = ActiveEvent.MediaDuration - ActiveEvent.EventInPoint;
                RebuildTimetable();
                UpdateDisplay();
            }
        }
        private void bn_ip_zero_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent .HasDuration)
            {
                ActiveEvent.EventInPoint = new TimeSpan(0);
                FixInPointAndDuration();
            }
        }

        private void bn_ip_50_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventInPoint += new TimeSpan(ActiveEvent.MediaDuration.Ticks / 2);
                FixInPointAndDuration();
            }
        }

        private void bn_ip_33_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventInPoint += new TimeSpan(ActiveEvent.MediaDuration.Ticks / 3);
                FixInPointAndDuration();
            }

        }

        private void bn_ip_25_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventInPoint += new TimeSpan(ActiveEvent.MediaDuration.Ticks / 4);
                FixInPointAndDuration();
            }

        }

        private void bn_dr_100_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventDuration = ActiveEvent.MediaDuration;
                FixInPointAndDuration();
            }
        }

        private void bn_dr_50_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventDuration = new TimeSpan(ActiveEvent.MediaDuration.Ticks/2);
                FixInPointAndDuration();
            }
        }

        private void bn_dr_33_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventDuration = new TimeSpan(ActiveEvent.MediaDuration.Ticks / 3);
                FixInPointAndDuration();
            }
        }

        private void bn_dr_25_Click(object sender, EventArgs e)
        {
            if (ActiveEvent != null && ActiveEvent.HasDuration)
            {
                ActiveEvent.EventDuration = new TimeSpan(ActiveEvent.MediaDuration.Ticks / 4);
                FixInPointAndDuration();
            }
        }




    }
}
