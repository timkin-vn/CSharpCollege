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
    public partial class StopwatchForm : Form
    {
        private TimeSpan elapsedTime;
        private bool isRunning = false;

        public StopwatchForm()
        {
            InitializeComponent();
            elapsedTime = TimeSpan.Zero;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            lblTime.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            UpdateLabel();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            isRunning = !isRunning;
            Timer.Enabled = isRunning;

            btnStartStop.Text = isRunning ? "Стоп" : "Старт";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Timer.Enabled = false;
            isRunning = false;
            elapsedTime = TimeSpan.Zero;
            UpdateLabel();
            btnStartStop.Text = "Старт";
        }
    }
}
