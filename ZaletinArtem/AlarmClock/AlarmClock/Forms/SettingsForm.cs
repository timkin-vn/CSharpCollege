using AlarmClock.Models;
using System;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(hourInput.Text + ":" + minuteInput.Text + ":00.0", out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;

            DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
