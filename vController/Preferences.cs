using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using AMS.Profile;

namespace vMixControler
{
    public partial class vMixPreferences : Form
    {
        private int _vMixPort = 8088;
        public string vMixURL { get { return "http://127.0.0.1:" + _vMixPort.ToString(); } }

        private int _vMixPreload = 5;
        public int vMixPreload { get { return _vMixPreload; } }

        private int _vMixLinger = 5;
        public int vMixLinger { get { return _vMixLinger; } }

        private bool _vMixAutoLoad = false;
        public bool vMixAutoLoad { get { return _vMixAutoLoad; } }

        private Xml settings;

        public vMixPreferences()
        {
            InitializeComponent();
            settings = new Xml (Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\vMixScheduler\\Settings.xml");
            ud_vMixPort.Value = _vMixPort = settings.GetValue("vMixScheduler", "vMixPort", 8088);
            ud_preload.Value = _vMixPreload = settings.GetValue("vMixScheduler", "MediaPreload", 5);
            ud_linger.Value = _vMixLinger = settings.GetValue("vMixScheduler", "MediaLinger", 5);
            cb_autoload.Checked = _vMixAutoLoad = settings.GetValue("vMixScheduler", "AutoLoad", false);
        }

        private void SaveSettings()
        {
            settings.SetValue("vMixScheduler", "vMixPort", _vMixPort);
            settings.SetValue("vMixScheduler", "MediaPreload", _vMixPreload);
            settings.SetValue("vMixScheduler", "MediaLinger", _vMixLinger);
            settings.SetValue("vMixScheduler", "AutoLoad", _vMixAutoLoad);
        }

        private void bn_testport_Click(object sender, EventArgs e)
        {
            WebClient vMix = new WebClient ();
            vMix.BaseAddress = vMixURL;
            XmlDocument doc = new XmlDocument();

            bool result = false;
            try
            {
                doc.LoadXml(vMix.DownloadString("api"));
                if (doc.SelectNodes("vmix/inputs/input").Count > 1)
                    result = true;
            }
            catch { }
            if (result)
                MessageBox.Show("vMix seems fine, or at least there\r\nis a responsive webserver at this URL:\r\n\r\n"+vMixURL,"vMix OK");
            else
                MessageBox.Show("vMix didn't respond at this URL:\r\n\r\n"+vMixURL,"vMix not found");
        }

        private void ud_vMixPort_ValueChanged(object sender, EventArgs e)
        {
            _vMixPort = (int)ud_vMixPort.Value;
        }

        private void ud_preload_ValueChanged(object sender, EventArgs e)
        {
            _vMixPreload = (int)ud_preload.Value;
        }

        private void ud_linger_ValueChanged(object sender, EventArgs e)
        {
            _vMixLinger = (int)ud_linger.Value;
        }

        private void vMixPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void cb_autoload_CheckedChanged(object sender, EventArgs e)
        {
            _vMixAutoLoad = cb_autoload.Checked;
        }
    }
}
