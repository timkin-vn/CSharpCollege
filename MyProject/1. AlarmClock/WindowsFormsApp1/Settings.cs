using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Settings : Form
    {
        public AlarmStatic AlarmStatic {  get; set; }

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (AlarmStatic.AlarmTime < DateTime.Now) {
                AlarmTime.Text = DateTime.Now.AddHours(1).ToShortTimeString();
            }
            else
            {
                AlarmTime.Text = AlarmStatic.AlarmTime.ToShortTimeString();
            }

            AlarmMessage.Text = AlarmStatic.AlarmMessege;
            ClockButte.Checked = AlarmStatic.IsAlarmActive;
            SoudButt.Checked = AlarmStatic.IsSoundActive;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(AlarmTime.Text, out var alarmTime))
            {
                MessageBox.Show("Время не правельное", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (alarmTime > DateTime.Now.TimeOfDay)
            {
                AlarmStatic.AlarmTime = DateTime.Today + alarmTime;
            }
            else
            {
                AlarmStatic.AlarmTime = DateTime.Today.AddDays(1) + alarmTime;
            }
            AlarmStatic.AlarmMessege = AlarmMessage.Text;
            AlarmStatic.IsAlarmActive = ClockButte.Checked;
            AlarmStatic.IsSoundActive = SoudButt.Checked;

            DialogResult = DialogResult.OK;
        }
    }
}
