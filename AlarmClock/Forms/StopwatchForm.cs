using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock.Forms
{
    public partial class StopwatchForm : Form
    {
        private System.Diagnostics.Stopwatch _stopwatch;
        private Timer _updateTimer;
        private int _lapCounter = 1;

        public StopwatchForm()
        {
            InitializeComponent();
            InitializeStopwatch();
        }

        private void InitializeStopwatch()
        {
            _stopwatch = new System.Diagnostics.Stopwatch();

            _updateTimer = new Timer();
            _updateTimer.Interval = 10; // Update every 10ms for smooth display
            _updateTimer.Tick += UpdateTimer_Tick;

            UpdateDisplay();
            UpdateButtonStates();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TimeSpan elapsed = _stopwatch.Elapsed;
            TimeLabel.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds,
                elapsed.Milliseconds / 10);
        }

        private void UpdateButtonStates()
        {
            StartButton.Enabled = !_stopwatch.IsRunning;
            PauseButton.Enabled = _stopwatch.IsRunning;
            ResetButton.Enabled = _stopwatch.IsRunning || _stopwatch.Elapsed > TimeSpan.Zero;
            LapButton.Enabled = _stopwatch.IsRunning;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _stopwatch.Start();
            _updateTimer.Start();
            UpdateButtonStates();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            _stopwatch.Stop();
            _updateTimer.Stop();
            UpdateButtonStates();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _stopwatch.Reset();
            _updateTimer.Stop();
            _lapCounter = 1;
            LapListBox.Items.Clear();
            UpdateDisplay();
            UpdateButtonStates();
        }

        private void LapButton_Click(object sender, EventArgs e)
        {
            if (_stopwatch.IsRunning)
            {
                string lapTime = TimeLabel.Text;
                LapListBox.Items.Add($"Круг {_lapCounter}: {lapTime}");
                _lapCounter++;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();
            base.OnFormClosing(e);
        }

        
    }
}