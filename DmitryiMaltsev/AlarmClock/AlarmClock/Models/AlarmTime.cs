using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    public class AlarmTime
    {
        public DateTime Times { get; set; }

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }
    }
}
