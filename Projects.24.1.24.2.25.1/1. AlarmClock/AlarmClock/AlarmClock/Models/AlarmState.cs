using System;

namespace AlarmClock.Models
{
    public class AlarmState
    {
        public DateTime AlarmTime { get; set; }

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }

        public bool IsAwakeActivated { get; set; }

        public DayOfWeek[] RepeatDays { get; set; } = new DayOfWeek[0];
    }
}