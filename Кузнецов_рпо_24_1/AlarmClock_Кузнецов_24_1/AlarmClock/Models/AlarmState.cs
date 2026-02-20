using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    public class AlarmState
    {
        public DateTime AlarmTime { get; set; }

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }

        public bool IsAwakeActivated { get; set; }

        public bool IsSnoozeEnabled { get; set; }

        public void Snooze()
        {
            AlarmTime = DateTime.Now.AddMinutes(5);
            IsAwakeActivated = false;
        }
    }
}
