using System;

namespace AlarmClock.Model
{
    internal class TimerState
    {
        public TimeSpan Duration { get; set; }
        public TimeSpan Remaining { get; set; }
        public bool IsActive { get; set; }
        public bool IsSoundActive { get; set; }
    }
}
