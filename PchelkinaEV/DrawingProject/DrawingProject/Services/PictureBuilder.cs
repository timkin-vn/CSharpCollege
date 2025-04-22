using DrawingProject.Models;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
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
            // основа
            var mainPen = new Pen(Color.Black, 3);
            var brushBody = Brushes.DarkRed;

            var rect = new RectangleModel { X = -1, Y = 1, Width = 9, Height = 6.5 };
            painter.DrawRectangle(brushBody, mainPen, rect);

            // труба
            var brushChimney = new HatchBrush(HatchStyle.ForwardDiagonal, Color.LightGray);
            var chimneyPen = new Pen(Color.Black, 3);

            rect = new RectangleModel { X = -0.2, Y = 8, Width = 1, Height = 2 };
            painter.DrawRectangle(brushChimney, chimneyPen, rect);

            // крыша
            var brushRoof = Brushes.Red;
            var penRoof = new Pen(Color.DarkRed, 3);
            PointModels[] roofModel = new PointModels[]
            {
                new PointModels { X = -1, Y = 7.5 }, // Левая нижняя точка
                new PointModels { X = 8, Y = 7.5 },  // Правая нижняя точка
                new PointModels { X = 3.5, Y = 10.5 }  // Верхняя точка
            };
            painter.DrawPolygon(brushRoof, penRoof, roofModel);

            var penWindow = new Pen(Color.Blue, 2);
            var brushWindow = Brushes.LightBlue;

            // окно слева
            rect = new RectangleModel { X = -0.5, Y = 5, Width = 2, Height = 2 };
            painter.DrawRectangle(brushWindow, penWindow, rect);

            // разделители для левого окна
            double midXLeft = rect.X + rect.Width / 2;
            double midYLeft = rect.Y + rect.Height / 2;

            painter.DrawLine(penWindow, new PointModels { X = rect.X, Y = midYLeft }, new PointModels { X = rect.X + rect.Width, Y = midYLeft }); // горизонтальная
            painter.DrawLine(penWindow, new PointModels { X = midXLeft, Y = rect.Y }, new PointModels { X = midXLeft, Y = rect.Y + rect.Height }); // вертикальная

            // окно посередине
            rect = new RectangleModel { X = 2.5, Y = 5, Width = 2, Height = 2 };
            painter.DrawRectangle(brushWindow, penWindow, rect);

            // разделители для среднего окна
            double midXMid = rect.X + rect.Width / 2;
            double midYMid = rect.Y + rect.Height / 2;

            painter.DrawLine(penWindow, new PointModels { X = rect.X, Y = midYMid }, new PointModels { X = rect.X + rect.Width, Y = midYMid }); // горизонтальная
            painter.DrawLine(penWindow, new PointModels { X = midXMid, Y = rect.Y }, new PointModels { X = midXMid, Y = rect.Y + rect.Height }); // вертикальная

            // окно справа
            rect = new RectangleModel { X = 5.5, Y = 5, Width = 2, Height = 2 };
            painter.DrawRectangle(brushWindow, penWindow, rect);

            // разделители для правого окна
            double midXRight = rect.X + rect.Width / 2;
            double midYRight = rect.Y + rect.Height / 2;

            painter.DrawLine(penWindow, new PointModels { X = rect.X, Y = midYRight }, new PointModels { X = rect.X + rect.Width, Y = midYRight }); // горизонтальная
            painter.DrawLine(penWindow, new PointModels { X = midXRight, Y = rect.Y }, new PointModels { X = midXRight, Y = rect.Y + rect.Height }); // вертикальная

            // дверь
            var brushDoor = Brushes.LightGray;
            var penDoor = new Pen(Color.Gray, 2);
            rect = new RectangleModel { X = 3.5, Y = 1, Width = 3, Height = 3.5 };
            painter.DrawRectangle(brushDoor, penDoor, rect);

            // ручка двери
            var penDoorknob = new Pen(Color.Black, 1);
            var brushDoorknob = Brushes.Black;
            rect = new RectangleModel { X = 6, Y = 3, Width = 0.3, Height = 0.3 };
            painter.DrawEllipse(brushDoorknob, penDoorknob, rect);
        }
    }
}