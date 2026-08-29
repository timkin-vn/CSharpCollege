using AlarmClock.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using AlarmClock.Forms;
using Budilnik.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private readonly ClockSettings _settings = new ClockSettings();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (_settings.AlarmTimes.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _settings.AlarmTimes.Count; i++)
            {
                var alarmTime = _settings.AlarmTimes[i];
                var isTriggered = _settings.AlarmTriggered[i];

                if (!isTriggered && DateTime.Now.Hour == alarmTime.Hour && DateTime.Now.Minute == alarmTime.Minute)
                {
                    _settings.AlarmTriggered[i] = true;

                    var awakeForm = new AwakeForm();
                    awakeForm.Settings = _settings;
                    awakeForm.AlarmMessage = _settings.AlarmMessages[i];
                    awakeForm.FormClosed += AwakeForm_FormClosed;
                    awakeForm.ShowDialog();

                    _settings.AlarmTimes.RemoveAt(i);
                    _settings.AlarmTriggered.RemoveAt(i);
                    _settings.AlarmMessages.RemoveAt(i);
                    UpdateView();
                    return;
                }
            }

            if (_settings.IsSoundActive && _settings.AlarmTriggered.Any(triggered => triggered))
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;

            for (int i = 0; i < _settings.AlarmTriggered.Count; i++)
            {
                _settings.AlarmTriggered[i] = false;
            }

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
            Text = _settings.AlarmTimes.Count > 0 ? "Будильник (ожидание)" : "Будильник";
        }
    }
}