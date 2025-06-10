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
    public partial class ClockForm : Form
    {
        private ClockSettings _settings = new ClockSettings();

        
        private TimeSpan _stopwatchTime = TimeSpan.Zero;
        private bool _isStopwatchRunning = false;

        public ClockForm()
        {
            InitializeComponent();
            var initialTime = _settings.AlarmTime;
            _settings.AlarmTime = new TimeSpan(initialTime.Hours, initialTime.Minutes, 0);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_settings.IsAlarmActive)
            {
                return;
            }

            if (!_settings.IsAwakeActivated &&
                DateTime.Now.Hour == _settings.AlarmTime.Hours &&
                DateTime.Now.Minute == _settings.AlarmTime.Minutes)
            {
                _settings.IsAwakeActivated = true;

                var awakeForm = new AwakeForm();
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;

                awakeForm.ShowDialog();
            }

            if (_settings.IsSoundActive && _settings.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateView();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Settings = _settings;

            if (settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }

        // === Секундомер ===

        private void StartStopwatchButton_Click(object sender, EventArgs e)
        {
            _isStopwatchRunning = true;
            stopwatchTimer.Start();
        }

        private void ResetStopwatchButton_Click(object sender, EventArgs e)
        {
            stopwatchTimer.Stop();
            _isStopwatchRunning = false;
            _stopwatchTime = TimeSpan.Zero;
            UpdateStopwatchDisplay();
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            if (_isStopwatchRunning)
            {
                _stopwatchTime = _stopwatchTime.Add(TimeSpan.FromSeconds(1));
                UpdateStopwatchDisplay();
            }
        }

        private void UpdateStopwatchDisplay()
        {
            StopwatchLabel.Text = _stopwatchTime.ToString(@"hh\:mm\:ss");
        }
    }
}