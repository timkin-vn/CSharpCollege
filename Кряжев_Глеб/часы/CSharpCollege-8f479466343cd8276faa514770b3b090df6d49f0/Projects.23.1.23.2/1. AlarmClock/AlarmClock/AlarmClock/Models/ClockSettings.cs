using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    internal class ClockSettings
    {
        public TimeSpan AlarmTime { get; set; } = DateTime.Now.TimeOfDay;

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }
    }
}
