using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Машина
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Машина";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;
            this.ResizeRedraw = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int startX = 100;
            int startY = 250;

            Point[] bodyPoints = new Point[]
            {
                new Point(startX + 70, startY + 70),
                new Point(startX + 520, startY + 70),
                new Point(startX + 630, startY + 90),
                new Point(startX + 630, startY + 160),
                new Point(startX + 70, startY + 160)
            };
            g.FillPolygon(Brushes.DarkRed, bodyPoints);
            g.DrawPolygon(Pens.Black, bodyPoints);

            Point[] cabinPoints = new Point[]
            {
                new Point(startX + 80, startY + 70),
                new Point(startX + 150, startY + 20),
                new Point(startX + 400, startY + 20),
                new Point(startX + 520, startY + 70)
            };
            g.FillPolygon(Brushes.DarkRed, cabinPoints);

            Point[] frontGlassPoints = new Point[]
            {
                new Point(startX + 110, startY + 70),
                new Point(startX + 155, startY + 25),
                new Point(startX + 240, startY + 25),
                new Point(startX + 230, startY + 70)
            };
            g.FillPolygon(Brushes.LightSkyBlue, frontGlassPoints);
            g.DrawPolygon(Pens.Black, frontGlassPoints);

            Point[] rearGlassPoints = new Point[]
            {
                new Point(startX + 250, startY + 25),
                new Point(startX + 390, startY + 25),
                new Point(startX + 510, startY + 70),
                new Point(startX + 380, startY + 70)
            };
            g.FillPolygon(Brushes.LightSkyBlue, rearGlassPoints);
            g.DrawPolygon(Pens.Black, rearGlassPoints);

            Point[] sideGlassPoints = new Point[]
            {
                new Point(startX + 245, startY + 25),
                new Point(startX + 385, startY + 25),
                new Point(startX + 375, startY + 70),
                new Point(startX + 255, startY + 70)
            };
            g.FillPolygon(Brushes.LightSkyBlue, sideGlassPoints);
            g.DrawPolygon(Pens.Black, sideGlassPoints);

            Rectangle frontTire = new Rectangle(startX + 145, startY + 110, 100, 100);
            g.FillEllipse(Brushes.Black, frontTire);

            Rectangle rearTire = new Rectangle(startX + 455, startY + 110, 100, 100);
            g.FillEllipse(Brushes.Black, rearTire);

            Rectangle frontRim = new Rectangle(startX + 170, startY + 135, 50, 50);
            g.FillEllipse(Brushes.Silver, frontRim);
            g.DrawEllipse(Pens.Gray, frontRim);

            Rectangle rearRim = new Rectangle(startX + 480, startY + 135, 50, 50);
            g.FillEllipse(Brushes.Silver, rearRim);
            g.DrawEllipse(Pens.Gray, rearRim);

            Rectangle headLight = new Rectangle(startX + 600, startY + 95, 20, 25);
            g.FillEllipse(Brushes.Yellow, headLight);
            g.DrawEllipse(Pens.Orange, headLight);

            Rectangle tailLight = new Rectangle(startX + 85, startY + 90, 20, 30);
            g.FillRectangle(Brushes.Red, tailLight);
            g.DrawRectangle(Pens.DarkRed, tailLight);

            Rectangle doorHandle1 = new Rectangle(startX + 260, startY + 85, 30, 10);
            g.FillRectangle(Brushes.Gray, doorHandle1);

            Rectangle doorHandle2 = new Rectangle(startX + 400, startY + 85, 30, 10);
            g.FillRectangle(Brushes.Gray, doorHandle2);

            Rectangle frontBumper = new Rectangle(startX + 615, startY + 120, 15, 40);
            g.FillRectangle(Brushes.Black, frontBumper);

            Rectangle rearBumper = new Rectangle(startX + 70, startY + 120, 15, 40);
            g.FillRectangle(Brushes.Black, rearBumper);

            Rectangle shadow = new Rectangle(startX + 70, startY + 200, 563, 25);
            using (LinearGradientBrush brush = new LinearGradientBrush(shadow, Color.Black, Color.Transparent, 90F))
            {
                g.FillRectangle(brush, shadow);
            }
        }
    }
}