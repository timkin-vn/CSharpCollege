using AlarmClock.Forms;
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

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private AlarmSettings _settings = new AlarmSettings();

        private Timer stopwatchTimer;
        private TimeSpan stopwatchTime;
        private bool isStopwatchRunning = false;

        public ClockForm()
        {
            InitializeComponent();
            stopwatchTimer = new Timer();
            stopwatchTimer.Interval = 1000;
            stopwatchTimer.Tick += StopwatchTimer_Tick;
            stopwatchTime = TimeSpan.Zero;

            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            btnReset.Click += BtnReset_Click;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            DisplayLabel.Text = DateTime.Now.ToLongTimeString();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.Settings = _settings;
            form.ShowDialog();
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1));
            lblStopwatchTime.Text = stopwatchTime.ToString(@"hh\:mm\:ss");
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!isStopwatchRunning)
            {
                stopwatchTimer.Start();
                isStopwatchRunning = true;
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (isStopwatchRunning)
            {
                stopwatchTimer.Stop();
                isStopwatchRunning = false;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            stopwatchTimer.Stop();
            stopwatchTime = TimeSpan.Zero;
            lblStopwatchTime.Text = "00:00:00";
            isStopwatchRunning = false;
        }

        private void lblStopwatchTime_Click(object sender, EventArgs e)
        {

        }
    }
}