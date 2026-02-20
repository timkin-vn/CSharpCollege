using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Будильник.Models;

namespace Будильник.Forms
{
    public partial class SettingsForm : Form
    {
        public AlarmState AlarmState {  get; set; }
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (AlarmState.AlarmTime < DateTime.Now)
            {
                AlarmTimeTextBox.Text = DateTime.Now.AddHours(1).ToShortTimeString();
            }
            else
            {
                AlarmTimeTextBox.Text = AlarmState.AlarmTime.ToShortTimeString();
            }

            AlarmMessageTextBox.Text = AlarmState.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = AlarmState.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = AlarmState.IsSoundActive;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Введено неверное время", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (alarmTime > DateTime.Now.TimeOfDay)
            {
                AlarmState.AlarmTime = DateTime.Today + alarmTime;
            }
            else
            {
                AlarmState.AlarmTime = DateTime.Today.AddDays(1) + alarmTime;
            }

            AlarmState.AlarmMessage = AlarmMessageTextBox.Text;
            AlarmState.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            AlarmState.IsSoundActive = IsSoundActiveCheckBox.Checked;

            AlarmState.IsSnoozed = false;

            DialogResult = DialogResult.OK;
        }
    }
}




