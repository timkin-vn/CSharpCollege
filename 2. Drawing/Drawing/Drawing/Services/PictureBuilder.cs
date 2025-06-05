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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 20, Y = -1, Height = 15 };

        public void BuildPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 2);

            // Цвета
            var bodyBrush = new SolidBrush(Color.Silver);             // Серебристый корпус
            var windowBrush = new SolidBrush(Color.LightSkyBlue);     // Окно
            var flameBrush = new SolidBrush(Color.OrangeRed);         // Пламя
            var wingBrush = new SolidBrush(Color.Red);                // Крылья
            var noseBrush = new SolidBrush(Color.DarkBlue);           // Нос
            var decorBrush = new SolidBrush(Color.Gold);              // Декоративные полосы
            var finBrush = new SolidBrush(Color.Green);               // Хвостовые стабилизаторы
            var flameYellowBrush = new SolidBrush(Color.Yellow);      // Яркое пламя

            // 1. Корпус (прямоугольник)
            var bodyRect = new RectangleModel { X = 3.5, Y = 5, Width = 3, Height = 5 };
            painter.DrawRectangle(bodyBrush, mainPen, bodyRect);

            // 2. Нос (эллипс)
            var noseRect = new RectangleModel { X = 3.5, Y = 3.5, Width = 3, Height = 2 };
            painter.DrawEllipse(noseBrush, mainPen, noseRect);

            // 3. Окно (эллипс)
            var windowRect = new RectangleModel { X = 4.5, Y = 6.2, Width = 1, Height = 1 };
            painter.DrawEllipse(windowBrush, mainPen, windowRect);

            // 4. Декоративные полосы (прямоугольники)
            var decorRect1 = new RectangleModel { X = 3.5, Y = 7, Width = 3, Height = 0.3 };
            var decorRect2 = new RectangleModel { X = 3.5, Y = 8.2, Width = 3, Height = 0.3 };
            painter.DrawRectangle(decorBrush, null, decorRect1);
            painter.DrawRectangle(decorBrush, null, decorRect2);

            // 5. Левое крыло (трапеция)
            var leftWingPoints = new[]
            {
                new PointModel { X = 3.5, Y = 8.5 },
                new PointModel { X = 2.2, Y = 10 },
                new PointModel { X = 3.5, Y = 10 },
                new PointModel { X = 4.1, Y = 8.5 }
            };
            painter.DrawPolygon(wingBrush, mainPen, leftWingPoints);

            // 6. Правое крыло (трапеция)
            var rightWingPoints = new[]
            {
                new PointModel { X = 6.5, Y = 8.5 },
                new PointModel { X = 7.8, Y = 10 },
                new PointModel { X = 6.5, Y = 10 },
                new PointModel { X = 5.9, Y = 8.5 }
            };
            painter.DrawPolygon(wingBrush, mainPen, rightWingPoints);

            // 7. Левый хвостовой стабилизатор (треугольник)
            var leftFinPoints = new[]
            {
                new PointModel { X = 3.7, Y = 10 },
                new PointModel { X = 2.7, Y = 11 },
                new PointModel { X = 4, Y = 11 }
            };
            painter.DrawPolygon(finBrush, mainPen, leftFinPoints);

            // 8. Правый хвостовой стабилизатор (треугольник)
            var rightFinPoints = new[]
            {
                new PointModel { X = 6.3, Y = 10 },
                new PointModel { X = 7.3, Y = 11 },
                new PointModel { X = 6, Y = 11 }
            };
            painter.DrawPolygon(finBrush, mainPen, rightFinPoints);

            // 9. Центральный хвостовой стабилизатор (треугольник)
            var centerFinPoints = new[]
            {
                new PointModel { X = 5, Y = 10 },
                new PointModel { X = 5.5, Y = 11.2 },
                new PointModel { X = 4.5, Y = 11.2 }
            };
            painter.DrawPolygon(finBrush, mainPen, centerFinPoints);

            // 10. Пламя (внешний слой, оранжевый)
            var flamePoints = new[]
            {
                new PointModel { X = 4, Y = 11 },
                new PointModel { X = 6, Y = 11 },
                new PointModel { X = 5.7, Y = 12 },
                new PointModel { X = 5, Y = 11.7 },
                new PointModel { X = 4.3, Y = 12 }
            };
            painter.DrawPolygon(flameBrush, mainPen, flamePoints);

            // 11. Пламя (внутренний слой, жёлтый)
            var flameYellowPoints = new[]
            {
                new PointModel { X = 4.5, Y = 11.2 },
                new PointModel { X = 5.5, Y = 11.2 },
                new PointModel { X = 5.2, Y = 11.8 },
                new PointModel { X = 5, Y = 11.6 },
                new PointModel { X = 4.8, Y = 11.8 }
            };
            painter.DrawPolygon(flameYellowBrush, mainPen, flameYellowPoints);

            // 12. Декоративная вертикальная линия (центральная)
            var decoPen = new Pen(Color.DarkRed, 1.2f);
            painter.DrawLine(decoPen, new PointModel { X = 5, Y = 5.5 }, new PointModel { X = 5, Y = 10 });

            // Освобождение ресурсов
            mainPen.Dispose();
            noseBrush.Dispose();
            bodyBrush.Dispose();
            windowBrush.Dispose();
            flameBrush.Dispose();
            wingBrush.Dispose();
            decorBrush.Dispose();
            finBrush.Dispose();
            flameYellowBrush.Dispose();
            decoPen.Dispose();
        }
    }
}