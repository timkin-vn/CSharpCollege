using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();
        private List<AlarmSettings> _alarms = new List<AlarmSettings>();

        public ClockForm()
        {
            InitializeComponent();

            var currentTime = DateTime.Now.AddMinutes(1).TimeOfDay;
            _settings.AlarmTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, 0);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (_settings.IsAlarmActive)
            {
                if (!_settings.IsAwakeActivated &&
                    DateTime.Now.Hour == _settings.AlarmTime.Hours &&
                    DateTime.Now.Minute == _settings.AlarmTime.Minutes)
                {
                    _settings.IsAwakeActivated = true;
                    var awakeForm = new AwakeForm();
                    awakeForm.Settings = _settings;
                    awakeForm.ShowDialog();
                }

                if (_settings.IsSoundActive && _settings.IsAwakeActivated)
                    SystemSounds.Beep.Play();
            }

            foreach (var alarm in _alarms)
            {
                if (!alarm.IsAlarmActive)
                    continue;

                if (!alarm.IsAwakeActivated &&
                    DateTime.Now.Hour == alarm.AlarmTime.Hours &&
                    DateTime.Now.Minute == alarm.AlarmTime.Minutes)
                {
                    alarm.IsAwakeActivated = true;

                    var awakeForm = new AwakeForm();
                    awakeForm.Settings = alarm;
                    awakeForm.ShowDialog();
                }

                if (alarm.IsSoundActive && alarm.IsAwakeActivated)
                    SystemSounds.Beep.Play();
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var newAlarm = new AlarmSettings();
            var settingsForm = new SettingsForm();
            settingsForm.Settings = newAlarm;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                _alarms.Add(newAlarm); // сохранение в список
            }
        }

        private void AlarmListButton_Click(object sender, EventArgs e)
        {
            var listForm = new AlarmListForm();
            listForm.Alarms = _alarms;
            listForm.ShowDialog();
        }

        private void ShowControls()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }

        private void OpenAlarmListButton_Click(object sender, EventArgs e)
        {
            var listForm = new AlarmListForm();
            listForm.Alarms = _alarms;   // передаём список
            listForm.ShowDialog();       
        }

    }
}
