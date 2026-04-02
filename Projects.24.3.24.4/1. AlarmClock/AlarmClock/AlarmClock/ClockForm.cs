using AlarmClock.Forms;
using AlarmClock.Model;
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
        private readonly List<AlarmClockState> _alarms = new List<AlarmClockState>();
        private AlarmClockState _currentAlarm;

        public ClockForm()
        {
            InitializeComponent();
            UpdateView();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            DisplayLabel.Text = now.ToLongTimeString();

            if (_currentAlarm != null)
            {
                if (_currentAlarm.IsSoundActive)
                {
                    SystemSounds.Beep.Play();
                }

                return;
            }

            var dueAlarm = _alarms
                .Where(alarm => alarm.IsAlarmActive && !alarm.IsAwakeActivated && now >= alarm.AlarmTime)
                .OrderBy(alarm => alarm.AlarmTime)
                .FirstOrDefault();

            if (dueAlarm == null)
            {
                return;
            }

            dueAlarm.IsAwakeActivated = true;
            _currentAlarm = dueAlarm;

            var awakeForm = new AwakeForm { ClockState = dueAlarm };
            awakeForm.FormClosed += AwakeForm_FormClosed;
            awakeForm.ShowDialog();
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;

            if (_currentAlarm != null)
            {
                _alarms.Remove(_currentAlarm);
                _currentAlarm = null;
            }

            UpdateView();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm
            {
                ClockState = new AlarmClockState
                {
                    AlarmTime = DateTime.Now.AddHours(1),
                    AlarmMessage = string.Empty,
                    IsAlarmActive = true,
                    IsSoundActive = true
                }
            };

            if (settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (settingsForm.ClockState.IsAlarmActive)
            {
                _alarms.Add(settingsForm.ClockState);
            }

            UpdateView();
        }

        private void UpdateView()
        {
            var nextAlarm = _alarms
                .Where(alarm => alarm.IsAlarmActive && !alarm.IsAwakeActivated)
                .OrderBy(alarm => alarm.AlarmTime)
                .FirstOrDefault();

            if (nextAlarm != null)
            {
                Text = $"Будильник. Активно: {_alarms.Count}. Ближайший: {nextAlarm.AlarmTime:HH:mm}";
            }
            else
            {
                Text = "Будильник. Активных нет";
            }
        }
    }
}
