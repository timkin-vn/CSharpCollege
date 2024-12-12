using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class TimerForm : Form
    {
        private int timerInterval = 1000; // интервал таймера в миллисекундах
        private int currentMinets = 1; 
        private int currentSecond = 59; // текущее время в секундах
        public TimerForm()
        {
            InitializeComponent();
            this.Text = "Таймер";
            

            this.TimerTime.Interval = timerInterval;
            this.TimerTime.Tick += TimerTick;
            this.TimerTime.Enabled = true;
        }

        private void UpdateLabel()
        {
            TimerLabel.Text = $"Время: {currentMinets:D2}:{currentSecond :D2}";
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (--currentSecond <= 0 && --currentMinets<=0)
            {
                TimerTime.Stop();
                MessageBox.Show("Время истекло!");
            }
            else
            {
                UpdateLabel();
            }
        }

       

        private void StopButton_Click(object sender, EventArgs e)
        {
            TimerTime.Stop();
            UpdateLabel();
        }

        private void Addminet_Click(object sender, EventArgs e)
        {
            if (currentSecond < 60)
            {
                currentMinets++;
                UpdateLabel();
            }
        }
       

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            TimerTime.Start();
            UpdateLabel();
        }
    }


}
