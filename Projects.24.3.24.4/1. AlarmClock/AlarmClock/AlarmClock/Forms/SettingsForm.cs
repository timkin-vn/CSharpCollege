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

            DialogResult = DialogResult.OK;
        }
    }
}
