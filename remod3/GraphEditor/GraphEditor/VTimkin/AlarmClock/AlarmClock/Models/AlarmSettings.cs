using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    public class AlarmSettings
    {
        public DateTime AlarmTime { get; set; } = DateTime.Now;

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }

    }
}
