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

namespace Picture
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

            var rect = new Rectangle
            {
                X = 200,
                Y = 150,
                Width = 150,
                Height = 100,
            };

            //var brush = new SolidBrush(Color.FromArgb(255, 204, 17));
            var brush = new LinearGradientBrush(rect, Color.Yellow, Color.Red, LinearGradientMode.ForwardDiagonal);
            //g.FillRectangle(brush, rect);

            var pen = new Pen(Color.FromArgb(0, 68, 153), 3);
            //g.DrawRectangle(pen, rect);

            var rect2 = ClientRectangle;
            rect2.Inflate(-1, -1);
            g.DrawRectangle(Pens.Black, rect2);
            g.DrawLine(Pens.Black, rect2.Location, rect2.Location + rect2.Size);
            g.DrawLine(Pens.Black, rect2.Left, rect2.Bottom, rect2.Right, rect2.Top);

            var polygonPoints = new[]
            {
                new Point { X = 150, Y = 100, },
                new Point { X = 450, Y = 100, },
                new Point { X = 500, Y = 200, },
                new Point { X = 100, Y = 200, },
            };

            var brush2 = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Blue, Color.Cyan);
            g.FillPolygon(brush2, polygonPoints);

            var pen2 = new Pen(Color.FromArgb(119, 17, 255), 3);
            g.DrawPolygon(pen2, polygonPoints);

            var rect3 = new Rectangle { X = 100, Y = 200, Width = 400, Height = 300, };
            brush = new LinearGradientBrush(rect3, Color.Yellow, Color.Red, LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(brush, rect3);
            g.DrawRectangle(pen, rect3);
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
