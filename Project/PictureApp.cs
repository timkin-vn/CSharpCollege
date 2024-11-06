using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Risunok
{
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Azure);

            // Head (Circle)
            using (LinearGradientBrush headBrush = new LinearGradientBrush(
                new Rectangle(200, 50, 100, 100),
                Color.FromArgb(255, 224, 189), 
                Color.FromArgb(255, 153, 102), 
                LinearGradientMode.Vertical))
            {
                g.FillEllipse(headBrush, 200, 50, 100, 100); 
            }

            // Body (Rectangle)
            using (SolidBrush bodyBrush = new SolidBrush(Color.FromArgb(0, 102, 204))) 
            {
                g.FillRectangle(bodyBrush, 225, 150, 50, 100); 
            }

            // Arms (Rectangles)
            using (SolidBrush armBrush = new SolidBrush(Color.FromArgb(255, 224, 189))) 
            {
                g.FillRectangle(armBrush, 175, 150, 50, 20); 
                g.FillRectangle(armBrush, 275, 150, 50, 20); 
            }

            // Legs (Rectangles)
            using (SolidBrush legBrush = new SolidBrush(Color.FromArgb(0, 51, 102))) 
            {
                g.FillRectangle(legBrush, 225, 250, 20, 100); 
                g.FillRectangle(legBrush, 255, 250, 20, 100); 
            }

            // Feet (Ellipses)
            using (SolidBrush footBrush = new SolidBrush(Color.FromArgb(255, 224, 189))) 
            {
                g.FillEllipse(footBrush, 225, 350, 20, 10); 
                g.FillEllipse(footBrush, 255, 350, 20, 10); 
            }

            // Eyes (Circles)
            g.FillEllipse(Brushes.White, 230, 80, 20, 20); 
            g.FillEllipse(Brushes.White, 250, 80, 20, 20); 
            g.FillEllipse(Brushes.Black, 235, 85, 10, 10); 
            g.FillEllipse(Brushes.Black, 255, 85, 10, 10); 

            // Mouth (Arc)
            using (Pen mouthPen = new Pen(Color.Black, 2))
            {
                g.DrawArc(mouthPen, 230, 100, 40, 20, 0, 180); 
            }

            // Hair (Polygon)
            using (SolidBrush hairBrush = new SolidBrush(Color.FromArgb(139, 69, 19)))
            {
                Point[] hairPoints = {
                    new Point(200, 50),
                    new Point(250, 10),
                    new Point(300, 50)
                };
                g.FillPolygon(hairBrush, hairPoints); 
            }

            // Outline for the body
            using (Pen outlinePen = new Pen(Color.Black, 2))
            {
                g.DrawRectangle(outlinePen, 225, 150, 50, 100);
                g.DrawRectangle(outlinePen, 175, 150, 50, 20);
                g.DrawRectangle(outlinePen, 275, 150, 50, 20);
                g.DrawRectangle(outlinePen, 225, 250, 20, 100);
                g.DrawRectangle(outlinePen, 255, 250, 20, 100);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}