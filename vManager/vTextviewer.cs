using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vMixManager
{
    public partial class vMixTextviewer : Form
    {
        private readonly string nl = Environment.NewLine;
        public vMixTextviewer()
        {
            InitializeComponent();
        }

        public void ViewText(List<vMixEvent> evnt_list)
        {
            rtb_viewer.Text = "Schedule:" + nl;
            foreach (vMixEvent e in evnt_list)
                rtb_viewer.AppendText(nl +e.EventStart.ToString("yy-MM-dd  HH:mm:ss") + "  " + e.Title);
            this.ShowDialog();
        }

        public void ViewHTML(List<vMixEvent> evnt_list)
        {
            rtb_viewer.Text = "Schedule:" + nl;
            rtb_viewer.AppendText(nl + "<table>"+nl+"<tr><th>Date</th><th>Time</th><th>Title</th></tr>");
            foreach (vMixEvent e in evnt_list)
                rtb_viewer.AppendText(nl + "<tr><td>" + e.EventStart.ToString("yy-MM-dd") + "</td><td>" + e.EventStart.ToString("HH:mm:ss") + "</td><td>" + e.Title+"</td></tr>");
            rtb_viewer.AppendText(nl + "</table>");
            this.ShowDialog();
        }

        public void ViewBBCode(List<vMixEvent> evnt_list)
        {
            rtb_viewer.Text = "Schedule:" + nl;
            rtb_viewer.AppendText(nl + "[list]");
            foreach (vMixEvent e in evnt_list)
                rtb_viewer.AppendText(nl + "[*]" + e.EventStart.ToString("yy-MM-dd  HH:mm:ss") + "  " + e.Title);
            rtb_viewer.AppendText(nl + "[/list]");
            this.ShowDialog();
        }
    }
}
