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
        private AwakeForm _awakeForm = null;

        public ClockForm()
        {
            InitializeComponent();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
            if (_settings.IsAlarmActive && DateTime.Now.TimeOfDay >= _settings.AlarmTime.TimeOfDay)
            {
                if (_awakeForm == null || _awakeForm.IsDisposed)
                {
                    _awakeForm = new AwakeForm();
                    _awakeForm.Settings = _settings;
                }

                _awakeForm.Show();

                if (_settings.IsSoundActive)
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        private void SnoozeButton_Click(object sender, EventArgs e)
        {
            if (_settings.IsAlarmActive)
            {
                _settings.AlarmTime = _settings.AlarmTime.AddMinutes(5);
                MessageBox.Show("Alarm snoozed for 5 minutes.");
            }
        }
        private void ChangeSoundButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 files (*.MP3)|*.MP3";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _settings.AlarmSoundFilePath = openFileDialog.FileName;
                    MessageBox.Show("Alarm sound changed.");
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
    }
}
