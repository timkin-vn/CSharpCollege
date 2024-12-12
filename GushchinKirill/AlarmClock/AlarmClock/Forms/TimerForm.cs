using AlarmClock.Properties;
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


namespace AlarmClock.Forms
{
    public partial class TimerForm : Form
    {
        private DateTime time;
        private DateTime pauseTime;
        public TimerForm()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            
            if (!DateTime.TryParse(TimerTextBox.Text, out var alarmtime))
            {
                MessageBox.Show("Неверно задано время таймера", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TimerTime.Enabled = true;
            time = DateTime.Now;
            time = time.Add(alarmtime.TimeOfDay);
            DisplayLabel.Text = (time - DateTime.Now.TimeOfDay).ToString("HH:mm:ss");
        }

        private void TimerTime_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now<time)
            {
                DisplayLabel.Text = (time - DateTime.Now.TimeOfDay).ToString("HH:mm:ss");
            }
            else
            {
                TimerTime.Stop();
                SystemSounds.Beep.Play();
                MessageBox.Show("Время вышло", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (TimerTime.Enabled)
            {
                pauseTime = DateTime.Now;
                TimerTime.Stop();
                return;
            }
            time = time.Add(DateTime.Now.Subtract(pauseTime));
            TimerTime.Start();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            time = new DateTime();
            DisplayLabel.Text = time.ToString("HH:mm:ss");
            TimerTime.Stop();
        }
    }
}
