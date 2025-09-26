using Alarm_clock_C_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_clock_C_.Forms
{
    public partial class settingsForm : Form
    {
        internal ClockSettings Settings { get; set; }

        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("h\\:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;

            LoadSounds();

            if (!string.IsNullOrEmpty(Settings.SelectedSounds))
            {
                ChoiceSounds.SelectedItem = Settings.SelectedSounds;
            }
        }

        private void LoadSounds()
        {
            string soundDirectory = Path.Combine(Application.StartupPath, "Sounds");
            if (Directory.Exists(soundDirectory))
            {
                string[] soundsFiles = Directory.GetFiles(soundDirectory, "*.wav");
                foreach (string soundFile in soundsFiles)
                {
                    ChoiceSounds.Items.Add(Path.GetFileName(soundFile));
                }
            }
            if (ChoiceSounds.Items.Count > 0)
            {
                ChoiceSounds.SelectedIndex = 0;
            }
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно указано время!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AlarmTimeTextBox.Focus();
                AlarmTimeTextBox.SelectAll();
                return;
            }

            Settings.AlarmTime = new TimeSpan(alarmTime.Hours, alarmTime.Minutes, 0);
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;
            Settings.SelectedSounds = ChoiceSounds.SelectedItem?.ToString();

            DialogResult = DialogResult.OK;
        }
    }
}
