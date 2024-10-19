using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    /// <summary>
    /// Настройки будильника
    /// </summary>
    internal class AlarmSettings
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public DateTime AlarmTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Сообщение напоминания
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Режим ожидания
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Звуковой сигнал включен
        /// </summary>
        public bool IsSoundActive { get; set; }
    }
}
