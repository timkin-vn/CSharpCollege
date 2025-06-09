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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 18, Y = -1, Height = 12, };

        public void BuildPicture(Painter painter)
        {
            var pen = new Pen(Color.Black, 2);
            double Xc = 4, Yc = 5.5;

            // 1. Вода (прямоугольник)
            var waterBrush = Brushes.LightBlue;
            var waterRect = new RectangleModel
            {
                X = 2 * Xc - (-5 + 18), // X = -5 + 18 = 13, X' = 8 - 13 = -5
                Y = 2 * Yc - (10 + 2),  // Y = 10 + 2 = 12, Y' = 11 - 12 = -1
                Width = 18,
                Height = 2
            };
            painter.DrawRectangle(waterBrush, null, waterRect);

            // 2. Корпус корабля (многоугольник)
            var hullBrush = Brushes.SaddleBrown;
            var hullPoints = new[]
            {
                new PointModel { X = 2 * Xc - 2, Y = 2 * Yc - 9 },   // (6, 2)
                new PointModel { X = 2 * Xc - 14, Y = 2 * Yc - 9 },  // (-6, 2)
                new PointModel { X = 2 * Xc - 12, Y = 2 * Yc - 7 },  // (-4, 4)
                new PointModel { X = 2 * Xc - 4, Y = 2 * Yc - 7 }    // (4, 4)
            };
            painter.DrawPolygon(hullBrush, pen, hullPoints);

            // 3. Палуба (прямоугольник)
            var deckBrush = Brushes.Peru;
            var deckRect = new RectangleModel
            {
                X = 2 * Xc - (5 + 6), // X = 11, X' = 8 - 11 = -3
                Y = 2 * Yc - (6 + 1), // Y = 7, Y' = 11 - 7 = 4
                Width = 6,
                Height = 1
            };
            painter.DrawRectangle(deckBrush, pen, deckRect);

            // 4. Каюта (прямоугольник)
            var cabinBrush = Brushes.LightGray;
            var cabinRect = new RectangleModel
            {
                X = 2 * Xc - (7 + 2), // X = 9, X' = 8 - 9 = -1
                Y = 2 * Yc - (4.5 + 1.5), // Y = 6, Y' = 11 - 6 = 5
                Width = 2,
                Height = 1.5
            };
            painter.DrawRectangle(cabinBrush, pen, cabinRect);

            // 5-7. Иллюминаторы (эллипсы)
            var windowBrush = Brushes.LightSkyBlue;
            for (double x = 5.7; x <= 9.3; x += 1.8)
            {
                var windowRect = new RectangleModel
                {
                    X = 2 * Xc - (x + 0.7), // X' = 8 - (x + 0.7)
                    Y = 2 * Yc - (7.7 + 0.7), // Y' = 11 - 8.4 = 2.6
                    Width = 0.7,
                    Height = 0.7
                };
                painter.DrawEllipse(windowBrush, Pens.DarkBlue, windowRect);
            }

            // 8. Мачта (линия)
            var mastPen = new Pen(Color.SaddleBrown, 2);
            painter.DrawLine(
                mastPen,
                new PointModel { X = 2 * Xc - 8, Y = 2 * Yc - 6 }, // (0, 5)
                new PointModel { X = 2 * Xc - 8, Y = 2 * Yc - 2 }  // (0, 9)
            );

            // 9. Парус (треугольник)
            var sailBrush = Brushes.White;
            var sailPoints = new[]
            {
                new PointModel { X = 2 * Xc - 8, Y = 2 * Yc - 2 }, // (0, 9)
                new PointModel { X = 2 * Xc - 8, Y = 2 * Yc - 6 }, // (0, 5)
                new PointModel { X = 2 * Xc - 11, Y = 2 * Yc - 6 } // (-3, 5)
            };
            painter.DrawPolygon(sailBrush, pen, sailPoints);

            // 10. Флаг (прямоугольник)
            var flagBrush = Brushes.Red;
            var flagRect = new RectangleModel
            {
                X = 2 * Xc - (8 + 1), // X = 9, X' = 8 - 9 = -1
                Y = 2 * Yc - (2 + 0.5), // Y = 2.5, Y' = 11 - 2.5 = 8.5
                Width = 1,
                Height = 0.5
            };
            painter.DrawRectangle(flagBrush, pen, flagRect);

            // 11. корма (нос корабля, полукруг)
            var noseBrush = Brushes.DarkRed;
            var noseRect = new RectangleModel
            {
                X = 2.26 * Xc - (13 + 2), // X = 15, X' = 8 - 15 = -7
                Y = 2 * Yc - (7.5 + 3), // Y = 10.5, Y' = 11 - 10.5 = 0.5
                Width = 12,
                Height = 3
            };
            painter.DrawPie(noseBrush, pen, noseRect, 0, 180);

            // 12. Тень от корабля (эллипс)
            var shadowBrush = Brushes.Gray;
            var shadowRect = new RectangleModel
            {
                X = 2 * Xc - (4 + 8), // X = 12, X' = 8 - 12 = -4
                Y = 2 * Yc - (10.5 + 0.7), // Y = 11.2, Y' = 11 - 11.2 = -0.2
                Width = 8,
                Height = 0.7
            };
            painter.DrawEllipse(shadowBrush, null, shadowRect);
        }
    }
}
