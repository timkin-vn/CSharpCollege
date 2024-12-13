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
        private const int BaseWidth = 800;
        private const int BaseHeight = 600;

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

            // Calculate scaling factor
            float scaleX = (float)ClientRectangle.Width / BaseWidth;
            float scaleY = (float)ClientRectangle.Height / BaseHeight;
            float scale = Math.Min(scaleX, scaleY); // Maintain aspect ratio

            // Calculate dimensions based on scaling
            var dx = (int)(BaseWidth / 6 * scale);
            var dy = (int)(BaseHeight / 6 * scale);

            using (Brush sandBrush = new SolidBrush(Color.SandyBrown))
            {
                g.FillRectangle(sandBrush, 0, dy * 4, this.ClientSize.Width, dy * 2);
            }

            using (Brush seaBrush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(seaBrush, 0, dy*5, this.ClientSize.Width,dy);
            }

            using (Brush houseBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(houseBrush, (dx * 3) / 2, dy * 2 + (dy / 2), dx * 2, dy * 2);
            }

            Point[] roofPoints = {
            new Point((dx * 3) / 2, dy * 2 + (dy / 2)),
            new Point(((dx * 3) / 2 + dx * 2) - dx, dy * 2 + (dy / 2) - dy),
            new Point((dx * 3) / 2 + dx * 2, dy * 2 + (dy / 2))
        };
            using (Brush roofBrush = new SolidBrush(Color.Brown))
            {
                g.FillPolygon(roofBrush, roofPoints);
            }

            using (Brush doorBrush = new SolidBrush(Color.Brown))
            {
                g.FillRectangle(doorBrush, (dx * 2) + dx / 4, dy * 3 + (dy / 2), (dx / 2), dy);
            }
            using (Brush upWindow = new SolidBrush(Color.White))
            {
                g.FillEllipse(upWindow, (dx * 2) + dx / 3, dy * 2 - dy / 4, dx / 3, dy / 3);
            }

            using (Brush windowBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(windowBrush, (dx * 2) - (dx / 4), dy * 3 - dy / 4, dx / 2, dy / 2);
                g.FillRectangle(windowBrush, (dx * 3) - (dx / 4), dy * 3 - dy / 4, dx / 2, dy / 2);
            }

            using (Brush sunBrush = new SolidBrush(Color.Yellow))
            {
                g.FillEllipse(sunBrush, dx / 2, dy / 2, dx, dy);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }

}
