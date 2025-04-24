using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace AlarmClock.Forms
{
    public partial class Timer_test : Form
    {

        private int totalSeconds;
        public Timer_test()
        {
            InitializeComponent();

            numericUpDown1.Maximum = 59;
            numericUpDown1.Minimum = 0;
            numericUpDown1.TabStop = false;

            numericUpDown2.Maximum = 59;
            numericUpDown2.Minimum = 0;
            numericUpDown2.TabStop = false;

        }

        private void startTime_Click(object sender, EventArgs e)
        {   
            int min = (int)numericUpDown1.Value;
            int sec = (int)numericUpDown2.Value;

            totalSeconds = min * 60 + sec;

            Timer_m.Text = $"{min:D2}:{sec:D2}";
            Timer2.Start();
        }

        private void Timer_m_Click(object sender, EventArgs e)
        {
            
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (totalSeconds > 0)
            {
                totalSeconds--;

                int minutes = totalSeconds / 60;
                int seconds = totalSeconds % 60;
                Timer_m.Text = $"{minutes:D2}:{seconds:D2}";
            }
            else
            {
                Timer2.Stop();
                MessageBox.Show("Время вышло!");
                Timer_m.Text = "00:00";
            }
        }
    }
}
