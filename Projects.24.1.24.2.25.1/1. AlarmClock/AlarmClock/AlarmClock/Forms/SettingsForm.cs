using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public AlarmState AlarmState { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            if (AlarmState.AlarmTime < now)
            {
                AlarmTimeTextBox.Text =
                    now.AddHours(1).ToShortTimeString();
            }
            else
            {
                AlarmTimeTextBox.Text =
                    AlarmState.AlarmTime.ToShortTimeString();
            }

            AlarmMessageTextBox.Text = AlarmState.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = AlarmState.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = AlarmState.IsSoundActive;

            RepeatDaysCheckedListBox.Items.Clear();

            DayOfWeek[] days = {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            string[] dayNames = {
                "Понедельник",
                "Вторник",
                "Среда",
                "Четверг",
                "Пятница",
                "Суббота",
                "Воскресенье"
            };

            if (AlarmState.RepeatDays == null)
            {
                AlarmState.RepeatDays = new DayOfWeek[0];
            }

            for (int i = 0; i < days.Length; i++)
            {
                int index = RepeatDaysCheckedListBox.Items.Add(
                    new RepeatDayItem(days[i], dayNames[i]));

                bool isChecked = Array.IndexOf(
                    AlarmState.RepeatDays,
                    days[i]) >= 0;

                RepeatDaysCheckedListBox.SetItemChecked(
                    index,
                    isChecked);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(
                AlarmTimeTextBox.Text,
                out TimeSpan alarmTime))
            {
                MessageBox.Show(
                    "Введено неверное время",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (alarmTime < TimeSpan.Zero ||
                alarmTime >= TimeSpan.FromDays(1))
            {
                MessageBox.Show(
                    "Введите время от 00:00 до 23:59",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DateTime now = DateTime.Now;

            DateTime selectedTime = DateTime.Today + alarmTime;

            if (selectedTime <= now)
            {
                selectedTime = selectedTime.AddDays(1);
            }

            AlarmState.AlarmTime = selectedTime;
            AlarmState.AlarmMessage = AlarmMessageTextBox.Text;
            AlarmState.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            AlarmState.IsSoundActive = IsSoundActiveCheckBox.Checked;

            AlarmState.RepeatDays = RepeatDaysCheckedListBox.CheckedItems
                .Cast<RepeatDayItem>()
                .Select(item => item.Day)
                .ToArray();

            AlarmState.IsAwakeActivated = false;

            DialogResult = DialogResult.OK;
            Close();
        }
        private sealed class RepeatDayItem
        {
            public DayOfWeek Day { get; private set; }

            public string Name { get; private set; }

            public RepeatDayItem(DayOfWeek day, string name)
            {
                Day = day;
                Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }

}
