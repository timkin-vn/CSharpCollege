using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class HouseBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -2,
            Width = 25,
            Height = 18,
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 2);

            // ДОМ 
            var houseBrush = Brushes.BurlyWood;
            var rect = new RectangleModel { X = 0, Y = 0, Width = 8, Height = 6 };
            painter.DrawRectangle(houseBrush, mainPen, rect);

            // КРЫША
            var roofBrush = Brushes.DarkRed;
            var roofPoints = new[]
            {
                new PointModel { X = -1, Y = 6 },
                new PointModel { X = 4, Y = 10 },
                new PointModel { X = 9, Y = 6 },
            };
            painter.DrawPolygon(roofBrush, mainPen, roofPoints);

            // ДВЕРЬ
            var doorBrush = Brushes.SaddleBrown;
            rect = new RectangleModel { X = 3, Y = 0, Width = 2, Height = 3 };
            painter.DrawRectangle(doorBrush, mainPen, rect);

            // ОКНО
            var windowBrush = Brushes.LightSkyBlue;
            rect = new RectangleModel { X = 1, Y = 3, Width = 2, Height = 2 };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            // ТРУБА 
            var pipeBrush = Brushes.Gray;
            rect = new RectangleModel { X = 6, Y = 8, Width = 1.5, Height = 3 };
            painter.DrawRectangle(pipeBrush, mainPen, rect);

            // ДЫМ 
            var smokeBrush = Brushes.LightGray;

            rect = new RectangleModel { X = 6.5, Y = 11, Width = 1, Height = 1 };
            painter.DrawEllipse(smokeBrush, mainPen, rect);

            rect.Y = 12;
            painter.DrawEllipse(smokeBrush, mainPen, rect);

            rect.Y = 13;
            painter.DrawEllipse(smokeBrush, mainPen, rect);

            // ЗАБОР 
            var fenceBrush = Brushes.Peru;

            for (int i = -2; i < 12; i++)
            {
                rect = new RectangleModel { X = i, Y = 0, Width = 0.3, Height = 2 };
                painter.DrawRectangle(fenceBrush, mainPen, rect);
            }

            // Перекладины забора
            rect = new RectangleModel { X = -2, Y = 1.5, Width = 14, Height = 0.2 };
            painter.DrawRectangle(fenceBrush, mainPen, rect);

            rect = new RectangleModel { X = -2, Y = 0.5, Width = 14, Height = 0.2 };
            painter.DrawRectangle(fenceBrush, mainPen, rect);

            // СОЛНЦЕ
            var sunBrush = Brushes.Gold;

            // круг солнца
            rect = new RectangleModel { X = 15, Y = 12, Width = 3, Height = 3 };
            painter.DrawEllipse(sunBrush, mainPen, rect);

            // лучи солнца (линии)
            var centerX = 16.5;
            var centerY = 13.5;

            for (int i = 0; i < 360; i += 30)
            {
                double angle = i * System.Math.PI / 180;

                var x1 = centerX + 1.5 * System.Math.Cos(angle);
                var y1 = centerY + 1.5 * System.Math.Sin(angle);

                var x2 = centerX + 2.5 * System.Math.Cos(angle);
                var y2 = centerY + 2.5 * System.Math.Sin(angle);

                painter.DrawLine(mainPen,
                    new PointModel { X = x1, Y = y1 },
                    new PointModel { X = x2, Y = y2 });
            }
        }
    }
}