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
        private AppTheme _currentTheme = AppTheme.Light;

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
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

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

        private void ThemeButton_Click(object sender, EventArgs e)
        {
            _currentTheme = _currentTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            if (_currentTheme == AppTheme.Dark)
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                this.ForeColor = Color.White;
                DisplayLabel.BackColor = Color.FromArgb(40, 40, 40);
                DisplayLabel.ForeColor = Color.LightSteelBlue;
                SettingsButton.BackColor = Color.FromArgb(64, 64, 64);
                SettingsButton.ForeColor = Color.White;
                AboutButton.BackColor = Color.FromArgb(64, 64, 64);
                AboutButton.ForeColor = Color.White;
                ExitButton.BackColor = Color.FromArgb(64, 64, 64);
                ExitButton.ForeColor = Color.White;
                ThemeButton.BackColor = Color.FromArgb(64, 64, 64);
                ThemeButton.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = Color.Black;
                DisplayLabel.BackColor = Color.Black;
                DisplayLabel.ForeColor = Color.LightSteelBlue;
                SettingsButton.BackColor = SystemColors.Control;
                SettingsButton.ForeColor = Color.Black;
                AboutButton.BackColor = SystemColors.Control;
                AboutButton.ForeColor = Color.Black;
                ExitButton.BackColor = SystemColors.Control;
                ExitButton.ForeColor = Color.Black;
                ThemeButton.BackColor = SystemColors.Control;
                ThemeButton.ForeColor = Color.Black;
            }
        }

        private void ShowControls()
        {
            Text = _settings.IsAlarmActive ? "Будильник (ожидание)" : "Будильник";
        }
    }

    public enum AppTheme
    {
        Light,
        Dark
    }
}
