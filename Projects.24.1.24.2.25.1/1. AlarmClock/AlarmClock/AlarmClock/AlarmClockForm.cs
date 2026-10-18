using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class AlarmClockForm : Form
    {
        private DateTime _lastTriggeredDate = DateTime.MinValue;
        private AlarmState _alarmState = new AlarmState();

        public AlarmClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            DisplayLabel.Text = now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive)
            {
                return;
            }

            bool isRepeatEnabled =
                _alarmState.RepeatDays != null &&
                _alarmState.RepeatDays.Length > 0;

            bool isCorrectDay =
                !isRepeatEnabled ||
                _alarmState.RepeatDays.Contains(now.DayOfWeek);

            bool isCorrectTime =
                now.Hour == _alarmState.AlarmTime.Hour &&
                now.Minute == _alarmState.AlarmTime.Minute;

            bool alreadyTriggeredToday =
                _lastTriggeredDate.Date == now.Date;

            if (!_alarmState.IsAwakeActivated &&
                isCorrectDay &&
                isCorrectTime &&
                !alreadyTriggeredToday)
            {
                _alarmState.IsAwakeActivated = true;
                _lastTriggeredDate = now.Date;

                var awakeForm = new AwakeForm();
                awakeForm.AlarmState = _alarmState;
                awakeForm.FormClosed += AwakeForm_FormClosed;

                awakeForm.ShowDialog();
            }

            if (_alarmState.IsSoundActive &&
                _alarmState.IsAwakeActivated)
            {
                System.Media.SystemSounds.Beep.Play();
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
            if (_alarmState.IsAlarmActive)
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
