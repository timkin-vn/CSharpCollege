using System.Drawing.Drawing2D;
using Drawing.Models;
using Drawing.ViewServices;
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
            var pen = new Pen(Color.Black, 0.1f);

            var skyBrush = new SolidBrush(Color.LightSkyBlue);
            painter.DrawRectangle(pen, skyBrush,
                new RectangleModel { X = -5f, Y = -1f, Width = 19f, Height = 12f });

            var sunBrush = new SolidBrush(Color.Yellow);
            painter.DrawEllipse(pen, sunBrush,
                new RectangleModel { X = 10f, Y = 7f, Width = 2f, Height = 2f });

            var cloudBrush = new SolidBrush(Color.White);
            painter.DrawEllipse(pen, cloudBrush, new RectangleModel { X = -2f, Y = 8f, Width = 2f, Height = 1f });
            painter.DrawEllipse(pen, cloudBrush, new RectangleModel { X = -1f, Y = 8.5f, Width = 2f, Height = 1f });

            painter.DrawEllipse(pen, cloudBrush, new RectangleModel { X = 5f, Y = 9f, Width = 2f, Height = 1f });
            painter.DrawEllipse(pen, cloudBrush, new RectangleModel { X = 6f, Y = 9.5f, Width = 2f, Height = 1f });

            var houseBrush1 = new SolidBrush(Color.Gray);
            painter.DrawRectangle(pen, houseBrush1,
                new RectangleModel { X = -4f, Y = 1f, Width = 3f, Height = 6f });

            var windowBrush = new SolidBrush(Color.LightYellow);

            for (float y = 5.5f; y >= 1.5f; y -= 1.5f)
            {
                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = -3.5f, Y = y, Width = 0.8f, Height = 0.8f });

                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = -2.3f, Y = y, Width = 0.8f, Height = 0.8f });
            }

            var doorBrush = new SolidBrush(Color.Brown);
            painter.DrawRectangle(pen, doorBrush,
                new RectangleModel { X = -3f, Y = 1f, Width = 1f, Height = 1f });

            painter.DrawRectangle(pen, windowBrush,
                new RectangleModel { X = -3.5f, Y = 5.5f, Width = 0.8f, Height = 0.8f });
            painter.DrawRectangle(pen, windowBrush,
                new RectangleModel { X = -2.3f, Y = 5.5f, Width = 0.8f, Height = 0.8f });

            var houseBrush2 = new SolidBrush(Color.DarkGray);
            painter.DrawRectangle(pen, houseBrush2,
                new RectangleModel { X = 0f, Y = 1f, Width = 3f, Height = 7f });

            for (float y = 7f; y >= 1.5f; y -= 1.5f)
            {
                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = 0.5f, Y = y, Width = 0.8f, Height = 0.8f });

                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = 1.7f, Y = y, Width = 0.8f, Height = 0.8f });
            }

            painter.DrawRectangle(pen, doorBrush,
                new RectangleModel { X = 1f, Y = 1f, Width= 1f, Height = 1f });

            painter.DrawRectangle(pen, windowBrush,
                new RectangleModel { X = 0.5f, Y = 5.5f, Width = 0.8f, Height = 0.8f });
            painter.DrawRectangle(pen, windowBrush,
                new RectangleModel { X = 1.7f, Y = 5.5f, Width = 0.8f, Height = 0.8f });

            var houseBrush3 = new SolidBrush(Color.SlateGray);
            painter.DrawRectangle(pen, houseBrush3,
                new RectangleModel { X = 4f, Y = 1f, Width = 3f, Height = 4f });

            for (float y = 3.5f; y >= 2f; y -= 1.5f)
            {
                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = 4.5f, Y = y, Width = 0.8f, Height = 0.8f });

                painter.DrawRectangle(pen, windowBrush,
                    new RectangleModel { X = 5.7f, Y = y, Width = 0.8f, Height = 0.8f });
            }

            painter.DrawRectangle(pen, doorBrush,
                new RectangleModel { X = 5f, Y = 1f, Width = 0.8f, Height = 0.8f });

            var trunkBrush = new SolidBrush(Color.SaddleBrown);
            painter.DrawRectangle(pen, trunkBrush,
                new RectangleModel { X = 8.5f, Y = 1f, Width = 0.7f, Height = 3f });

            var leavesBrush = new SolidBrush(Color.Green);
            painter.DrawEllipse(pen, leavesBrush,
                new RectangleModel { X = 8f, Y = 3f, Width = 2f, Height = 2f });

            var roadBrush = new SolidBrush(Color.DarkGray);
            painter.DrawRectangle(pen, roadBrush,
                new RectangleModel { X = -5f, Y = -1f, Width = 19f, Height = 2f });

            var linePen = new Pen(Color.White, 0.15f);
            for (int i = -4; i < 14; i += 2)
            {
                painter.DrawEllipse(linePen, Brushes.White,
                    new RectangleModel { X = i, Y = -0.2f, Width = 1f, Height = 0.2f });
            }
        }
    }
}