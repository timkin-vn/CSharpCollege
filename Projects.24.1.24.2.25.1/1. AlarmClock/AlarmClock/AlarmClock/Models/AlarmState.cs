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
        public bool IsSnoozed { get; set; } = false;

        public TimeSpan SnoozeDuration { get; set; } = TimeSpan.FromMinutes(5);
    }
}