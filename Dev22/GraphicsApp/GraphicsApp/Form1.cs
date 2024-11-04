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

namespace GraphicsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            
            g.Clear(Color.Cyan);

            
            using (Brush seaBrush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(seaBrush, 0, 400, this.ClientSize.Width, 200);
            }

            
            using (Brush sandBrush = new SolidBrush(Color.SandyBrown))
            {
                g.FillRectangle(sandBrush, 0, 350, this.ClientSize.Width, 50);
            }

            
            using (Brush houseBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(houseBrush, 300, 250, 200, 150);
            }

            
            Point[] roofPoints = {
            new Point(250, 250),
            new Point(400, 150),
            new Point(550, 250)
        };
            using (Brush roofBrush = new SolidBrush(Color.Brown))
            {
                g.FillPolygon(roofBrush, roofPoints);
            }

            
            using (Brush doorBrush = new SolidBrush(Color.Brown))
            {
                g.FillRectangle(doorBrush, 375, 325, 50, 75);
            }
            using (Brush upWindow = new SolidBrush(Color.White)) {
                g.FillEllipse(upWindow, 370, 175, 65, 65);
            }
            
            using (Brush windowBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(windowBrush, 325, 275, 50, 50);
                g.FillRectangle(windowBrush, 425, 275, 50, 50);
            }

            
            using (Brush sunBrush = new SolidBrush(Color.Yellow))
            {
                g.FillEllipse(sunBrush, 50, 50, 100, 100);
            }
        }

    }
}
