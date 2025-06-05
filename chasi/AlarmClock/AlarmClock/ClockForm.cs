// ClockForm.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmClock
{
    public partial class ClockForm : Form
    {
        private Timer timer;

        public ClockForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToLongTimeString();
            analogClockPanel.Invalidate();
        }

        private void AnalogClockPanel_Paint(object sender, PaintEventArgs e)
        {
            DateTime now = DateTime.Now;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int size = Math.Min(analogClockPanel.Width, analogClockPanel.Height);
            int center = size / 2;
            int radius = center - 10;

            g.TranslateTransform(center, center);

            Pen hourPen = new Pen(Color.Black, 4);
            Pen minutePen = new Pen(Color.Blue, 3);
            Pen secondPen = new Pen(Color.Red, 1);

            double hourAngle = (now.Hour % 12 + now.Minute / 60.0) * 30;
            double minuteAngle = (now.Minute + now.Second / 60.0) * 6;
            double secondAngle = now.Second * 6;

            DrawHand(g, hourAngle, radius * 0.5, hourPen);
            DrawHand(g, minuteAngle, radius * 0.75, minutePen);
            DrawHand(g, secondAngle, radius * 0.9, secondPen);

            g.DrawEllipse(Pens.Black, -radius, -radius, radius * 2, radius * 2);
        }

        private void DrawHand(Graphics g, double angleDegrees, double length, Pen pen)
        {
            double angleRadians = (Math.PI / 180) * (angleDegrees - 90);
            float x = (float)(length * Math.Cos(angleRadians));
            float y = (float)(length * Math.Sin(angleRadians));
            g.DrawLine(pen, 0, 0, x, y);
        }
    }
}
