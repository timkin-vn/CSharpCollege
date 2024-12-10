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
        private AlarmSettings _settings = new AlarmSettings();
        private DateTime _timerStartTime;
        private AwakeForm _awakeForm = null;
        private TimeSpan _stopwatchElapsedTime = TimeSpan.Zero; // Добавлено
        private bool _isStopwatchRunning = false; // Добавлено

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
            if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
            {
                if (_awakeForm == null || _awakeForm.IsDisposed)
                {
                    _awakeForm = new AwakeForm();
                    _awakeForm.Settings = _settings;
                }

                _awakeForm.Show();

                if (_settings.IsSoundActive)
                {
                    SystemSounds.Beep.Play();
                }
            }

            if (_settings.IsTimerActive && _timerStartTime != DateTime.MinValue)
            {
                if (DateTime.Now - _timerStartTime >= _settings.TimerDuration)
                {
                    if (_awakeForm == null || _awakeForm.IsDisposed)
                    {
                        _awakeForm = new AwakeForm();
                        _awakeForm.Settings = _settings;
                    }

                    _awakeForm.Show();

                    if (_settings.IsSoundActive)
                    {
                        PlaySelectedSound();
                    }

                    _timerStartTime = DateTime.MinValue; // Сброс таймера
                }
            }
        }

        private void PlaySelectedSound()
        {
            switch (_settings.SelectedSound)
            {
                case "Beep":
                    SystemSounds.Beep.Play();
                    break;
                case "Hand":
                    SystemSounds.Hand.Play();
                    break;
                case "Question":
                    SystemSounds.Question.Play();
                    break;
                case "Exclamation":
                    SystemSounds.Exclamation.Play();
                    break;
                case "Asterisk":
                    SystemSounds.Asterisk.Play();
                    break;
                default:
                    SystemSounds.Beep.Play();
                    break;
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            form.ShowDialog();
        }

        private void StartTimerButton_Click(object sender, EventArgs e)
        {
            _timerStartTime = DateTime.Now;
        }

        private void StartStopwatchButton_Click(object sender, EventArgs e)
        {
            if (!_isStopwatchRunning)
            {
                _isStopwatchRunning = true;
                stopwatchTimer.Start();
            }
        }

        private void StopStopwatchButton_Click(object sender, EventArgs e)
        {
            if (_isStopwatchRunning)
            {
                _isStopwatchRunning = false;
                stopwatchTimer.Stop();
            }
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            if (_isStopwatchRunning)
            {
                _stopwatchElapsedTime = _stopwatchElapsedTime.Add(TimeSpan.FromSeconds(1));
                stopwatchLabel.Text = _stopwatchElapsedTime.ToString(@"hh\:mm\:ss");
            }
        }

        private void stopwatchLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
