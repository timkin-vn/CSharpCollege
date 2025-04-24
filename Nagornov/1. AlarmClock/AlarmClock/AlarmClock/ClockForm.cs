using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private readonly AlarmSettings _settings = new AlarmSettings();
        private const string AlarmWaitingText = "Будильник (ожидание)";
        private const string AlarmInactiveText = "Будильник";

        public ClockForm()
        {
            InitializeComponent();
            InitializeAlarmTime();
            UpdateFormTitle();
        }

        private void InitializeAlarmTime()
        {
            var currentTime = DateTime.Now.AddMinutes(1).TimeOfDay;
            _settings.AlarmTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, 0);
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeDisplay();

            if (ShouldTriggerAlarm())
            {
                TriggerAlarm();
            }

            PlayAlarmSoundIfNeeded();
        }

        private void UpdateTimeDisplay()
        {
            DisplayLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private bool ShouldTriggerAlarm()
        {
            return _settings.IsAlarmActive &&
                   !_settings.IsAwakeActivated &&
                   DateTime.Now.Hour == _settings.AlarmTime.Hours &&
                   DateTime.Now.Minute == _settings.AlarmTime.Minutes;
        }

        private void TriggerAlarm()
        {
            _settings.IsAwakeActivated = true;
            using (var awakeForm = new AwakeForm())
            {
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }
        }

        private void PlayAlarmSoundIfNeeded()
        {
            if (_settings.IsSoundActive && _settings.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateFormTitle();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                settingsForm.Settings = _settings;
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateFormTitle();
                }
            }
        }

        private void UpdateFormTitle()
        {
            Text = _settings.IsAlarmActive ? AlarmWaitingText : AlarmInactiveText;
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TimerButton_Click(object sender, EventArgs e)
        {
            
            }

        private void DarkThemeButton_Click(object sender, EventArgs e)
        {
            
            using (var darkteamForm = new Darkteam())
            {
                darkteamForm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var aboutForm = new X();
            aboutForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var darkteam = new Darkteam();
            darkteam.ShowDialog();
        }

        private void DisplayLabel_Click(object sender, EventArgs e)
        {

        }
    }
}