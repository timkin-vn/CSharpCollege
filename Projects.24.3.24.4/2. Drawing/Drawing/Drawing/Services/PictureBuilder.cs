using Drawing.Models;
using Drawing.ViewServices;
using System;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 20,
            Height = 15,
        };

        public void DrawPicture(Painter painter)
        {
            var thickBlackPen = new Pen(Color.Black, 3);
            var thinBrownPen = new Pen(Color.SaddleBrown, 2);
            var redPen = new Pen(Color.Red, 2);
            var yellowPen = new Pen(Color.Gold, 1);

            var skyBrush = Brushes.LightCyan;
            var sunBrush = Brushes.Yellow;
            var houseBrush = Brushes.BurlyWood;
            var roofBrush = Brushes.IndianRed;
            var windowBrush = Brushes.LightSkyBlue;
            var fenceBrush = Brushes.Chocolate;

            RectangleModel rect;

            rect = new RectangleModel { X = -3, Y = 10, Width = 3, Height = 3 };
            painter.DrawEllipse(yellowPen, sunBrush, rect);

            rect = new RectangleModel { X = -4, Y = 9, Width = 5, Height = 5 };
            painter.DrawPie(yellowPen, null, rect, 0, 45);
            painter.DrawPie(yellowPen, null, rect, 90, 45);

            rect = new RectangleModel { X = 2, Y = 1, Width = 8, Height = 6 };
            painter.DrawRectangle(thickBlackPen, houseBrush, rect);

            rect = new RectangleModel { X = 8, Y = 7, Width = 1, Height = 3 };
            painter.DrawRectangle(thickBlackPen, Brushes.Gray, rect);

            rect = new RectangleModel { X = 4, Y = 3, Width = 3, Height = 3 };
            painter.DrawRectangle(thickBlackPen, windowBrush, rect);

            var roofPoints = new[]
            {
                new PointModel { X = 1, Y = 7 },
                new PointModel { X = 11, Y = 7 },
                new PointModel { X = 6, Y = 11 }
            };
            painter.DrawPolygon(redPen, roofBrush, roofPoints);

            rect = new RectangleModel { X = 11, Y = 1, Width = 0.5, Height = 3 };
            painter.DrawRectangle(thinBrownPen, fenceBrush, rect);

            rect = new RectangleModel { X = 12, Y = 1, Width = 0.5, Height = 3 };
            painter.DrawRectangle(thinBrownPen, fenceBrush, rect);

            rect = new RectangleModel { X = 13, Y = 1, Width = 0.5, Height = 3 };
            painter.DrawRectangle(thinBrownPen, fenceBrush, rect);
        }
    }
}