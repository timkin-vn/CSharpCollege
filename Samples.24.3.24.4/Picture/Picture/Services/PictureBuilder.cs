using Picture.Models;
using Picture.ViewServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Picture.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel(0, 0, 500, 500);

        public void DrawPicture(Painter painter)
        {
            // Кисти и перья
            var pen = new Pen(Color.DarkGreen, 3);
            var brush = Brushes.Yellow;

            // Голова (эллипс)
            var headRect = new RectangleModel(
                SourceBounds.Width * 2 / 5,     // X = 200
                SourceBounds.Height / 5,         // Y = 100
                SourceBounds.Width / 5,           // Width = 100
                SourceBounds.Height / 5           // Height = 100
            );
            painter.DrawEllipse(pen, brush, headRect);

            // Тело (прямоугольник)
            var bodyRect = new RectangleModel(
                SourceBounds.Width * 2 / 5,     // X = 200
                SourceBounds.Height * 2 / 5,     // Y = 200
                SourceBounds.Width / 5,           // Width = 100
                SourceBounds.Height / 5           // Height = 100
            );
            painter.DrawRectangle(pen, brush, bodyRect);

            // Левая рука
            var leftHandPoints = new[]
            {
                new PointModel(SourceBounds.Width * 2 / 5, SourceBounds.Height * 2 / 5),
                new PointModel(SourceBounds.Width / 5, SourceBounds.Height * 3 / 5)
            };
            painter.DrawLine(pen, leftHandPoints[0], leftHandPoints[1]);

            // Правая рука
            var rightHandPoints = new[]
            {
                new PointModel(SourceBounds.Width * 3 / 5, SourceBounds.Height * 2 / 5),
                new PointModel(SourceBounds.Width * 4 / 5, SourceBounds.Height * 3 / 5)
            };
            painter.DrawLine(pen, rightHandPoints[0], rightHandPoints[1]);

            // Левая нога
            var leftLegPoints = new[]
            {
                new PointModel(SourceBounds.Width * 2 / 5, SourceBounds.Height * 3 / 5),
                new PointModel(SourceBounds.Width * 2 / 5, SourceBounds.Height * 4 / 5)
            };
            painter.DrawLine(pen, leftLegPoints[0], leftLegPoints[1]);

            // Правая нога
            var rightLegPoints = new[]
            {
                new PointModel(SourceBounds.Width * 3 / 5, SourceBounds.Height * 3 / 5),
                new PointModel(SourceBounds.Width * 3 / 5, SourceBounds.Height * 4 / 5)
            };
            painter.DrawLine(pen, rightLegPoints[0], rightLegPoints[1]);
        }
    }
}