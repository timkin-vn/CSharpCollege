using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private ClockSettings _settings = new ClockSettings();
        private bool _isTimePaused = false;

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
            if (_isTimePaused)
            {
                return;
            }

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

        private void PauseResumeButton_Click_1(object sender, EventArgs e)
        {
            _isTimePaused = !_isTimePaused;

            PauseResumeButton.Text = _isTimePaused ? "Возобновить" : "Пауза";
        }
    }
}
