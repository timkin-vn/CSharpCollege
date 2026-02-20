using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Model
{
    internal class AlarmClockState
    {
        /// <summary>
        /// Время срабатывания
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// Сообщение при срабатывании
        /// </summary>
        public string AlarmMessage { get; set; }

        /// <summary>
        /// Включен режим ожидания срабатывания
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Срабатывание сопровождается звуковым сигналом
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Срабатывание будильника зафиксировано
        /// </summary>
        public bool IsAwakeActivated { get; set; }
    }
}
