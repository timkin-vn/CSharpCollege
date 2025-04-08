using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarm_clock_C_.Forms
{
    public partial class TimerForm : Form
    {
        private int timeInSecond;
        public TimerForm()
        {
            InitializeComponent();
            timerT = new Timer();
            timerT.Interval = 1000;
            timerT.Tick += timerT_Tick;
        }


        private void timerT_Tick(object sender, EventArgs e)
        {
            if (timeInSecond > 0)
            {
                timeInSecond--;
                UpdateTimerLabel();
            }
            else
            {
                timerT.Stop();
                TimerTime.Text = "00:00:00";
                MessageBox.Show("Время истекло!");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TimerTextBox.Text, out int minutes))
            {
                timeInSecond = minutes * 60;
                timerT.Start();
                UpdateTimerLabel();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное время!");
            }
        }

        private void UpdateTimerLabel()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSecond);
            TimerTime.Text = timeSpan.ToString("hh\\:mm\\:ss");
        }

        private void StopTimer_Click(object sender, EventArgs e)
        {
            timerT.Stop();
            TimerTime.Text = "00:00:00";
            TimerTextBox.Clear();
        }
    }
}
