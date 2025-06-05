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
        private bool isDarkTheme = false;
        private bool use12HourFormat = false;

        public ClockForm()
        {
            InitializeComponent();
            var currentTime = DateTime.Now.AddMinutes(1).TimeOfDay;
            _settings.AlarmTime = new TimeSpan(currentTime.Hours, currentTime.Minutes, 0);
            ApplyTheme();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimeDisplay();

            if (!_settings.IsAlarmActive)
            {
                return;
            }

            if (!_settings.IsAwakeActivated && 
                DateTime.Now.Hour == _settings.AlarmTime.Hours &&
                DateTime.Now.Minute == _settings.AlarmTime.Minutes)
            {
                _settings.IsAwakeActivated = true;
                var awakeForm = new AwakeForm();
                awakeForm.Settings = _settings;
                awakeForm.FormClosed += AwakeForm_FormClosed;
                awakeForm.ShowDialog();
            }

            if (_settings.IsSoundActive && _settings.IsAwakeActivated)
            {
                SystemSounds.Beep.Play();
            }
        }

        private void AwakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= AwakeForm_FormClosed;
            ShowControls();
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

            ShowControls();
        }

        private void ShowControls()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }

        private void btnSwitchTheme_Click(object sender, EventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (isDarkTheme)
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.White;
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.BackColor = Color.FromArgb(30, 30, 30);
                    ctrl.ForeColor = Color.White;
                }
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.BackColor = SystemColors.Control;
                    ctrl.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void chkTimeFormat_CheckedChanged(object sender, EventArgs e)
        {
            use12HourFormat = chkTimeFormat.Checked;
            UpdateTimeDisplay();
        }

        private void UpdateTimeDisplay()
        {
            if (use12HourFormat)
                DisplayLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
            else
                DisplayLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
