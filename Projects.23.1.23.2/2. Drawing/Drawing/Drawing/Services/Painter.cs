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
        public Scaler Scaler { get; set; }

        public void Initialize(Rectangle bounds)
        {
            Scaler = new Scaler();
            Scaler.TargetBounds = bounds;
            Scaler.SourceBounds = new RectangleModel
            {
                X = -2,
                Y = -1,
                Width = 5,
                Height = 5,
            };
        }

        public void Paint(Graphics g)
        {
            var pen = new Pen(Color.DarkCyan, 3);
            var brush = Brushes.LightGoldenrodYellow;

            var rect = Scaler.Scale(new RectangleModel { X = 0, Y = 2, Width = 1, Height = 1 });
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect);

            rect = Scaler.Scale(new RectangleModel { X = 0, Y = 1, Width = 1, Height = 1 });
            g.FillRectangle(brush, rect);
            g.DrawRectangle(pen, rect);
        }
    }
}
