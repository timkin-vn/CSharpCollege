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
        private AlarmState _alarmState = new AlarmState();

        private DateTime? _stopwatchStartTime;
        private TimeSpan _stopwatchElapsed = TimeSpan.Zero;
        private bool _isStopwatchRunning = false;

        public AlarmClockForm()
        {
            InitializeComponent();

            ClockTimer.Interval = 1000;
            ClockTimer.Start();

            StopwatchTimer.Interval = 10;
            StopwatchTimer.Tick += StopwatchTimer_Tick;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive) return;

            if (!_alarmState.IsAwakeActivated &&
                DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                DateTime.Now.Minute == _alarmState.AlarmTime.Minute)
            {
                _alarmState.IsAwakeActivated = true;
                var awakeForm = new AwakeForm { AlarmState = _alarmState };
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            if (_alarmState.IsSoundActive && _alarmState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            if (_isStopwatchRunning && _stopwatchStartTime.HasValue)
            {
                var current = (DateTime.Now - _stopwatchStartTime.Value) + _stopwatchElapsed;

                Labletimer.Text = current.ToString(@"mm\:ss\.ff");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!_isStopwatchRunning)
            {
                _stopwatchStartTime = DateTime.Now;
                _isStopwatchRunning = true;
                StopwatchTimer.Start();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (_isStopwatchRunning)
            {
                StopwatchTimer.Stop();
                if (_stopwatchStartTime.HasValue)
                {
                    _stopwatchElapsed += DateTime.Now - _stopwatchStartTime.Value;
                }
                _isStopwatchRunning = false;
            }
            else
            {
                _stopwatchElapsed = TimeSpan.Zero;
                _stopwatchStartTime = null;
                Labletimer.Text = "00:00.00";
            }
        }

        private void UpdateControls()
        {
            Text = _alarmState.IsAlarmActive
                ? $"Будильник ({_alarmState.AlarmTime.ToShortTimeString()})"
                : "Будильник";
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateControls();
        }

        private void ExitButton_Click(object sender, EventArgs e) => Close();

        private void AboutButton_Click(object sender, EventArgs e) => new AboutForm().ShowDialog();

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm { AlarmState = _alarmState };
            if (form.ShowDialog() == DialogResult.OK) UpdateControls();
        }

        private void Labletimer_Click(object sender, EventArgs e) { }
    }
}
