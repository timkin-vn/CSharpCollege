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
            var bodyBrush = Brushes.LightGray;
            var windowBrush = Brushes.CadetBlue;
            var flameBrush = Brushes.Red;

            var thinRedPen = new Pen(Color.Red, 1);
            var thickBluePen = new Pen(Color.Blue, 5);

            var customColor = Color.FromArgb(120, 80, 200);
            var customPen = new Pen(customColor, 2);

            // Основная часть ракеты (корпус)
            var rocketBody = new RectangleModel { X = 1, Y = 0, Width = 3, Height = 8 };
            painter.DrawRectangle(bodyBrush, mainPen, rocketBody);

            // Носовая часть ракеты
            var noseCone = new RectangleModel { X = 1, Y = -2, Width = 3, Height = 2 };
            painter.DrawPie(bodyBrush, mainPen, noseCone, 0, -180);

            // Окно кабины
            var window = new RectangleModel { X = 2, Y = 1, Width = 1, Height = 1 };
            painter.DrawEllipse(windowBrush, thickBluePen, window);

            // Стабилизаторы (крылья)
            var leftWing = new RectangleModel { X = -1, Y = 2, Width = 2, Height = 1 };
            painter.DrawRectangle(bodyBrush, mainPen, leftWing);

            var rightWing = new RectangleModel { X = 4, Y = 2, Width = 2, Height = 1 };
            painter.DrawRectangle(bodyBrush, mainPen, rightWing);

            // Двигатель (основание)
            var engine = new RectangleModel { X = 1.5, Y = 8, Width = 2, Height = 1 };
            painter.DrawRectangle(bodyBrush, mainPen, engine);

            // Пламя
            var flame = new RectangleModel { X = 1.5, Y = 7.5, Width = 2, Height = 3 };
            painter.DrawPie(flameBrush, thinRedPen, flame, 180, 180);

            // Детали корпуса (полосы)
            var stripe1 = new RectangleModel { X = 1, Y = 3, Width = 3, Height = 0.2 };
            painter.DrawRectangle(Brushes.Blue, mainPen, stripe1);

            var stripe2 = new RectangleModel { X = 1, Y = 5, Width = 3, Height = 0.2 };
            painter.DrawRectangle(Brushes.Red, mainPen, stripe2);

            // Антенна
            var antenna = new RectangleModel { X = 2.5, Y = -2, Width = 0.1, Height = 1 };
            var start = new PointModel { X = antenna.X + antenna.Width / 2, Y = antenna.Y + antenna.Height };
            var end = new PointModel { X = antenna.X + antenna.Width / 2, Y = antenna.Y };
            painter.DrawLine(customPen, start, end);

            var antennaBall = new RectangleModel { X = 2.3, Y = -2.5, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(flameBrush, mainPen, antennaBall);
        }
    }
}
