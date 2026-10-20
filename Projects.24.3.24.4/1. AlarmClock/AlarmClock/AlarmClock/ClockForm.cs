using AlarmClock.Forms;
using AlarmClock.Model;
using System;
using System.Media;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private readonly AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_clockState.IsAlarmActive)
            {
                return;
            }

            if (!_clockState.IsAwakeActivated && DateTime.Now >= _clockState.AlarmTime)
            {
                _clockState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm { ClockState = _clockState };
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;

            if (_clockState.IsSnoozeRequested)
            {
                _clockState.AlarmTime = DateTime.Now.AddMinutes(_clockState.SnoozeMinutes);
                _clockState.IsAwakeActivated = false;
                _clockState.IsSnoozeRequested = false;
                _clockState.IsAlarmActive = true;
            }
            else
            {
                _clockState.IsAlarmActive = false;
                _clockState.IsAwakeActivated = false;
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
            var settingsForm = new SettingsForm { ClockState = _clockState };

            if (settingsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            if (_clockState.IsAlarmActive)
            {
                Text = $"Будильник. Срабатывание в {_clockState.AlarmTime.ToShortTimeString()}, отложить на {_clockState.SnoozeMinutes} мин.";
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}
