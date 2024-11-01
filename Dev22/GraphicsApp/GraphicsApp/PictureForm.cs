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
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            //var brush = new SolidBrush(Color.FromArgb(255, 204, 17));
            //var brush = new LinearGradientBrush(
            //    new Point { X = 200, Y = 50, },
            //    new Point { X = 400, Y = 200, },
            //    Color.FromArgb(255, 204, 17),
            //    Color.FromArgb(0, 68, 153));
            //g.FillRectangle(brush, 200, 50, 200, 150);

            //var pen = new Pen(Color.FromArgb(255, 187, 102), 3)
            //{
            //    DashPattern = new[] { 8f, 2f, 2f, 2f, 2f, 2f, },
            //};
            //g.DrawRectangle(pen, 200, 50, 200, 150);

            //var xMin = 1;
            //var yMin = 1;
            //var xMax = ClientRectangle.Width - 2;
            //var yMax = ClientRectangle.Height - 2;
            //var width = xMax - xMin;
            //var height = yMax - yMin;
            //g.DrawRectangle(Pens.Blue, xMin, yMin, width, height);
            //g.DrawLine(Pens.Blue, xMin, yMin, xMax, yMax);
            //g.DrawLine(Pens.Blue, xMin, yMax, xMax, yMin);
            var dx = ClientRectangle.Width / 6;
            var dy = ClientRectangle.Height / 6;

            var pen = new Pen(Color.Blue, 3);
            g.DrawRectangle(pen, dx * 2, dy * 3, dx * 2, dy * 2);
            g.DrawLines(pen, new[]
                {
                    new Point { X = dx * 2, Y = dy * 3 },
                    new Point { X = dx * 3, Y = dy, },
                    new Point { X = dx * 4, Y = dy * 3 }
                });
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
