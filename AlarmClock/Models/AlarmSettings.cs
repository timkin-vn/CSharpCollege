using System;

namespace AlarmClock.Models
{
    public class AlarmSettings
    {
        public DateTime AlarmTime { get; set; }
        public string AlarmMessage { get; set; }
        public bool IsAlarmActive { get; set; }
        public bool IsSoundActive { get; set; }
        public string SelectedSound { get; set; }
        public bool IsTimerActive { get; set; }
        public TimeSpan TimerDuration { get; set; }
    }
}
