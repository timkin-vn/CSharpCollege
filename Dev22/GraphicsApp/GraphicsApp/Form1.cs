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
                g.FillRectangle(seaBrush, 0, this.ClientSize.Height*5/6 , this.ClientSize.Width, this.ClientSize.Height/6);
            }

            var dx = ClientRectangle.Width / 6;
            var dy = ClientRectangle.Height / 6;
            using (Brush sandBrush = new SolidBrush(Color.SandyBrown))
            {
                g.FillRectangle(sandBrush, 0, dy*4, this.ClientSize.Width , dy);
            }

            
            using (Brush houseBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(houseBrush, (dx*3)/2, dy*2+(dy/2), dx * 2, dy * 2);
            }

            
            Point[] roofPoints = {
            new Point((dx*3)/2, dy*2+(dy/2)),
            new Point(((dx*3)/2+dx * 2)-dx, dy*2+(dy/2)-dy),
            new Point((dx*3)/2+dx * 2, dy*2+(dy/2))
        };
            using (Brush roofBrush = new SolidBrush(Color.Brown))
            {
                g.FillPolygon(roofBrush, roofPoints);
            }

            
            using (Brush doorBrush = new SolidBrush(Color.Brown))
            {
                g.FillRectangle(doorBrush, (dx * 2)+dx/4 , dy * 3+(dy/2), (dx/2), dy );
            }
            using (Brush upWindow = new SolidBrush(Color.White))
            {
                g.FillEllipse(upWindow, (dx * 2)+dx/3 , dy * 2-dy/4 , dx / 3, dy / 3);
            }

            using (Brush windowBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(windowBrush, (dx*2)-(dx/4) , dy * 3-dy/4 , dx / 2, dy / 2);
                g.FillRectangle(windowBrush, (dx * 3) - (dx / 4), dy * 3 - dy / 4, dx / 2, dy / 2);
            }

            
            using (Brush sunBrush = new SolidBrush(Color.Yellow))
            {
                g.FillEllipse(sunBrush, dx/2, dy/2, dy , dy );
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
            
        }

    }
}
