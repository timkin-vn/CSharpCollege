using System;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class StopwatchForm : Form
    {
        private TimeSpan elapsedTime;
        private DateTime startTime;
        private bool isRunning;

        public StopwatchForm()
        {
            InitializeComponent();
            elapsedTime = TimeSpan.Zero;
            isRunning = false;
        }

        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            if (isRunning)
            {
                elapsedTime = DateTime.Now - startTime;
                DisplayLabel.Text = elapsedTime.ToString(@"mm\:ss\.ff");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                startTime = DateTime.Now - elapsedTime;
                StopwatchTimer.Start();
                isRunning = true;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            StopwatchTimer.Stop();
            elapsedTime = TimeSpan.Zero;
            DisplayLabel.Text = "00:00:00";
            isRunning = false;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                StopwatchTimer.Stop();
                isRunning = false;
            }
            else
            {
                startTime = DateTime.Now - elapsedTime;
                StopwatchTimer.Start();
                isRunning = true;
            }
        }
    }
}