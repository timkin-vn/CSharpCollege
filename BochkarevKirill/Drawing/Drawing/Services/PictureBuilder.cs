using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -5,
            Width = 20,
            Height = 20,
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 2);

            var houseBrush = Brushes.White;
            var rect = new RectangleModel { X = 0, Y = -2, Width = 8, Height = 6 };
            painter.DrawRectangle(houseBrush, mainPen, rect);

            var roofBrush = Brushes.Red;
            var roofPoints = new[]
            {
                new PointModel { X = -1, Y = 4 },
                new PointModel { X = 4, Y = 8 },
                new PointModel { X = 9, Y = 4 },
            };
            painter.DrawPolygon(roofBrush, mainPen, roofPoints);

            var doorBrush = Brushes.Brown;
            rect = new RectangleModel { X = 3, Y = -2, Width = 2, Height = 3 };
            painter.DrawRectangle(doorBrush, mainPen, rect);

            var doorHandle = Brushes.RosyBrown;
            rect = new RectangleModel { X = 4.4, Y = -0.8, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(doorHandle, mainPen, rect);

            var windowBrush = Brushes.SkyBlue;
            rect = new RectangleModel { X = 1, Y = 1, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            rect = new RectangleModel { X = 5.5, Y = 1, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            var chimneyBrush = Brushes.Gray;
            rect = new RectangleModel { X = 6, Y = 5, Width = 1, Height = 3 };
            painter.DrawRectangle(chimneyBrush, mainPen, rect);

            var smokeBrush = Brushes.Gray;
            painter.DrawEllipse(smokeBrush, mainPen, new RectangleModel { X = 6.5, Y = 9, Width = 0.5, Height = 1 });
            painter.DrawEllipse(smokeBrush, mainPen, new RectangleModel { X = 6.7, Y = 10, Width = 0.7, Height = 1.2 });
            painter.DrawEllipse(smokeBrush, mainPen, new RectangleModel { X = 6.9, Y = 11, Width = 0.9, Height = 1.4 });
        }
    }
}
