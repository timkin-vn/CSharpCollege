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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = 0, Y = 0, Width = 10, Height = 20 };

        private double FlipY(double y, double height, double totalHeight)
        {
            return totalHeight - y - height;
        }

        public void BuildPicture(Painter painter)
        {
            var pen = new Pen(Color.Black, 0.2f);
            double H = PictureBounds.Height;

            // Основание (прямоугольник)
            var baseBrush = Brushes.SaddleBrown;
            var baseRect = new RectangleModel { X = 1, Y = FlipY(16, 4, H), Width = 8, Height = 4 };
            painter.DrawRectangle(baseBrush, pen, baseRect);

            // Основной корпус (прямоугольник)
            var bodyBrush = Brushes.LightGray;
            var bodyRect = new RectangleModel { X = 3, Y = FlipY(8, 10, H), Width = 4, Height = 10 };
            painter.DrawRectangle(bodyBrush, pen, bodyRect);

            // Круглое окно (эллипс)
            var roundWindowBrush = Brushes.LightBlue;
            var roundWindowRect = new RectangleModel { X = 4.2, Y = FlipY(13, 1.6, H), Width = 1.6, Height = 1.6 };
            painter.DrawEllipse(roundWindowBrush, pen, roundWindowRect);

            // Арка (дуга)
            var archBrush = Brushes.DarkRed;
            var archRect = new RectangleModel { X = 4.2, Y = FlipY(17, 1.6, H), Width = 1.6, Height = 1.6 };
            painter.DrawPie(archBrush, pen, archRect, 180, 180);

            // Балкон (многоугольник)
            var balconyBrush = Brushes.Gold;
            var balcony = new[]
            {
                new PointModel { X = 3.2, Y = FlipY(12, 0, H) },
                new PointModel { X = 6.8, Y = FlipY(12, 0, H) },
                new PointModel { X = 6.4, Y = FlipY(12.5, 0, H) },
                new PointModel { X = 3.6, Y = FlipY(12.5, 0, H) }
            };
            painter.DrawPolygon(balconyBrush, pen, balcony);

            // Декоративная линия
            var decoPen = new Pen(Color.DarkGreen, 0.18f);
            painter.DrawLine(decoPen,
                new PointModel { X = 3, Y = FlipY(10, 0, H) },
                new PointModel { X = 7, Y = FlipY(10, 0, H) });

            // Треугольное окно
            var triangleBrush = Brushes.MediumPurple;
            var triangle = new[]
            {
                new PointModel { X = 5, Y = FlipY(9, 0, H) },
                new PointModel { X = 4.5, Y = FlipY(10, 0, H) },
                new PointModel { X = 5.5, Y = FlipY(10, 0, H) }
            };
            painter.DrawPolygon(triangleBrush, pen, triangle);

            // Крыша (треугольник)
            var roofBrush = Brushes.OrangeRed;
            var roofPoints = new[]
            {
                new PointModel { X = 2.5, Y = FlipY(8, 0, H) },
                new PointModel { X = 7.5, Y = FlipY(8, 0, H) },
                new PointModel { X = 5, Y = FlipY(4, 0, H) }
            };
            painter.DrawPolygon(roofBrush, pen, roofPoints);

            // Флагшток (линия)
            var flagPen = new Pen(Color.Black, 0.15f);
            painter.DrawLine(flagPen,
                new PointModel { X = 5, Y = FlipY(4, 0, H) },
                new PointModel { X = 5, Y = FlipY(2.5, 0, H) });

            // Флаг (треугольник)
            var flagBrush = Brushes.Red;
            var flagPoints = new[]
            {
                new PointModel { X = 5, Y = FlipY(1.5, 0, H) },
                new PointModel { X = 5.7, Y = FlipY(1.8, 0, H) },
                new PointModel { X = 5, Y = FlipY(2.1, 0, H) }
            };
            painter.DrawPolygon(flagBrush, pen, flagPoints);
        }
    }
}
