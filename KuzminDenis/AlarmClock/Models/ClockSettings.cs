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
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Время срабатывания
        /// </summary>
        public TimeSpan AlarmTime { get; set; } = DateTime.Now.AddMinutes(1).TimeOfDay;

        /// <summary>
        /// Включено ли ожидание срабатывания
        /// </summary>
        public bool IsAlarmON { get; set; }

        /// <summary>
        /// Включен ли звук при срабатывании
        /// </summary>
        public bool IsSoundON { get; set; }

        /// <summary>
        /// Произошло ли срабатывание
        /// </summary>
        public bool IsAwakeActivated { get; set; }

    }
}
