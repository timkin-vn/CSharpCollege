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
        private TimeSpan _countdownTime = TimeSpan.Zero;
        private bool _isCountdownRunning = false;

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

            if (!_settings.IsAlarmActive) return;

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

            if (settingsForm.ShowDialog() != DialogResult.OK) return;

            UpdateView();
        }

        private void UpdateView()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }

        // === Таймер обратного отсчета ===

        private void StartCountdownButton_Click(object sender, EventArgs e)
        {
            if (_isCountdownRunning)
            {
                countdownTimer.Stop();
                _isCountdownRunning = false;
                StartCountdownButton.Text = "Старт";
                return;
            }

            int hours = (int)hoursNumeric.Value;
            int minutes = (int)minutesNumeric.Value;
            int seconds = (int)secondsNumeric.Value;

            if (hours == 0 && minutes == 0 && seconds == 0)
            {
                MessageBox.Show("Установите время для таймера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _countdownTime = new TimeSpan(hours, minutes, seconds);
            _isCountdownRunning = true;
            StartCountdownButton.Text = "Стоп";
            countdownTimer.Start();
        }

        private void ResetCountdownButton_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();
            _isCountdownRunning = false;
            _countdownTime = TimeSpan.Zero;
            StartCountdownButton.Text = "Старт";
            UpdateCountdownDisplay();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (_countdownTime.TotalSeconds > 0)
            {
                _countdownTime = _countdownTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateCountdownDisplay();
            }
            else
            {
                countdownTimer.Stop();
                _isCountdownRunning = false;
                StartCountdownButton.Text = "Старт";
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Время вышло!", "Таймер",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateCountdownDisplay()
        {
            CountdownLabel.Text = _countdownTime.ToString(@"hh\:mm\:ss");
        }
    }
}