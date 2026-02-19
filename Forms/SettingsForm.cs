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
        internal AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString(@"h\:mm");
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AlarmTimeTextBox.Focus();
                AlarmTimeTextBox.SelectAll();
                return;
            }

            Settings.AlarmTime = new TimeSpan(alarmTime.Hours, alarmTime.Minutes, 0);
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }

        private void IsSoundActiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
