using Alarm_clock_C_.Forms;
using Alarm_clock_C_.Models;
using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Alarm_clock_C_
{
    public partial class ClockForm : Form
    {
        private ClockSettings _settings = new ClockSettings();
        private SoundPlayer _player;

        public ClockForm()
        {
            InitializeComponent();
            _player = new SoundPlayer();
            var initialTime = _settings.AlarmTime;
            _settings.AlarmTime = new TimeSpan(initialTime.Hours, initialTime.Minutes, 0);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToLongTimeString();
            if (!_settings.IsAlarmActive)
            {
                return;
            }

            if (!_settings.IsAwakeActivated && DateTime.Now.Hour >= _settings.AlarmTime.Hours && DateTime.Now.Minute >= _settings.AlarmTime.Minutes)
            {
                _settings.IsAwakeActivated = true;
                var awakeForm = new AwakeForm();
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.Shown += AwakeForm_Shown;

                awakeForm.ShowDialog(); 
            }
        }

        private void AwakeForm_Shown(object sender, EventArgs e)
        {
            if (_settings.IsSoundActive && _settings.IsAwakeActivated)
            {
                PlayAlarmSound();
            }
        }

        private void PlayAlarmSound()
        {
            if (!string.IsNullOrEmpty(_settings.SelectedSounds))
            {
                string soundFilePath = Path.Combine(Application.StartupPath, "Sounds", _settings.SelectedSounds);

                if (File.Exists(soundFilePath))
                {
                    _player.SoundLocation = soundFilePath;
                    _player.Play();
                }
                else 
                {
                    MessageBox.Show("Звуковой файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            StopSoundsPlayer();
            UpdateView();
        }

        private void StopSoundsPlayer()
        {
            _player.Stop();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new aboutForm();
            aboutForm.ShowDialog();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new settingsForm();
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

        private void timerButton_Click(object sender, EventArgs e)
        {
            var timerForm = new TimerForm();
            timerForm.ShowDialog();
        }

        private void SecButton_Click(object sender, EventArgs e)
        {
            var secForm = new SecForm();
            secForm.ShowDialog();
        }
    }
}
