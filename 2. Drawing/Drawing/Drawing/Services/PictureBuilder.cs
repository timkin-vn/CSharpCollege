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
            // === ПЕРА ===
            var pen1 = new Pen(Color.Black, 2); // основное
            var pen2 = new Pen(Color.Red, 3);   // для контура машины
            var pen3 = new Pen(Color.FromArgb(128, 64, 255), 2); // фиолетовое RGB-перо

            // === КИСТИ ===
            var carBrush = Brushes.LightGray;
            var houseBrush = Brushes.SandyBrown;
            var roofBrush = Brushes.Firebrick;

            // === МАШИНА (сдвинута вправо) ===
            // Корпус
            painter.DrawRectangle(carBrush, pen2, new RectangleModel { X = 3, Y = 3, Width = 8, Height = 2 });

            // Окна
            painter.DrawRectangle(Brushes.LightSkyBlue, pen2, new RectangleModel { X = 4, Y = 5, Width = 2, Height = 1 });
            painter.DrawRectangle(Brushes.LightSkyBlue, pen2, new RectangleModel { X = 7, Y = 5, Width = 2, Height = 1 });

            // Колёса
            painter.DrawEllipse(Brushes.DarkGray, pen2, new RectangleModel { X = 4, Y = 1.5, Width = 1.5, Height = 1.5 });
            painter.DrawEllipse(Brushes.DarkGray, pen2, new RectangleModel { X = 8.5, Y = 1.5, Width = 1.5, Height = 1.5 });

            // Фары
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = 2.5, Y = 4, Width = 0.5, Height = 0.5 });
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = 11, Y = 4, Width = 0.5, Height = 0.5 });

            // === ДОМ (остался на месте) ===
            // Стены
            painter.DrawRectangle(houseBrush, pen1, new RectangleModel { X = -4, Y = 3, Width = 5, Height = 4 });

            // Крыша (треугольник)
            var roofPoints = new[]
            {
        new PointModel { X = -4, Y = 7 },
        new PointModel { X = -1.5, Y = 9 },
        new PointModel { X = 1, Y = 7 }
    };
            painter.DrawPolygon(roofBrush, pen1, roofPoints);

            // Дверь
            painter.DrawRectangle(Brushes.Brown, pen1, new RectangleModel { X = -3, Y = 3, Width = 1, Height = 2 });

            // Дым из трубы
            painter.DrawEllipse(Brushes.LightGray, pen3, new RectangleModel { X = -2.5, Y = 8.7, Width = 0.8, Height = 0.8 });
            painter.DrawEllipse(Brushes.LightGray, pen3, new RectangleModel { X = -3, Y = 9, Width = 1, Height = 1 });
            painter.DrawEllipse(Brushes.LightGray, pen3, new RectangleModel { X = -2.7, Y = 9.5, Width = 1.2, Height = 1.2 });

            // Труба
            painter.DrawRectangle(Brushes.Gray, pen1, new RectangleModel { X = -2.8, Y = 7.5, Width = 0.5, Height = 1.5 });
        }
    }
}
