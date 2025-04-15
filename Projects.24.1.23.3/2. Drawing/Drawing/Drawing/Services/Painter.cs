using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class Painter
    {
        private Scaler _scaler = new Scaler();

        public void Initialize(Rectangle targetRectangle)
        {
            _scaler.Initialize(targetRectangle, new RectangleModel { X = -2, Width = 5, Y = -1, Height = 5, });
        }

        public void Paint(Graphics g)
        {
            var pen = new Pen(Color.DarkGreen, 3);
            var brush = Brushes.LightGoldenrodYellow;

            var headRect = _scaler.Scale(new RectangleModel { X = 0, Y = 2, Width = 1, Height = 1, });

            g.FillEllipse(brush, headRect);
            g.DrawEllipse(pen, headRect);

            var bodyRect = _scaler.Scale(new RectangleModel { X = 0, Y = 1, Width = 1, Height = 1, });

            g.FillRectangle(brush, bodyRect);
            g.DrawRectangle(pen, bodyRect);

            var point1 = _scaler.Scale(new PointModel { X = 0, Y = 2, });
            var point2 = _scaler.Scale(new PointModel { X = -1, Y = 1, });

            g.DrawLine(pen, point1, point2);
        }
    }
}
