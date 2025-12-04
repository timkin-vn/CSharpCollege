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
            var mainPen = new Pen(Color.Black, 3);

            // Стены
            var houseBrush = Brushes.BurlyWood;
            var housePen = mainPen;
            var houseRect = new RectangleModel { X = -4, Y = -1, Width = 4, Height = 5 };
            painter.DrawRectangle(houseBrush, housePen, houseRect);

            // Крыша
            var roofBrush = Brushes.SaddleBrown;
            var roofPoints = new[]
            {
                new PointModel { X = -4, Y = 4 },
                new PointModel { X = -2, Y = 6 },
                new PointModel { X = 0, Y = 4 },
            };
            painter.DrawPolygon(roofBrush, housePen, roofPoints);

            // Окно
            var windowBrush = Brushes.LightSkyBlue;
            var windowRect = new RectangleModel { X = -3.2, Y = 1, Width = 1.4, Height = 1.4 };
            painter.DrawRectangle(windowBrush, housePen, windowRect);

            // Дверь
            var doorBrush = Brushes.Sienna;
            var doorRect = new RectangleModel { X = -1.6, Y = -1, Width = 1.2, Height = 2.5 };
            painter.DrawRectangle(doorBrush, housePen, doorRect);

            // Труба
            var chimneyRect = new RectangleModel { X = -3.2, Y = 5, Width = 0.7, Height = 2 };
            painter.DrawRectangle(doorBrush, housePen, chimneyRect);

            // Дым (3 эллипса)
            var smokeBrush = Brushes.Gray;
            var smoke1 = new RectangleModel { X = -3.1, Y = 7, Width = 1, Height = 1 };
            var smoke2 = new RectangleModel { X = -2.8, Y = 8, Width = 1.2, Height = 1.2 };
            var smoke3 = new RectangleModel { X = -2.4, Y = 9, Width = 1.4, Height = 1.4 };

            painter.DrawEllipse(smokeBrush, null, smoke1);
            painter.DrawEllipse(smokeBrush, null, smoke2);
            painter.DrawEllipse(smokeBrush, null, smoke3);

            // Забор
            var fenceBrush = Brushes.Brown;

            double startX = -6.5;
            double step = 0.5;
            int count = 43;  // сколько столбиков

            for (int i = 0; i < count; i++)
            {
                // 6 - 13 исключаем
                if (i >= 5 && i <= 12)
                    continue;

                var rect = new RectangleModel
                {
                    X = startX + i * step,
                    Y = -1,
                    Width = 0.3,
                    Height = 3
                };

                painter.DrawRectangle(fenceBrush, null, rect);
            }

            var fenceTopLeft = new RectangleModel { X = -6.7, Y = 1, Width = 2.7, Height = 0.3 };
            var fenceBottomLeft = new RectangleModel { X = -6.7, Y = -0.5, Width = 2.7, Height = 0.3 };

            var fenceTopRight = new RectangleModel { X = -0.15, Y = 1, Width = 15, Height = 0.3 };
            var fenceBottomRight = new RectangleModel { X = -0.15, Y = -0.5, Width = 15, Height = 0.3 };


            painter.DrawRectangle(fenceBrush, null, fenceTopLeft);
            painter.DrawRectangle(fenceBrush, null, fenceBottomLeft);
            painter.DrawRectangle(fenceBrush, null, fenceTopRight);
            painter.DrawRectangle(fenceBrush, null, fenceBottomRight);

            // Солнце
            var sun = Brushes.Yellow;
            var sunBrush = new RectangleModel {X = 10, Y = 5, Width = 3, Height = 3};

            painter.DrawEllipse(sun, null, sunBrush);

            // Облака
            var cloud = Brushes.LightSkyBlue;
            var cloudBrush1 = new RectangleModel {X = 1, Y = 6.5, Width = 3, Height = 2.3};
            var cloudBrush2 = new RectangleModel {X = 3, Y = 6.5, Width = 4.5, Height = 3};
            var cloudBrush3 = new RectangleModel {X = 6, Y = 6.5, Width = 3, Height = 2.3};
            var cloudBrush4 = new RectangleModel { X = -1, Y = 7.5, Width = 3.4, Height = 2.3 };
            var cloudBrush5 = new RectangleModel { X = 2, Y = 7.5, Width = 4.9, Height = 3 };
            var cloudBrush6 = new RectangleModel { X = 5, Y = 7.5, Width = 3.6, Height = 2.7 };

            painter.DrawEllipse(cloud, null, cloudBrush1);
            painter.DrawEllipse(cloud, null, cloudBrush2);
            painter.DrawEllipse(cloud, null, cloudBrush3);
            painter.DrawEllipse(cloud, null, cloudBrush4);
            painter.DrawEllipse(cloud, null, cloudBrush5);
            painter.DrawEllipse(cloud, null, cloudBrush6);

        }


    }
}
