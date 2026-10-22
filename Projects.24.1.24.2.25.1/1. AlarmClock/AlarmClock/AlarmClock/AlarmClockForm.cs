using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class AlarmClockForm : Form
    {
        private Models.AlarmState _alarmState = new Models.AlarmState();

        public AlarmClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive)
                return;

            if (!_alarmState.IsAwakeActivated &&
                DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                DateTime.Now.Minute == _alarmState.AlarmTime.Minute)
            {
                _alarmState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm();
                awakeForm.AlarmState = _alarmState;
                awakeForm.FormClosed += AwakeForm_FormClosed;

                awakeForm.ShowDialog();
            }

            if (_alarmState.IsSoundActive && _alarmState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateControls();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.AlarmState = _alarmState;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            if (_alarmState.IsSnoozed)
            {
                Text = $"Будильник отложен до {_alarmState.AlarmTime.ToShortTimeString()}";
            }
            else if (_alarmState.IsAlarmActive)
            {
                Text = $"Будильник (ожидает срабатывания в {_alarmState.AlarmTime.ToShortTimeString()})";
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}