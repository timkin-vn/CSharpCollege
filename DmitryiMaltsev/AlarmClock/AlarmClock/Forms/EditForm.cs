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
    public partial class EditForm : Form
    {
        public AlarmTime Setting { get; set; }
        public EditForm()
        {
            InitializeComponent();
           

        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Время указано неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Setting.Times = alarmTime;
            Setting.AlarmMessage = AlarmMessageTextBox.Text;
            Setting.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            DialogResult = DialogResult.OK;
            
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Setting.Times.ToString("HH:mm");
            AlarmMessageTextBox.Text = Setting.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Setting.IsAlarmActive;
        }
    }
}
