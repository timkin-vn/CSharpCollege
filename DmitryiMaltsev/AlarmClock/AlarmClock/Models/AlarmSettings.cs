using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    public class AlarmSettings
    {
        public List<AlarmTime> TimeSettings { get; } = new List<AlarmTime>();

        public bool IsSoundActive { get; set; }
    }
}
