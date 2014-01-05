using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Web;

namespace vMixControler
{
    public class vMixWebClient
    {
        WebClient vMix;

        public string URL { get { return vMix.BaseAddress; } set { vMix.BaseAddress = value; } }
        public List<vMixInput> vMixInputs;

        public vMixWebClient(string baseadress)
        {
            vMix = new WebClient();
            vMix.BaseAddress = baseadress;
            vMixInputs = new List<vMixInput>();
        }

        public bool GetStatus()
        {
            vMixInputs.Clear();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(vMix.DownloadString("api"));

                if (!CheckVersion())
                    return false;

                foreach (XmlNode node in doc.SelectNodes("vmix/inputs/input"))
                {
                    vMixInput vmi = new vMixInput();
                    vmi.guid = node.Attributes.GetNamedItem("key").Value;
                    vmi.number = int.Parse(node.Attributes.GetNamedItem("number").Value);
                    vmi.type = node.Attributes.GetNamedItem("type").Value;
                    vmi.state = node.Attributes.GetNamedItem("state").Value;
                    vmi.position = int.Parse(node.Attributes.GetNamedItem("position").Value);
                    vmi.duration = int.Parse(node.Attributes.GetNamedItem("duration").Value);
                    vmi.muted = bool.Parse(node.Attributes.GetNamedItem("muted").Value);
                    vmi.loop = bool.Parse(node.Attributes.GetNamedItem("loop").Value);
                    vmi.name = node.InnerText;
                    vMixInputs.Add(vmi);
                }
                return true;
            }
            catch { }
            return false;
        }

        public bool CheckVersion()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(vMix.DownloadString("api"));
                string[] v = doc.SelectNodes("vmix/version")[0].InnerText.Split('.');
                if (int.Parse(v[0]) >= 11)
                    return true;
            }
            catch { }
            return false;
        }

        public bool GetGUID(string inputname, out string guid)
        {
            guid = "invalid";

            if (!GetStatus())
                return false;

            foreach (vMixInput vmi in vMixInputs)
                if (vmi.name == inputname)
                {
                    guid = vmi.guid;
                    break;
                }
            return true;
        }

        public bool AddInput(string type, string path, string guid)
        {
            try
            {
                vMix.DownloadString("api?function=AddInput&Input=" + guid + "&Value=" + type + "|" + HttpUtility.UrlEncode(path));
                return true;
            }
            catch { }
            return false;
        }

        public bool SetupSlideshow(int intervall, string transitioneffect, int transitiontime, string guid)
        {
            try
            {
                vMix.DownloadString("api?function=SetPictureTransition&Input=" + guid + "&Value=" + intervall.ToString());
                vMix.DownloadString("api?function=SetPictureEffect&Input=" + guid + "&Value=" + transitioneffect);
                vMix.DownloadString("api?function=SetPictureEffectDuration&Input=" + guid + "&Value=" + transitiontime.ToString());
                return true;
            }
            catch { }
            return false;
        }

        public bool ForwardTo(string guid, int position)
        {
            try
            {
                vMix.DownloadString("api?function=SetPosition&Value=" + position.ToString() + "&Input=" + guid);
                //vMix.DownloadString("api?function=PlayPause&Input=" + guid);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool Transition(string guid, string type, int duration)
        {
            try
            {
                vMix.DownloadString("api?function=" + type + "&Duration=" + duration.ToString() + "&Input=" + guid);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool NextPicture(string guid)
        {
            try
            {
                vMix.DownloadString("api?function=NextPicture&Input=" + guid);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool CloseInput(string guid)
        {
            try
            {
                vMix.DownloadString("api?function=RemoveInput&Input=" + guid);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public struct vMixInput
    {
        public int number;
        public string guid;
        public string type;
        public string state;
        public int position;
        public int duration;
        public bool muted;
        public bool loop;
        public string name;
    }
}
