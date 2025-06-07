using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Timer : Form
    {
        private Stopwatch stopwatch;
        private bool isRunning;
        public Timer()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            isRunning = false;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                stopwatch.Start();
                await UpdateTime();
            }
        }

        private async Task UpdateTime()
        {
            while (isRunning)
            {
                TimeSpan ts = stopwatch.Elapsed;
                label1.Text = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours, ts.Minutes, ts.Seconds);
                await Task.Delay(1000); // Задержка на 1 секунду
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                stopwatch.Stop();
            }
        }
    }
}
