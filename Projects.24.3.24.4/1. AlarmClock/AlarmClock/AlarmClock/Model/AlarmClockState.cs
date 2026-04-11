using System;

namespace AlarmClock.Model
{
    internal class AlarmClockState
    {
        public DateTime AlarmTime { get; set; }

        public string AlarmMessage { get; set; }

        public bool IsAlarmActive { get; set; }

        public bool IsSoundActive { get; set; }

        public bool IsAwakeActivated { get; set; }

        public int SnoozeMinutes { get; set; } = 5;

        public bool IsSnoozeRequested { get; set; }
    }
}
