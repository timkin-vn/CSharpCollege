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
    public partial class AlarmClockForm : Form
    {
        private AlarmState _alarmState = new AlarmState();

        private int sec = 0;
        private int min = 0;
        private int hour = 0;

        public AlarmClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAlarmActive)
            {
                return;
            }

            if (!_alarmState.IsAwakeActivated && 
                DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                DateTime.Now.Minute == _alarmState.AlarmTime.Minute)
            {
                _alarmState.IsAwakeActivated = true;

                var awakeForm = new AwakeForm();
                awakeForm.AlarmState = _alarmState;
                awakeForm.FormClosed += AwakeForm_FormClosed;

                awakeForm.ShowDialog();
            }

            if (_alarmState.IsSoundActive && _alarmState.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            UpdateControls();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.AlarmState = _alarmState;

            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            if (_alarmState.IsAlarmActive)
            {
                Text = $"Будильник (ожидает срабатывания в {_alarmState.AlarmTime.ToShortTimeString()})";
            }
            else
            {
                Text = "Будильник";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            timerSeconds.Enabled = true;
        }

        private void timerSeconds_Tick(object sender, EventArgs e)
        {
            sec++;

            if (sec >= 60)
            {
                sec = 0;
                min++;

                if (min >= 60)
                {
                    min = 0;
                    hour++;
                }
            }

            SecondsLabel.Text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timerSeconds.Enabled = false;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            timerSeconds.Enabled = false;

            hour = 0;
            min = 0;
            sec = 0;

            SecondsLabel.Text = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
        }
    }
}
