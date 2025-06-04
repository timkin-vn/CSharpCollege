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
            Y = -2,
            Width = 15,
            Height = 20,
        };

        public void DrawPicture(Painter painter)
        {
            // Пера
            var blackPen = new Pen(Color.Black, 2);
            var bluePen = new Pen(Color.Blue, 1);
            var rgbPen = new Pen(Color.FromArgb(50, 50, 50), 3);

            // Кисти
            var skinBrush = new SolidBrush(Color.FromArgb(255, 220, 180));
            var redShirtBrush = Brushes.Red;
            var pantsBrush = new SolidBrush(Color.FromArgb(100, 149, 237)); // CornflowerBlue

            // Голова
            var head = new[]
            {
                new PointModel { X = 2.5, Y = 15 },
                new PointModel { X = 3.5, Y = 16 },
                new PointModel { X = 4.5, Y = 15 },
                new PointModel { X = 4.5, Y = 13.5 },
                new PointModel { X = 3.5, Y = 13 },
                new PointModel { X = 2.5, Y = 13.5 },
            };
            painter.DrawPolygon(skinBrush, blackPen, head);

            // Тело
            painter.DrawRectangle(redShirtBrush, blackPen, new RectangleModel { X = 2.3, Y = 9, Width = 2.5, Height = 4 });

            // Рукава
            var leftSleeve = new[]
            {
                new PointModel { X = 2.3, Y = 12 },
                new PointModel { X = 1.5, Y = 11 },
                new PointModel { X = 2.3, Y = 11 },
            };
            var rightSleeve = new[]
            {
                new PointModel { X = 4.8, Y = 12 },
                new PointModel { X = 5.6, Y = 11 },
                new PointModel { X = 4.8, Y = 11 },
            };
            painter.DrawPolygon(redShirtBrush, blackPen, leftSleeve);
            painter.DrawPolygon(redShirtBrush, blackPen, rightSleeve);

            // Руки
            painter.DrawPolygon(skinBrush, blackPen, new[]
            {
                new PointModel { X = 1.5, Y = 11 },
                new PointModel { X = 2.3, Y = 9 },
                new PointModel { X = 2.1, Y = 9 },
                new PointModel { X = 1.2, Y = 10.8 },
            });
            painter.DrawPolygon(skinBrush, blackPen, new[]
            {
                new PointModel { X = 5.6, Y = 11 },
                new PointModel { X = 4.8, Y = 9 },
                new PointModel { X = 5, Y = 9 },
                new PointModel { X = 5.9, Y = 10.8 },
            });

            // Ноги
            painter.DrawRectangle(pantsBrush, blackPen, new RectangleModel { X = 2.3, Y = 5.5, Width = 1.2, Height = 3.5 });
            painter.DrawRectangle(pantsBrush, blackPen, new RectangleModel { X = 3.6, Y = 5.5, Width = 1.2, Height = 3.5 });

            // Обувь
            painter.DrawPolygon(Brushes.Brown, blackPen, new[]
            {
                new PointModel { X = 2.3, Y = 5.5 },
                new PointModel { X = 2, Y = 5 },
                new PointModel { X = 3.5, Y = 5 },
                new PointModel { X = 3.5, Y = 5.5 },
            });
            painter.DrawPolygon(Brushes.Brown, blackPen, new[]
            {
                new PointModel { X = 3.6, Y = 5.5 },
                new PointModel { X = 3.6, Y = 5 },
                new PointModel { X = 5.1, Y = 5 },
                new PointModel { X = 4.8, Y = 5.5 },
            });

            // Глаза
            painter.DrawEllipse(Brushes.White, blackPen, new RectangleModel { X = 2.7, Y = 14.1, Width = 0.6, Height = 0.6 });
            painter.DrawEllipse(Brushes.White, blackPen, new RectangleModel { X = 3.7, Y = 14.1, Width = 0.6, Height = 0.6 });
            painter.DrawEllipse(Brushes.Black, null, new RectangleModel { X = 2.9, Y = 14.3, Width = 0.2, Height = 0.2 });
            painter.DrawEllipse(Brushes.Black, null, new RectangleModel { X = 3.9, Y = 14.3, Width = 0.2, Height = 0.2 });

            // Лампочка
            painter.DrawEllipse(Brushes.Yellow, blackPen, new RectangleModel { X = 3.2, Y = 16.5, Width = 0.6, Height = 0.8 });
            painter.DrawRectangle(Brushes.Gray, blackPen, new RectangleModel { X = 3.3, Y = 16.3, Width = 0.4, Height = 0.2 });

            // Нос
            painter.DrawLine(blackPen, new PointModel { X = 3.45, Y = 13.8 }, new PointModel { X = 3.45, Y = 13.5 });

            // Рот
            painter.DrawLine(blackPen, new PointModel { X = 3.2, Y = 13.2 }, new PointModel { X = 3.7, Y = 13.2 });

            // Брови
            var browPen = new Pen(Color.FromArgb(139, 69, 19), 3); // тёмно-коричневый, толщина 3
                        
            painter.DrawLine(browPen,
                new PointModel { X = 2.7, Y = 14.85 },
                new PointModel { X = 3.2, Y = 14.85 });
                        
            painter.DrawLine(browPen,
                new PointModel { X = 3.8, Y = 14.85 },
                new PointModel { X = 4.3, Y = 14.85 });

        }
    }
}
