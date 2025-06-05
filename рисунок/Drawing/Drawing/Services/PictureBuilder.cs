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
            X = -10,
            Y = -5,
            Width = 30,
            Height = 20,
        };

        public void DrawPicture(Painter painter)
        {
            // Перья
            var mainPen = new Pen(Color.Black, 3);
            var roofPen = new Pen(Color.Red, 2);
            var windowPen = new Pen(Color.Green, 1);

            // Кисти
            var wallBrush = Brushes.Bisque;
            var roofBrush = Brushes.SaddleBrown;
            var windowBrush = Brushes.LightSkyBlue;
            var doorBrush = new SolidBrush(Color.FromArgb(139, 69, 19)); // Цвет через RGB

            // Стены дома
            var houseRect = new RectangleModel { X = 0, Y = 0, Width = 10, Height = 8 };
            painter.DrawRectangle(wallBrush, mainPen, houseRect);

            // Крыша (треугольник)
            var roofPoints = new[]
            {
                new PointModel { X = 0, Y = 0 },
                new PointModel { X = 5, Y = -4 },
                new PointModel { X = 10, Y = 0 }
            };
            painter.DrawPolygon(roofBrush, roofPen, roofPoints);

            // Окно слева
            var windowLeft = new RectangleModel { X = 1, Y = 1, Width = 2, Height = 3 };
            painter.DrawRectangle(windowBrush, windowPen, windowLeft);

            // Рамки окна слева
            painter.DrawLine(windowPen, new PointModel { X = 2, Y = 1 }, new PointModel { X = 2, Y = 4 });
            painter.DrawLine(windowPen, new PointModel { X = 1, Y = 2.5 }, new PointModel { X = 3, Y = 2.5 });

            // Окно справа
            var windowRight = new RectangleModel { X = 7, Y = 1, Width = 2, Height = 3 };
            painter.DrawRectangle(windowBrush, windowPen, windowRight);

            // Рамки окна справа
            painter.DrawLine(windowPen, new PointModel { X = 8, Y = 1 }, new PointModel { X = 8, Y = 4 });
            painter.DrawLine(windowPen, new PointModel { X = 7, Y = 2.5 }, new PointModel { X = 9, Y = 2.5 });

            // Дверь
            var door = new RectangleModel { X = 4, Y = 3, Width = 2, Height = 5 };
            painter.DrawRectangle(doorBrush, mainPen, door);

            // Ручка двери
            var doorKnob = new RectangleModel { X = 5.6, Y = 5.5, Width = 0.3, Height = 0.3 };
            painter.DrawEllipse(Brushes.Gold, mainPen, doorKnob);

            // Труба
            var chimneyPoints = new[]
            {
                new PointModel { X = 8, Y = -2 },
                new PointModel { X = 9, Y = -2 },
                new PointModel { X = 9, Y = -4 },
                new PointModel { X = 8, Y = -4 }
            };
            painter.DrawPolygon(Brushes.Gray, mainPen, chimneyPoints);

            // Дым из трубы (сектора как капли дыма)
            painter.DrawPie(Brushes.LightGray, null, new RectangleModel { X = 8.3, Y = -4.5, Width = 0.5, Height = 0.5 }, 0, 360);
            painter.DrawPie(Brushes.DarkGray, null, new RectangleModel { X = 8.6, Y = -5, Width = 0.6, Height = 0.6 }, 0, 360);

            // Дерево рядом с домом
            painter.DrawRectangle(Brushes.SaddleBrown, mainPen, new RectangleModel { X = 13, Y = 2, Width = 0.5, Height = 4 });
            painter.DrawEllipse(Brushes.ForestGreen, null, new RectangleModel { X = 12, Y = 0.5, Width = 2.5, Height = 2.5 });
        }
    }
}