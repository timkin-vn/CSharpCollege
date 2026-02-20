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
using Будильник.Forms;
using Будильник.Models;

namespace Будильник
{


    public partial class AlarmClockForm : Form
    {
        private AlarmState _alarmState = new AlarmState();


        public AlarmClockForm()
        {
            InitializeComponent();
            UpdateControls();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (!_alarmState.IsAwakeActivated &&
                DateTime.Now.Hour == _alarmState.AlarmTime.Hour &&
                DateTime.Now.Minute == _alarmState.AlarmTime.Minute)
            {
                _alarmState.IsAwakeActivated = true;

                _alarmState.IsSnoozed = false;

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

        private void StopButton_Click(object sender, EventArgs e)
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
                if (_alarmState.IsSnoozed)
                {

                    Text = $"Будильник (отложен до {_alarmState.AlarmTime.ToShortTimeString()})";
                }
                else
                {

                    Text = $"Будильник (ожидает срабатывания в {_alarmState.AlarmTime.ToShortTimeString()})";
                }
            }
            else
            {
                Text = "Будильник";
            }
        }

        private void DisplayLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
