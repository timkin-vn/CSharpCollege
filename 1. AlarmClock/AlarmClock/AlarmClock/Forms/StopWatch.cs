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
    public partial class StopWatch : Form
    {
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private DateTime lastTick;
        private bool isRunning = false;
        private List<TimeSpan> intervals = new List<TimeSpan>();
        private TimeSpan lastIntervalTime = TimeSpan.Zero;

        public StopWatch()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            elapsedTime += now - lastTick;
            lastTick = now;
            StopWatchLabel.Text = elapsedTime.ToString(@"hh\:mm\:ss\.fff");
        }

        private void StopWatchStartPauseButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                timer1.Stop();
                isRunning = false;
                StopWatchStartPauseButton.Text = "Старт";
            }
            else
            {
                lastTick = DateTime.Now;
                timer1.Start();
                isRunning = true;
                StopWatchStartPauseButton.Text = "Пауза";
            }
        }

        private void StopWatchResetButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            elapsedTime = TimeSpan.Zero;
            lastIntervalTime = TimeSpan.Zero;
            intervals.Clear();
            StopWatchIntervalListBox.Items.Clear();
            StopWatchLabel.Text = "00:00:00.000";
            isRunning = false;
        }

        private void StopWatchIntervalButton_Click(object sender, EventArgs e)
        {
            var interval = elapsedTime - lastIntervalTime;
            lastIntervalTime = elapsedTime;
            intervals.Add(interval);
            StopWatchIntervalListBox.Items.Add($"Интервал {intervals.Count}: {interval.ToString(@"hh\:mm\:ss\.fff")}");
        }
    }
}
