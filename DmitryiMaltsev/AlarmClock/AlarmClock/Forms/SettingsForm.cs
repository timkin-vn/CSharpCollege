using AlarmClock.Models;
using System;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            
        }

        public AlarmSettings Settings { get; set; }

        public AlarmTime SettingsAlarm { get; set; }
        public int Index = -1;
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Время указано неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Index != -1)
            {
                Settings.TimeSettings[Index].Times = alarmTime;
                Settings.TimeSettings[Index].AlarmMessage = AlarmMessageTextBox.Text;
                Settings.TimeSettings[Index].IsAlarmActive = IsAlarmActiveCheckBox.Checked;
                Index = -1;
                DialogResult = DialogResult.OK;
                return;
            }

            SettingsAlarm.Times = alarmTime;
            SettingsAlarm.AlarmMessage = AlarmMessageTextBox.Text;
            SettingsAlarm.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            DialogResult = DialogResult.OK;

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
           
        }
    }
}
