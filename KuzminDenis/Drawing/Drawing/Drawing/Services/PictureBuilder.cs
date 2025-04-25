using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = 0,
            Y = -0.5,
            Width = 14,
            Height = 10
        };

        public void DrawPicture(Painter painter)
        {
            var thickBlackPen = new Pen(Color.Black, 4);
            var thinBluePen = new Pen(Color.Blue, 1);
            var customPen = new Pen(Color.FromArgb(150, 75, 0), 2);

            var wallBrush = Brushes.Bisque;
            var roofBrush = new SolidBrush(Color.DarkSlateGray);
            var customBrush = new SolidBrush(Color.FromArgb(0, 100, 0));


            var houseRect = new RectangleModel { X = 3, Y = 3, Width = 8, Height = 6 };
            painter.DrawRectangle(wallBrush, thickBlackPen, houseRect);


            var roofPoints = new[]
            {
                new PointModel { X = 2, Y = 3 },
                new PointModel { X = 7, Y = 0 },
                new PointModel { X = 12, Y = 3 }
            };
            painter.DrawPolygon(roofBrush, customPen, roofPoints);


            var doorRect = new RectangleModel { X = 6, Y = 6, Width = 2, Height = 3 };
            painter.DrawRectangle(Brushes.SaddleBrown, thinBluePen, doorRect);


            var windowRect = new RectangleModel { X = 4, Y = 4, Width = 2, Height = 2 };
            painter.DrawEllipse(Brushes.LightSkyBlue, thinBluePen, windowRect);


            var windowRect2 = new RectangleModel { X = 8.2, Y = 4, Width = 2, Height = 2 };
            painter.DrawRectangle(Brushes.LightSkyBlue, thinBluePen, windowRect2);


            var pipePoints = new[]
            {
                new PointModel { X = 9, Y = 1 },
                new PointModel { X = 10, Y = 1 },
                new PointModel { X = 10, Y = 2 },
                new PointModel { X = 9, Y = 3 }
            };
            painter.DrawPolygon(Brushes.Brown, customPen, pipePoints);


            var bushRect1 = new RectangleModel { X = 1, Y = 7, Width = 3, Height = 2 };
            painter.DrawEllipse(customBrush, thickBlackPen, bushRect1);


            var bushRect2 = new RectangleModel { X = 10, Y = 7, Width = 3, Height = 2 };
            painter.DrawEllipse(customBrush, thickBlackPen, bushRect2);


            var pacmanRect = new RectangleModel { X = 3, Y = 2, Width = 1, Height = 1 };
            painter.DrawPie(Brushes.Gold, customPen, pacmanRect, 45, 270);


            var sunRect = new RectangleModel { X = 13, Y = 0.5, Width = 2, Height = 2 };
            painter.DrawEllipse(Brushes.Yellow, customPen, sunRect);
        }
    }
}