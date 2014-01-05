using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vMixControler
{
    public enum vmMicroEventType {prepare,setup,fastforward,transition,remove,tick,exit};

    public struct vMixMicroEvent
    {
        public DateTime when;
        public vmMicroEventType what;
        public vMixEvent with;
        public vMixMicroEvent(DateTime time, vmMicroEventType type, vMixEvent vmevent)
        {
            when = time;
            what = type;
            with = vmevent;
        }
        public vMixMicroEvent(vmMicroEventType type)
        {
            when = new DateTime();
            what = type;
            with = null;
        }
    }
}
