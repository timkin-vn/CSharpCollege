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
        private AlarmClockState _clockState = new AlarmClockState();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Отображает текущее время
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_clockState.IsAlarmActive)
            {
                return;
            }

            // Когда наступает время сработать, отображает форму срабатывания
            if (!_clockState.IsAwakeActivated &&
                DateTime.Now.Minute == _clockState.AlarmTime.Minute &&
                DateTime.Now.Hour == _clockState.AlarmTime.Hour)
            {
                _clockState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm { ClockState = _clockState };
                awakeForm.FormClosed += AwakeForm_FormClosed;

                awakeForm.ShowDialog();
            }

            // Если требуется, воспроизвести системный звук
            if (_clockState.IsSoundActive && _clockState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;

            _clockState.IsAlarmActive = false;
            _clockState.IsAwakeActivated = false;
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
                Text = $"Будильник. Ожидается срабатывание в {_clockState.AlarmTime.ToShortTimeString()}";
            }
            else
            {
                Text = "Будильник";
            }
        }
    }
}
