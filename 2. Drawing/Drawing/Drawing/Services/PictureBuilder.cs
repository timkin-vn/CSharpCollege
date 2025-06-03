using Drawing.Models;
using System;
using System.Drawing;
using System.Linq;

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
            // ==== Пера ====
            var penBlack = new Pen(Color.Black, 2);
            var penRed = new Pen(Color.Red, 3);
            var penRgb = new Pen(Color.FromArgb(123, 45, 200), 4); // нестандартный цвет

            // ==== Кисти ====
            var brushGray = Brushes.Gray;
            var brushYellow = Brushes.Yellow;
            var brushCustom = new SolidBrush(Color.FromArgb(100, 200, 150)); // нестандартный цвет

            // ==== Домик ====

            // Основа дома (квадрат)
            var houseBase = new RectangleModel { X = 1, Y = 2, Width = 4, Height = 4 };
            painter.DrawRectangle(brushGray, penBlack, houseBase);

            // Крыша (треугольник)
            var roof = new[]
            {
                new PointModel { X = 1, Y = 6 },
                new PointModel { X = 3, Y = 8 },
                new PointModel { X = 5, Y = 6 },
            };
            painter.DrawPolygon(brushYellow, penRed, roof);

            // Окна (2 квадрата)
            painter.DrawRectangle(brushCustom, penBlack, new RectangleModel { X = 1.2, Y = 2.3, Width = 1, Height = 1 });
            painter.DrawRectangle(brushCustom, penBlack, new RectangleModel { X = 3.8, Y = 2.3, Width = 1, Height = 1 });

            // Дверь (прямоугольник)
            painter.DrawRectangle(Brushes.Brown, penBlack, new RectangleModel { X = 2.2, Y = 2, Width = 1.6, Height = 2 });

            // Дымоход (прямоугольник)
            painter.DrawRectangle(Brushes.DarkRed, penBlack, new RectangleModel { X = 4.5, Y = 6.5, Width = 0.4, Height = 1.2 });

            // Дым (3 овала)
            painter.DrawEllipse(Brushes.LightGray, penRgb, new RectangleModel { X = 4.5, Y = 7.8, Width = 0.8, Height = 0.5 });
            painter.DrawEllipse(Brushes.LightGray, penRgb, new RectangleModel { X = 4.2, Y = 8.4, Width = 1, Height = 0.6 });
            painter.DrawEllipse(Brushes.LightGray, penRgb, new RectangleModel { X = 3.9, Y = 9.2, Width = 1.2, Height = 0.7 });

            // ==== Корова ====

            // Туловище (овал)
            painter.DrawEllipse(Brushes.White, penBlack, new RectangleModel { X = 10, Y = 2, Width = 5, Height = 2 });

            // Голова (круг)
            painter.DrawEllipse(Brushes.White, penBlack, new RectangleModel { X = 15.2, Y = 2.5, Width = 1, Height = 1 });

            // Ноги (4 овала)
            painter.DrawEllipse(Brushes.Black, penRed, new RectangleModel { X = 10.5, Y = 1, Width = 0.4, Height = 1 });
            painter.DrawEllipse(Brushes.Black, penRed, new RectangleModel { X = 11.5, Y = 1, Width = 0.4, Height = 1 });
            painter.DrawEllipse(Brushes.Black, penRed, new RectangleModel { X = 13.5, Y = 1, Width = 0.4, Height = 1 });
            painter.DrawEllipse(Brushes.Black, penRed, new RectangleModel { X = 14.5, Y = 1, Width = 0.4, Height = 1 });
        }
    }
}
