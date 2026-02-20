using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Будильник.Models
{
    public class AlarmState
    {
        public DateTime AlarmTime { get; set; }

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }

        public bool IsAwakeActivated { get; set; }

        public bool IsSnoozed { get; set; }
    }
}
