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

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        
        internal ClockSettings Settings { get; set; }
        
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("h\\:mm");

            AlarmMessageTextBox.Text = Settings.AlarmMessage;

            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;

            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно указано время срабатывания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AlarmTimeTextBox.Focus();
                AlarmTimeTextBox.SelectAll();
                return;
            }

            Settings.AlarmTime = new TimeSpan(alarmTime.Hours, alarmTime.Minutes, 0);
            if (!string.IsNullOrEmpty(AlarmMessageTextBox.Text))
            {
                Settings.AlarmMessage = AlarmMessageTextBox.Text;
            }
            else
            {
                Settings.AlarmMessage = "Сработал будильник";
            }
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }

        private void IsSoundActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                // Открываем диалог выбора файла
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Audio Files|*.wav;*.mp3;*.wma"; // Форматы звуковых файлов
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Settings.soundPath = openFileDialog.FileName;
                    }
                }
            }
            else
            {
                Settings.soundPath = "";
            }
        }
    }
}

