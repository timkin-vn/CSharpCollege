using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Models
{
    internal class AlarmSettings
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public TimeSpan AlarmTime { get; set; }

        /// <summary>
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Включен ли режим будильника
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Включен ли звуковой сигнал при срабатывании
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Сработал ли будильник
        /// </summary>
        public bool IsAwakeActivated { get; set; }
    }
}
