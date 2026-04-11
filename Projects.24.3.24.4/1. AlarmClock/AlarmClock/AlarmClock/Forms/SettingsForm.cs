using AlarmClock.Model;
using System;
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
            SnoozeMinutesTextBox.Text = ClockState.SnoozeMinutes.ToString();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Время задано неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(SnoozeMinutesTextBox.Text, out var snoozeMinutes) || snoozeMinutes <= 0)
            {
                MessageBox.Show("Минуты для откладывания заданы неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ClockState.SnoozeMinutes = snoozeMinutes;
            ClockState.IsSnoozeRequested = false;

            DialogResult = DialogResult.OK;
        }
    }
}
