using Drawing.Models;
using Drawing.ViewServices;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = 0,
            Y = 0,
            Width = 10,
            Height = 10,
        };

        public void DrawPicture(Painter painter)
        {
            var blackPen = new Pen(Color.Black, 0.05f);

            var bodyBrush = new SolidBrush(Color.DarkRed);
            var noseBrush = new SolidBrush(Color.DarkOrange);
            var stabilizerBrush = new SolidBrush(Color.DarkBlue);
            var windowBrush = new SolidBrush(Color.LightSlateGray);
            var nozzleBodyBrush = new SolidBrush(Color.DimGray);
            var nozzleInnerBrush = new SolidBrush(Color.SlateGray);
            var fireBrush1 = new SolidBrush(Color.Goldenrod);
            var fireBrush2 = new SolidBrush(Color.Firebrick);
            var ringBrush = new SolidBrush(Color.SlateGray);
            var stripeBrush = new SolidBrush(Color.Gray);
            var blackBrush = Brushes.Black;

            // Корпус
            var body = new RectangleModel { X = 3, Y = 2, Width = 4, Height = 6 };
            painter.DrawRectangle(blackPen, bodyBrush, body);

            // Нос
            var nosePoints = new[]
            {
                new PointModel { X = 3, Y = 8 },
                new PointModel { X = 7, Y = 8 },
                new PointModel { X = 5, Y = 10 }
            };
            painter.DrawPolygon(blackPen, noseBrush, nosePoints);

            // Хвостовая часть
            var tail = new RectangleModel { X = 2.8, Y = 1, Width = 4.4, Height = 1 };
            painter.DrawRectangle(blackPen, nozzleBodyBrush, tail);

            // Серая полоса
            var stripe = new RectangleModel { X = 4.9, Y = 2, Width = 0.2, Height = 6 };
            painter.DrawRectangle(null, stripeBrush, stripe);

            // Кольца
            var ring1 = new RectangleModel { X = 3, Y = 7.3, Width = 4, Height = 0.2 };
            painter.DrawRectangle(blackPen, ringBrush, ring1);
            var ring2 = new RectangleModel { X = 3, Y = 5.6, Width = 4, Height = 0.2 };
            painter.DrawRectangle(blackPen, ringBrush, ring2);
            var ring3 = new RectangleModel { X = 3, Y = 3.8, Width = 4, Height = 0.2 };
            painter.DrawRectangle(blackPen, ringBrush, ring3);

            // Большие стабилизаторы
            var leftBigFin = new[]
            {
                new PointModel { X = 3, Y = 2 },
                new PointModel { X = 3, Y = 4 },
                new PointModel { X = 2, Y = 2 }
            };
            painter.DrawPolygon(blackPen, stabilizerBrush, leftBigFin);

            var rightBigFin = new[]
            {
                new PointModel { X = 7, Y = 2 },
                new PointModel { X = 7, Y = 4 },
                new PointModel { X = 8, Y = 2 }
            };
            painter.DrawPolygon(blackPen, stabilizerBrush, rightBigFin);

            // Маленькие стабилизаторы
            var leftSmallFin = new[]
            {
                new PointModel { X = 3, Y = 6.5 },
                new PointModel { X = 3, Y = 7.5 },
                new PointModel { X = 2.75, Y = 6.5 }
            };
            painter.DrawPolygon(blackPen, stabilizerBrush, leftSmallFin);

            var rightSmallFin = new[]
            {
                new PointModel { X = 7, Y = 6.5 },
                new PointModel { X = 7, Y = 7.5 },
                new PointModel { X = 7.25, Y = 6.5 }
            };
            painter.DrawPolygon(blackPen, stabilizerBrush, rightSmallFin);

            // Сопло
            var nozzleCone = new[]
            {
                new PointModel { X = 4.2, Y = 1 },
                new PointModel { X = 5.8, Y = 1 },
                new PointModel { X = 6.2, Y = 0.2 },
                new PointModel { X = 3.8, Y = 0.2 }
            };
            painter.DrawPolygon(blackPen, nozzleBodyBrush, nozzleCone);

            var nozzleInner = new RectangleModel { X = 4.5, Y = 0.5, Width = 1, Height = 0.6 };
            painter.DrawEllipse(blackPen, nozzleInnerBrush, nozzleInner);

            var nozzleCenter = new RectangleModel { X = 4.9, Y = 0.8, Width = 0.2, Height = 0.2 };
            painter.DrawEllipse(null, blackBrush, nozzleCenter);

            // Пламя
            var flameInner = new RectangleModel { X = 4.2, Y = -0.5, Width = 1.6, Height = 1.5 };
            painter.DrawPie(null, fireBrush1, flameInner, 0, 180);
            var flameOuter = new RectangleModel { X = 4, Y = -0.8, Width = 2, Height = 1.8 };
            painter.DrawPie(null, fireBrush2, flameOuter, 0, 180);

            // Иллюминаторы
            var window1 = new RectangleModel { X = 4.2, Y = 5.5, Width = 1, Height = 1 };
            painter.DrawEllipse(blackPen, windowBrush, window1);
            var highlight1 = new RectangleModel { X = 4.5, Y = 6.2, Width = 0.2, Height = 0.2 };
            painter.DrawEllipse(null, Brushes.White, highlight1);

            var window2 = new RectangleModel { X = 4.2, Y = 4, Width = 1, Height = 1 };
            painter.DrawEllipse(blackPen, windowBrush, window2);
            var highlight2 = new RectangleModel { X = 4.5, Y = 4.7, Width = 0.2, Height = 0.2 };
            painter.DrawEllipse(null, Brushes.White, highlight2);
        }
    }
}