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

        // 1. Первичное отображение на экране
        // 2. Восстановление после сворачивания
        // 3. Увеличение размера
        // 4. Принудительная перерисовка
        private void PictureForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //var rect = new Rectangle
            //{
            //    X = 250,
            //    Y = 100,
            //    Width = 150,
            //    Height = 100,
            //};

            ////var brush = new SolidBrush(Color.FromArgb(255, 204, 17));
            //var brush = new LinearGradientBrush(rect, Color.Yellow, Color.Red, LinearGradientMode.ForwardDiagonal);
            //g.FillRectangle(brush, rect);

            //var pen = new Pen(Color.FromArgb(0, 68, 153), 3);
            //g.DrawRectangle(pen, rect);

            //var polygonPoints = new[]
            //{
            //    new Point { X = 150, Y = 100, },
            //    new Point { X = 450, Y = 100, },
            //    new Point { X = 500, Y = 200, },
            //    new Point { X = 100, Y = 200, },
            //};

            //var brush2 = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Blue, Color.Cyan);
            //g.FillPolygon(brush2, polygonPoints);

            //var pen2 = new Pen(Color.FromArgb(119, 17, 255), 3);
            //g.DrawPolygon(pen2, polygonPoints);

            //var rect2 = ClientRectangle;
            //rect2.Inflate(-1, -1);
            //g.DrawRectangle(Pens.Black, rect2);
            //g.DrawLine(Pens.Black, rect2.Location, rect2.Location + rect2.Size);
            //g.DrawLine(Pens.Black, rect2.Left, rect2.Bottom, rect2.Right, rect2.Top);

            var bounds = ClientRectangle;
            var pen = new Pen(Color.DarkGreen, 3);
            var brush = Brushes.Yellow;

            var headRect = new Rectangle
            {
                X = bounds.Width * 2 / 5,
                Y = bounds.Height / 5,
                Width = bounds.Width / 5,
                Height = bounds.Height / 5,
            };

            g.FillEllipse(brush, headRect);
            g.DrawEllipse(pen, headRect);

            var bodyRect = new Rectangle
            {
                X = bounds.Width * 2 / 5,
                Y = bounds.Height * 2 / 5,
                Width = bounds.Width / 5,
                Height = bounds.Height / 5,
            };

            g.FillRectangle(brush, bodyRect);
            g.DrawRectangle(pen, bodyRect);

            g.DrawLine(pen, bounds.Width * 2 / 5, bounds.Height * 2 / 5, bounds.Width / 5, bounds.Height * 3 / 5);
            g.DrawLine(pen, bounds.Width * 3 / 5, bounds.Height * 2 / 5, bounds.Width * 4 / 5, bounds.Height * 3 / 5);

            g.DrawLine(pen, bounds.Width * 2 / 5, bounds.Height * 3 / 5, bounds.Width * 2 / 5, bounds.Height * 4 / 5);
            g.DrawLine(pen, bounds.Width * 3 / 5, bounds.Height * 3 / 5, bounds.Width * 3 / 5, bounds.Height * 4 / 5);
        }

        private void PictureForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
