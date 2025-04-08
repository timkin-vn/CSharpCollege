using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm_clock_C_.Models
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
        /// Включен будильник
        /// </summary>
        public bool IsAlarmActive { get; set; }

        /// <summary>
        /// Включен звук
        /// </summary>
        public bool IsSoundActive { get; set; }

        /// <summary>
        /// Окно срабатывания
        /// </summary>
        public bool IsAwakeActivated { get; set; }

        /// <summary>
        /// Выбор звука
        /// </summary>
        public string SelectedSounds { get; set; }


    }
}
