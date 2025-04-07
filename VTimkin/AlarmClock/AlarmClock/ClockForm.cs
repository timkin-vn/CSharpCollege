using AlarmClock.Forms;
using AlarmClock.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();

            if (_settings.IsAlarmActive)
            {
                DateTime now = DateTime.Now;

                if (now.Hour == _settings.AlarmTime.Hour &&
                    now.Minute == _settings.AlarmTime.Minute &&
                    now.Second == 0)
                {
                    _settings.IsAlarmActive = false;

                    if (_settings.IsSoundActive)
                    {
                        if (!string.IsNullOrEmpty(_settings.SoundFilePath) && System.IO.File.Exists(_settings.SoundFilePath))
                        {
                            try
                            {
                                var player = new System.Media.SoundPlayer(_settings.SoundFilePath);
                                player.Play();
                            }
                            catch
                            {
                                System.Media.SystemSounds.Exclamation.Play();
                            }
                        }
                        else
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }
                    }

                    string message = string.IsNullOrWhiteSpace(_settings.AlarmMessage)
                        ? "Будильник!"
                        : _settings.AlarmMessage;

                    MessageBox.Show(message, "Сработал будильник!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            form.ShowDialog();
        }

        private void DisplayLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
