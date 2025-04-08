using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    internal class ClockSettings
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public TimeSpan AlarmTime { get; set; } = DateTime.Now.AddMinutes(1).TimeOfDay;

        /// <summary>
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Включен ли режим ожидания
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Включен ли звуковой сигнал
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Произошло ли срабатывание
        /// </summary>
        public bool IsAwakeActivated { get; set; }
    }
}
