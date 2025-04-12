using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private Timer countdownTimer;
        private Timer clockRefreshTimer;
        private TimeSpan remainingTime;
        private bool isTimerRunning = false;

        public ClockForm()
        {
            InitializeComponent();

            countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            clockRefreshTimer = new Timer();
            clockRefreshTimer.Interval = 1000;
            clockRefreshTimer.Tick += (s, e) => analogClockPanel.Invalidate();
            clockRefreshTimer.Start();

            timerLabelDown.Text = "00:00:00";
        }

        private void SetTimerButton_Click(object sender, EventArgs e)
        {
            int hours = (int)numericUpDownHour.Value;
            int minutes = (int)numericUpDownMinute.Value;
            int seconds = (int)numericUpDownSecond.Value;

            remainingTime = new TimeSpan(hours, minutes, seconds);
            timerLabelDown.Text = remainingTime.ToString(@"hh\:mm\:ss");
        }

        private void StartPauseButtonDown_Click(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                countdownTimer.Stop();
            }
            else
            {
                countdownTimer.Start();
            }

            isTimerRunning = !isTimerRunning;
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                timerLabelDown.Text = remainingTime.ToString(@"hh\:mm\:ss");
            }
            else
            {
                countdownTimer.Stop();
                isTimerRunning = false;

                System.Media.SystemSounds.Exclamation.Play(); // sound
                MessageBox.Show("Timer ended!");
            }

        }

        private void AnalogClockPanel_Paint(object sender, PaintEventArgs e)
        {
            DateTime now = DateTime.Now;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int centerX = analogClockPanel.Width / 2;
            int centerY = analogClockPanel.Height / 2;
            int radius = Math.Min(centerX, centerY) - 10;

            // Quadrante
            g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

            // Lancette
            DrawHand(g, now.Hour % 12 * 30 + now.Minute / 2, radius * 0.5f, centerX, centerY, 6); // ore
            DrawHand(g, now.Minute * 6, radius * 0.7f, centerX, centerY, 4); // minuti
            DrawHand(g, now.Second * 6, radius * 0.9f, centerX, centerY, 2, Pens.Red); // secondi
        }

        private void DrawHand(Graphics g, float angleDeg, float length, int cx, int cy, int thickness, Pen pen = null)
        {
            if (pen == null)
                pen = new Pen(Color.Black, thickness);

            double angleRad = (Math.PI / 180) * (angleDeg - 90);
            float x = cx + (float)(length * Math.Cos(angleRad));
            float y = cy + (float)(length * Math.Sin(angleRad));
            g.DrawLine(pen, cx, cy, x, y);
        }
    }
}
