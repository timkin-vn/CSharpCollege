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

        // Секундомер
        private Timer stopwatchTimer = new Timer();
        private TimeSpan stopwatchElapsed = TimeSpan.Zero;

        public ClockForm()
        {
            InitializeComponent();

            var initialTime = _settings.AlarmTime;
            _settings.AlarmTime = new TimeSpan(initialTime.Hours, initialTime.Minutes, 0);

            stopwatchTimer.Interval = 1000;
            stopwatchTimer.Tick += StopwatchTimer_Tick;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_settings.IsAlarmActive)
                return;

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
                return;

            UpdateView();
        }

        private void UpdateView()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }

        // Секундомер
        private void StartStopwatchButton_Click(object sender, EventArgs e)
        {
            stopwatchTimer.Start();
        }

        private void StopStopwatchButton_Click(object sender, EventArgs e)
        {
            stopwatchTimer.Stop();
        }

        private void ResetStopwatchButton_Click(object sender, EventArgs e)
        {
            stopwatchTimer.Stop();
            stopwatchElapsed = TimeSpan.Zero;
            StopwatchLabel.Text = "00:00:00";
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            stopwatchElapsed = stopwatchElapsed.Add(TimeSpan.FromSeconds(1));
            StopwatchLabel.Text = stopwatchElapsed.ToString(@"hh\:mm\:ss");
        }
    }
}
