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
    public partial class secyndforma : Form
    {
        private int second = 0;
        public secyndforma()
        {
            InitializeComponent();
            sec.Interval = 100;
            UpdateTimerDisplay();
        }
        private void UpdateTimerDisplay()
        {
            TimeSpan time = TimeSpan.FromSeconds(second);
            timelabel.Text = time.ToString(@"hh\:mm\:ss");
        }
        private void startbtn_Click(object sender, EventArgs e)
        {
            sec.Start();
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            sec.Stop();
            UpdateTimerDisplay();
        }

        private void sercalbtn_Click(object sender, EventArgs e)
        {
            TimeSpan currentTime = TimeSpan.FromSeconds(second);
            string lapTime = currentTime.ToString(@"hh\:mm\:ss");

          
            listBox1.Items.Add($"Круг {listBox1.Items.Count + 1}: {lapTime}");
        
        }

        private void clerbtn_Click(object sender, EventArgs e)
        {
            second = 0;
            listBox1.Items.Clear();
            UpdateTimerDisplay();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timelabel_Click(object sender, EventArgs e)
        {

        }

        private void sec_Tick(object sender, EventArgs e)
        {
            second++;
            UpdateTimerDisplay();
        }
    }
}
