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
using WMPLib;

namespace AlarmClock
{
    public partial class ClockForm1 : Form
    {
        private WMPLib.WindowsMediaPlayer _mediaPlayer;

        private ClockSettings _settings = new ClockSettings();

        public ClockForm1()
        {
            InitializeComponent();
            var InitialTime = _settings.AlarmTime;
            _settings.AlarmTime = new TimeSpan(InitialTime.Hours, InitialTime.Minutes, 0);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ClockLabel.Text = DateTime.Now.ToLongTimeString();

            if(!_settings.IsAlarmON)
            {
                return;
            }

            if(!_settings.IsAwakeActivated &&
                DateTime.Now.Hour == _settings.AlarmTime.Hours && 
                DateTime.Now.Minute == _settings.AlarmTime.Minutes)
            {
                _settings.IsAwakeActivated = true;

                var awakeForm = new AwakeForm();
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            if(_settings.IsSoundON && _settings.IsAwakeActivated)
            {
                PlaySelectedSound();
            }
        }

        private void PlaySelectedSound()
        {
            if (string.IsNullOrEmpty(_settings.SelectedSoundPath)) return;

            try
            {
                if (_mediaPlayer == null)
                {
                    _mediaPlayer = new WMPLib.WindowsMediaPlayer();
                    _mediaPlayer.URL = _settings.SelectedSoundPath;
                    _mediaPlayer.settings.volume = 100;
                    _mediaPlayer.controls.play();
                }
                else if (_mediaPlayer.playState != WMPLib.WMPPlayState.wmppsPlaying)
                {
                    _mediaPlayer.controls.play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка воспроизведения: {ex.Message}");
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateView();
            StopSound();
        }

        private void StopSound()
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.controls.stop();
                _mediaPlayer = null;
            }
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

            if(settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            UpdateView();
        }

        private void UpdateView()
        {
            Text = _settings.IsAlarmON ? $"Будильник сработает в { _settings.AlarmTime}"  : "Будильник";
        }

        private void SoundSettingsButton_Click(object sender, EventArgs e)
        {
            var soundSettingsForm = new SoundSettingsForm();
            soundSettingsForm.Settings = _settings;
            soundSettingsForm.ShowDialog();
        }
    }
}
