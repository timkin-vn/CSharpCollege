using System;
using System.Collections.Generic;

namespace AlarmClock.Models
{
    internal class ClockSettings
    {
        /// <summary>
        /// Список времен срабатывания
        /// </summary>
        public List<DateTime> AlarmTimes { get; set; } = new List<DateTime>();

        /// <summary>
        /// Список флагов, указывающих, сработал ли соответствующий будильник
        /// </summary>
        public List<bool> AlarmTriggered { get; set; } = new List<bool>();

        /// <summary>
        /// Список сообщений для каждого будильника
        /// </summary>
        public List<string> AlarmMessages { get; set; } = new List<string>();

        /// <summary>
        /// Включен ли звуковой сигнал
        /// </summary>
        public bool IsSoundActive { get; set; }

        public string GetAlarmDisplayString(int index)
        {
            if (index >= 0 && index < AlarmTimes.Count && index < AlarmMessages.Count)
            {
                return $"{AlarmTimes[index]:HH:mm} - {AlarmMessages[index]}";
            }
            return "";
        }
    }
}