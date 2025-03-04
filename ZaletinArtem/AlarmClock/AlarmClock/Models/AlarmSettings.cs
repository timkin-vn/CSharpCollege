using System;

namespace AlarmClock.Models
{
    /// <summary>
    /// Настройки будильника
    /// </summary>
    public class AlarmSettings
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public DateTime AlarmTime { get; set; } = DateTime.Now;
    }
}
