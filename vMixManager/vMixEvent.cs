using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace vMixManager
{
    public enum vmEventType {manual, black, video, audio, image, photos, input};
    public enum vmTransitionType {cut, fade, zoom, wipe, slide, fly, cross, rotate, cube, cubezoom};
    public class vMixEvent
    {
        public string Title;
        public vmEventType EventType;
        public DateTime EventStart;
        public bool EventLooping;
        public vmTransitionType EventTransition;
        public int EventTransitionTime;
        public TimeSpan EventInPoint; 
        public TimeSpan MediaDuration;
        public TimeSpan EventDuration;
        public DateTime EventEnd {
            get
            {
                return EventStart + EventInPoint + EventDuration; 
            }
            set
            {
                EventDuration = value - (EventStart + EventInPoint);
            }
        }
        public bool KeepDuration;
        public bool HasDuration { get { return EventType == vmEventType.video || EventType == vmEventType.audio; } }
        public string EventPath;
        public string EventInfoText;
        public int SlideshowInterval;
        public vmTransitionType SlideshowTransition;
        public int SlideshowTransitionTime;

        public string EventTypeString()
        {
            switch (EventType)
            {
                case vmEventType.black:
                    return "black";
                case vmEventType.video:
                    return "video";
                case vmEventType.audio:
                    return "audio";
                case vmEventType.image:
                    return "image";
                case vmEventType.photos:
                    return "photos";
                case vmEventType.input:
                    return "input";
                default:
                    return "manual";
            }
        }
        public vmEventType EventTypeFromString(string type)
        {
            switch (type.ToLower ())
            {
                case "black":
                    return vmEventType.black;
                case "video":
                    return vmEventType.video;
                case "audio":
                    return vmEventType.audio;
                case "image":
                    return vmEventType.image;
                case "photos":
                    return vmEventType.photos;
                case "input":
                    return vmEventType.input;
                default:
                    return vmEventType.manual;
            }
        }
        public string TransitionTypeString()
        {
            return TransitionTypeString(EventTransition);
        }
        public string SlideshowTypeString()
        {
            return TransitionTypeString(SlideshowTransition);
        }
        public string TransitionTypeString(vmTransitionType trans)
        {
            switch (trans)
            {
                case vmTransitionType.fade :
                    return "Fade";
                case vmTransitionType.zoom:
                    return "Zoom";
                case vmTransitionType.wipe:
                    return "Wipe";
                case vmTransitionType.slide:
                    return "Slide";
                case vmTransitionType.fly:
                    return "Fly";
                case vmTransitionType.cross:
                    return "CrossZoom";
                case vmTransitionType.rotate:
                    return "FlyRotate";
                case vmTransitionType.cube:
                    return "Cube";
                case vmTransitionType.cubezoom:
                    return "CubeZoom";
                default:
                    return "Cut";
            }
        }
        public vmTransitionType TransitionTypeFromString(string type)
        {
            switch (type)
            {
                case "Fade":
                    return vmTransitionType.fade;
                case "Zoom":
                    return vmTransitionType.zoom;
                case "Wipe":
                    return vmTransitionType.wipe;
                case "Slide":
                    return vmTransitionType.slide;
                case "Fly":
                    return vmTransitionType.fly;
                case "CrossZoom":
                    return vmTransitionType.cross;
                case "FlyRotate":
                    return vmTransitionType.rotate;
                case "Cube":
                    return vmTransitionType.cube;
                case "CubeZoom":
                    return vmTransitionType.cubezoom;
                default:
                    return vmTransitionType.cut;
            }
        }
        public vMixEvent(vMixEvent e)
        {
            Title = e.Title;
            EventPath = e.EventPath;
            EventType = e.EventType;
            EventStart = e.EventEnd;
            EventInPoint = e.EventInPoint;
            MediaDuration = e.MediaDuration;
            EventDuration = e.EventDuration;
            KeepDuration = e.KeepDuration;
            EventTransition = e.EventTransition;
            EventTransitionTime = e.EventTransitionTime;
            EventLooping = e.EventLooping;
            SlideshowInterval = e.SlideshowInterval;
            SlideshowTransition = e.SlideshowTransition;
            SlideshowTransitionTime = e.SlideshowTransitionTime;
            EventInfoText = e.EventInfoText;
        }

        public vMixEvent(string title, string path, vmEventType type, DateTime start, TimeSpan inpoint, TimeSpan media_duration, TimeSpan event_duration, bool keep_duration, vmTransitionType transition, int transition_time, bool looping)
        {
            Title = title;
            EventPath = path; 
            EventType = type;
            EventStart = start;
            EventInPoint = inpoint;
            MediaDuration = media_duration;
            EventDuration = event_duration;
            KeepDuration = keep_duration;
            EventTransition = transition;
            EventTransitionTime = transition_time;
            EventLooping = looping;
            SlideshowInterval = 10; 
            SlideshowTransition = vmTransitionType.fade;
            SlideshowTransitionTime = 500;
        }
        public vMixEvent(vmEventType type, DateTime start, TimeSpan duration)
        {
            switch (type)
            {
                case vmEventType.black:
                    Title = "Blackness";
                    EventType = vmEventType.black;
                    EventInfoText = "vMix will switch to black";
                    break;
                case vmEventType.manual:
                    Title = "Operator Mode";
                    EventType = vmEventType.manual;
                    EventInfoText = "vMix will be free for manual operation";
                    break;
                case vmEventType.input:
                    Title = "enter input name";
                    EventType = vmEventType.input;
                    EventInfoText = "vMix will switch to the input of the same name";
                    break;
            }

            EventPath = "";
            EventStart = start;
            EventInPoint = new TimeSpan(0);
            MediaDuration = new TimeSpan (0);
            EventDuration = duration;
            KeepDuration = false;
            EventTransition = vmTransitionType.fade;
            EventTransitionTime = 1000;
            EventLooping = true;
            SlideshowInterval = 10; 
            SlideshowTransition = vmTransitionType.fade;
            SlideshowTransitionTime = 500;
        }

        public vMixEvent(XmlNode node)
        {
            Title = node.Attributes.GetNamedItem("Title").Value;
            EventPath = node.Attributes.GetNamedItem("Path").Value;
            EventType = EventTypeFromString (node.Attributes .GetNamedItem ("Type").Value);
            EventStart = DateTime.Parse(node.Attributes.GetNamedItem("Start").Value);
            EventDuration = TimeSpan.Parse(node.Attributes.GetNamedItem("EventDuration").Value);
            if (HasDuration)
            {
                EventInPoint = TimeSpan.Parse(node.Attributes.GetNamedItem("InPoint").Value);
                MediaDuration = TimeSpan.Parse(node.Attributes.GetNamedItem("MediaDuration").Value);
                KeepDuration = bool.Parse(node.Attributes.GetNamedItem("KeepDuration").Value);
                EventLooping = bool.Parse(node.Attributes.GetNamedItem("Looping").Value);
            }
            else
            {
                EventInPoint = new TimeSpan(0);
                MediaDuration = EventDuration;
                KeepDuration = false;
                EventLooping = false;
            }
            EventTransition = TransitionTypeFromString(node.Attributes.GetNamedItem("Transition").Value);
            if (EventTransition != vmTransitionType.cut)
                EventTransitionTime = int.Parse(node.Attributes.GetNamedItem("TransitionTime").Value);
            else
                EventTransitionTime = 0;

            EventInfoText = node.InnerText;

            if (EventType == vmEventType.photos)
            {
                SlideshowInterval = int.Parse(node.Attributes.GetNamedItem("SlideInterval").Value);
                SlideshowTransition = TransitionTypeFromString(node.Attributes.GetNamedItem("SlideTransition").Value);
                if (SlideshowTransition != vmTransitionType.cut)
                    SlideshowTransitionTime = int.Parse(node.Attributes.GetNamedItem("SlideTransitionTime").Value);
                else
                    SlideshowTransitionTime = 0;
            }
        }

        public XmlNode ToXMLNode(XmlDocument document)
        {
            XmlNode event_node = document.CreateElement("Event");
            event_node.InnerText = EventInfoText;

            XmlAttribute a = document.CreateAttribute("Title");
            a.InnerText = Title;
            event_node.Attributes.Append(a);

            a = document.CreateAttribute("Type");
            a.InnerText = EventTypeString();
            event_node.Attributes.Append(a);

            a = document.CreateAttribute("Start");
            a.InnerText = EventStart.ToString();
            event_node.Attributes.Append(a);

            a = document.CreateAttribute("Transition");
            a.InnerText = TransitionTypeString(EventTransition);
            event_node.Attributes.Append(a);

            if (EventTransition != vmTransitionType.cut)
            {
                a = document.CreateAttribute("TransitionTime");
                a.InnerText = EventTransitionTime.ToString();
                event_node.Attributes.Append(a);
            }

            a = document.CreateAttribute("EventDuration");
            a.InnerText = EventDuration.ToString();
            event_node.Attributes.Append(a);

            if (HasDuration)
            {
                a = document.CreateAttribute("InPoint");
                a.InnerText = EventInPoint.ToString();
                event_node.Attributes.Append(a);

                a = document.CreateAttribute("MediaDuration");
                a.InnerText = MediaDuration.ToString();
                event_node.Attributes.Append(a);

                a = document.CreateAttribute("KeepDuration");
                a.InnerText = KeepDuration.ToString();
                event_node.Attributes.Append(a);

                a = document.CreateAttribute("Looping");
                a.InnerText = EventLooping.ToString();
                event_node.Attributes.Append(a);
            }
            
            a = document.CreateAttribute("Path");
            a.InnerText = EventPath;
            event_node.Attributes.Append(a);

            if (EventType == vmEventType.photos)
            {
                a = document.CreateAttribute("SlideInterval");
                a.InnerText = SlideshowInterval.ToString();
                event_node.Attributes.Append(a);

                a = document.CreateAttribute("SlideTransition");
                a.InnerText = TransitionTypeString(SlideshowTransition);
                event_node.Attributes.Append(a);

                if (SlideshowTransition != vmTransitionType.cut)
                {
                    a = document.CreateAttribute("SlideTransitionTime");
                    a.InnerText = SlideshowTransitionTime.ToString();
                    event_node.Attributes.Append(a);
                }
            }

            return event_node;
        }
    }
}
