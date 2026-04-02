using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BoatDrawing
{
    public partial class BoatForm : Form
    {
        public BoatForm()
        {
            Text = "Кораблик";
            BackColor = Color.FromArgb(135, 206, 235);
            ClientSize = new Size(900, 600);
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.ResetTransform();

            float designW = 900f;
            float designH = 600f;

            float sx = (float)ClientSize.Width / designW;
            float sy = (float)ClientSize.Height / designH;
            float s = Math.Min(sx, sy);

            float ox = (ClientSize.Width - designW * s) / 2f;
            float oy = (ClientSize.Height - designH * s) / 2f;

            Matrix m = new Matrix();
            m.Scale(s, s);
            m.Translate(ox, oy, MatrixOrder.Append);
            g.Transform = m;

            Pen outline = new Pen(Color.FromArgb(40, 40, 40), 2);

            Brush seaBrush = new SolidBrush(Color.FromArgb(30, 100, 180));
            Brush sunBrush = new SolidBrush(Color.FromArgb(255, 220, 50));
            Brush whiteBrush = new SolidBrush(Color.White);
            Brush redBrush = new SolidBrush(Color.FromArgb(210, 50, 50));
            Brush woodBrush = new SolidBrush(Color.FromArgb(180, 110, 50));
            Brush darkWoodBrush = new SolidBrush(Color.FromArgb(120, 60, 20));


            Rectangle sun = new Rectangle(750, 60, 90, 90);
            g.FillEllipse(sunBrush, sun);
            g.DrawEllipse(outline, sun);

            Rectangle sea = new Rectangle(0, 450, 900, 200);
            g.FillRectangle(seaBrush, sea);
            g.DrawLine(outline, 0, 450, 900, 450);

            int boatX = 450;
            int boatY = 400;


            Rectangle mast = new Rectangle(boatX - 5, boatY - 220, 10, 220);
            g.FillRectangle(woodBrush, mast);
            g.DrawRectangle(outline, mast);



            Point[] leftSail = {
                new Point(boatX - 10, boatY - 190),
                new Point(boatX - 130, boatY - 30), 
                new Point(boatX - 10, boatY - 30)   
            };
            g.FillPolygon(whiteBrush, leftSail);
            g.DrawPolygon(outline, leftSail);

            Point[] rightSail = {
                new Point(boatX + 10, boatY - 160),
                new Point(boatX + 110, boatY - 30), 
                new Point(boatX + 10, boatY - 30)   
            };
            g.FillPolygon(whiteBrush, rightSail);
            g.DrawPolygon(outline, rightSail);

            Point[] flag = {
                new Point(boatX + 5, boatY - 220),
                new Point(boatX + 50, boatY - 200),
                new Point(boatX + 5, boatY - 180)
            };
            g.FillPolygon(redBrush, flag);
            g.DrawPolygon(outline, flag);

            Point[] hull = {
                new Point(boatX - 150, boatY),     
                new Point(boatX + 140, boatY),      
                new Point(boatX + 90, boatY + 60),  
                new Point(boatX - 100, boatY + 60)  
            };
            g.FillPolygon(darkWoodBrush, hull);
            g.DrawPolygon(outline, hull);

            outline.Dispose();
            seaBrush.Dispose();
            sunBrush.Dispose();
            whiteBrush.Dispose();
            redBrush.Dispose();
            woodBrush.Dispose();
            darkWoodBrush.Dispose();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BoatForm());
        }
    }
}