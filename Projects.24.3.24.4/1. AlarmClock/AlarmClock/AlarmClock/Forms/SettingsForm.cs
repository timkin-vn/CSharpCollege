using AlarmClock.Model;
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
        internal AlarmClockState ClockState { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (ClockState.AlarmTime > DateTime.Now)
            {
                AlarmTimeTextBox.Text = ClockState.AlarmTime.ToShortTimeString();
            }
            else
            {
                AlarmTimeTextBox.Text = DateTime.Now.AddHours(1).ToShortTimeString();
            }

            AlarmMessageTextBox.Text = ClockState.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = ClockState.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = ClockState.IsSoundActive;

            foreach (var day in ClockState.RepeatDays)
            {
                switch (day)
                {
                    case DayOfWeek.Monday:
                        checkedListBoxDays.SetItemChecked(0, true);
                        break;
                    case DayOfWeek.Tuesday:
                        checkedListBoxDays.SetItemChecked (1, true);
                        break;
                    case DayOfWeek.Wednesday:
                        checkedListBoxDays.SetItemChecked (2, true);
                        break;
                    case DayOfWeek.Thursday:
                        checkedListBoxDays.SetItemChecked (3, true);
                        break;
                    case DayOfWeek.Friday:
                        checkedListBoxDays.SetItemChecked (4, true);
                        break;
                    case DayOfWeek.Saturday:
                        checkedListBoxDays.SetItemChecked (5, true);
                        break;
                    case DayOfWeek.Sunday:
                        checkedListBoxDays.SetItemChecked (6, true);
                        break;
                }
            }
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Время задано неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (alarmTime > DateTime.Now.TimeOfDay)
            {
                ClockState.AlarmTime = DateTime.Today + alarmTime;
            }
            else
            {
                ClockState.AlarmTime = DateTime.Today.AddDays(1) + alarmTime;
            }

            ClockState.AlarmMessage = AlarmMessageTextBox.Text;
            ClockState.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            ClockState.IsSoundActive = IsSoundActiveCheckBox.Checked;

            ClockState.RepeatDays.Clear();
            foreach (var item in checkedListBoxDays.CheckedItems)
            {
                switch (item.ToString())
                {
                    case "Понедельник":
                        ClockState.RepeatDays.Add(DayOfWeek.Monday);
                        break;
                    case "Вторник":
                        ClockState.RepeatDays.Add(DayOfWeek.Tuesday);
                        break;
                    case "Среда":
                        ClockState.RepeatDays.Add(DayOfWeek.Wednesday);
                        break;
                    case "Четверг":
                        ClockState.RepeatDays.Add(DayOfWeek.Thursday);
                        break;
                    case "Пятница":
                        ClockState.RepeatDays.Add(DayOfWeek.Friday);
                        break;
                    case "Суббота":
                        ClockState.RepeatDays.Add(DayOfWeek.Saturday);
                        break;
                    case "Воскресенье":
                        ClockState.RepeatDays.Add(DayOfWeek.Sunday);
                        break;
                }
            }

            ClockState.IsRepeating = ClockState.RepeatDays.Any();

            DialogResult = DialogResult.OK;
        }

        private void checkedListBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            checkedListBoxDays.ClearSelected();
        }
    }
}
