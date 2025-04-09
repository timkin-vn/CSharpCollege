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
    public partial class X : Form
    {
        private int seconds = 0;
        public X()
        {
            InitializeComponent();
            timerSec.Interval = 100;
            UpdateTimerDisplay();
        }
        private void UpdateTimerDisplay()
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            laiblSec.Text = time.ToString(@"hh\:mm\:ss");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timerSec.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerSec.Stop();
            UpdateTimerDisplay();
        }

        private void laiblSec_Click(object sender, EventArgs e)
        {

        }

        private void timerSec_Tick(object sender, EventArgs e)
        {
            seconds++;
            UpdateTimerDisplay();
        }

        private void button2_Click(object sender, EventArgs e)// Кнопка круг
        {
            

            TimeSpan currentTime = TimeSpan.FromSeconds(seconds);
            string lapTime = currentTime.ToString(@"hh\:mm\:ss");

            // Добавляем круг в ListBox
            listKrugi.Items.Add($"Круг {listKrugi.Items.Count + 1}: {lapTime}");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void listKrugi_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        private void buttonSbros_Click(object sender, EventArgs e)
        {
            
            seconds = 0;
            listKrugi.Items.Clear();
            UpdateTimerDisplay();
        }
    }
}
