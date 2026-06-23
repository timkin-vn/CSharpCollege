using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class StopwatchForm : Form
    {

        private TimeSpan elapsedTime;
        private DateTime startTime;
        private bool isRunning = false;
        private int lapCounter = 1;


        public StopwatchForm()
        {
            InitializeComponent();
            StopwatchTimer.Interval = 1000; // 1 секунда
            StopwatchTimer.Tick += StopwatchTimer_Tick;
            elapsedTime = TimeSpan.Zero;
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime = DateTime.Now - startTime;
            StopwatchText.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }


        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                startTime = DateTime.Now - elapsedTime;
                StopwatchTimer.Start();
                isRunning = true;
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                StopwatchTimer.Stop();
                isRunning = false;
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            StopwatchTimer.Stop();
            elapsedTime = TimeSpan.Zero;
            StopwatchText.Text = "00:00:00";
            isRunning = false;
            lapCounter = 1;
            StopwatchListBox.Items.Clear();
        }

        private void ButtonLap_Click(object sender, EventArgs e)
        {
            string lapTime = elapsedTime.ToString(@"hh\:mm\:ss");
            StopwatchListBox.Items.Add($"Интервал {lapCounter++}: {lapTime}");
        }
    }
}
