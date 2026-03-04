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
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Brown, 2);

            // Рисуем траву (фон)
            var grassBrush = Brushes.LightGreen;
            var ground = new RectangleModel { X = -5, Y = -1, Width = 19, Height = 6 };
            painter.DrawRectangle(grassBrush, null, ground);

            // Рисуем небо (фон)
            var skyBrush = Brushes.LightBlue;
            var sky = new RectangleModel { X = -5, Y = 5, Width = 19, Height = 6 };
            painter.DrawRectangle(skyBrush, null, sky);

            // Рисуем дом (основное здание)
            var houseBrush = Brushes.SandyBrown;
            var house = new RectangleModel { X = 0, Y = 2, Width = 8, Height = 6 };
            painter.DrawRectangle(houseBrush, mainPen, house);

            // Рисуем крышу (треугольник)
            var roofBrush = Brushes.Red;
            var roofPoints = new[]
            {
                new PointModel { X = -1, Y = 8 },
                new PointModel { X = 4, Y = 11 },
                new PointModel { X = 9, Y = 8 }
            };
            painter.DrawPolygon(roofBrush, mainPen, roofPoints);

            // Рисуем дверь
            var doorBrush = Brushes.Brown;
            var door = new RectangleModel { X = 2, Y = 2, Width = 2, Height = 4 };
            painter.DrawRectangle(doorBrush, mainPen, door);

            // Ручка двери
            var handleBrush = Brushes.Gold;
            var handle = new RectangleModel { X = 2.3, Y = 4, Width = 0.3, Height = 0.3 };
            painter.DrawEllipse(handleBrush, mainPen, handle);

            // Окно
            var windowBrush = Brushes.LightYellow;
            var window = new RectangleModel { X = 5, Y = 4, Width = 2, Height = 2 };
            painter.DrawRectangle(windowBrush, mainPen, window);

            // Перекладины на окне
            var linePen = new Pen(Color.Black, 1);
            var horizontalLine = new RectangleModel { X = 5, Y = 5, Width = 2, Height = 0.1 };
            painter.DrawRectangle(null, linePen, horizontalLine);

            var verticalLine = new RectangleModel { X = 6, Y = 4, Width = 0.1, Height = 2 };
            painter.DrawRectangle(null, linePen, verticalLine);

            // Солнце
            var sunBrush = Brushes.Yellow;
            var sun = new RectangleModel { X = -3, Y = 8, Width = 2, Height = 2 };
            painter.DrawEllipse(sunBrush, null, sun);

            // Облака
            var cloudBrush = Brushes.White;

            // Первое облако
            var cloud1 = new RectangleModel { X = 8, Y = 9, Width = 1.5, Height = 1 };
            painter.DrawEllipse(cloudBrush, null, cloud1);

            var cloud2 = new RectangleModel { X = 9, Y = 9.5, Width = 1.8, Height = 1.2 };
            painter.DrawEllipse(cloudBrush, null, cloud2);

            var cloud3 = new RectangleModel { X = 10, Y = 9, Width = 1.5, Height = 1 };
            painter.DrawEllipse(cloudBrush, null, cloud3);
        }
    }
}