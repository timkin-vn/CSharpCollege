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

        /// <summary>
        /// Повтор по дням недели
        /// </summary>
        public bool IsRepeating { get; set; }

        /// <summary>
        /// Дни из недели, в которые будильник будет срабатывать
        /// </summary>
        public List<DayOfWeek> RepeatDays { get; set; } = new List<DayOfWeek>();

        /// <summary>
        /// Время последнего срабатывания будильника
        /// </summary>
        public DateTime? LastTriggered { get; set; } 

        /// <summary>
        /// Проверка на должен ли будильник сегодня сработать
        /// </summary>
        public bool ShouldTriggerNow()
        {
            if (!IsAlarmActive)
                return false;

            DateTime now = DateTime.Now;

            bool isTimeMatch = now.Hour == AlarmTime.Hour && now.Minute == AlarmTime.Minute;

            if (!isTimeMatch)
                return false;

            if (LastTriggered.HasValue && LastTriggered.Value.Date == now.Date && LastTriggered.Value.Hour == now.Hour && LastTriggered.Value.Minute == now.Minute)
            {
                return false;
            }

            if (!IsRepeating)
            {
                return now.Date == AlarmTime.Date;
            }

            return RepeatDays.Contains(now.DayOfWeek);

        }

        /// <summary>
        /// Вызывается при срабатывании
        /// </summary>
        public void Trigger()
        {
            LastTriggered = DateTime.Now;
            IsAlarmActive = true;

            if (!IsRepeating)
            {
                IsAlarmActive = false;
            }
        }
    }
}
