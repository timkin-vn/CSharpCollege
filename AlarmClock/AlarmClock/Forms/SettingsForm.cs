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
        public AlarmSettings Settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();

        }
        
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(AlarmTimeTextBox.Text, out var alarmTime))
            {
                MessageBox.Show("Неверно задано время срабатывания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.AlarmTime = alarmTime;
            Settings.AlarmMessage = AlarmMessageTextBox.Text;
            Settings.IsAlarmActive = IsAlarmActiveCheckBox.Checked;
            Settings.IsSoundActive = IsSoundActiveCheckBox.Checked;

            DialogResult = DialogResult.OK;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AlarmTimeTextBox.Text = Settings.AlarmTime.ToString("0H:mm");
            trackBar1.Value = Settings.AlarmTime.Hour;
            trackBar2.Value = Settings.AlarmTime.Minute;
            AlarmMessageTextBox.Text = Settings.AlarmMessage;
            IsAlarmActiveCheckBox.Checked = Settings.IsAlarmActive;
            IsSoundActiveCheckBox.Checked = Settings.IsSoundActive;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                char temp = AlarmTimeTextBox.Text[3];
                char temp2 = AlarmTimeTextBox.Text[4];

                AlarmTimeTextBox.Text = ($"{(trackBar1.Value < 10 ? "0" : "")}{trackBar1.Value.ToString()}:{ temp}{ temp2}");


            } catch (Exception ex) {
                
                MessageBox.Show($"Errrr {ex}");
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            try
            {
                char temp = AlarmTimeTextBox.Text[0];
                char temp2 = AlarmTimeTextBox.Text[1];
                AlarmTimeTextBox.Text = "";

                AlarmTimeTextBox.Text = ($"{temp}{temp2}{(trackBar2.Value < 10 ? ":0" : ":")}{trackBar2.Value.ToString()}");
            }
            catch (Exception ex) {
                
                MessageBox.Show($"I don`t know\n{ex}");
            }
        }

    }
}
    

