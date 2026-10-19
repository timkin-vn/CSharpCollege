using System;
using System.Media;
using System.Windows.Forms;
using AlarmClock.Forms;
using AlarmClock.Model;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (_clockState.IsAlarmActive == false)
            {
                return;
            }

            if (_clockState.IsAwakeActivated == false &&
                DateTime.Now.Minute == _clockState.AlarmTime.Minute &&
                DateTime.Now.Hour == _clockState.AlarmTime.Hour)
            {
                _clockState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm { ClockState = _clockState };
                awakeForm.ShowDialog();

                _clockState.IsAlarmActive = false;
                _clockState.IsAwakeActivated = false;
                UpdateView();
            }

            if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
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
                Text = "Будильник. Ожидается срабатывание в " + _clockState.AlarmTime.ToShortTimeString();
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}