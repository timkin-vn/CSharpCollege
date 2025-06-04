using Drawing.Models;
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
            var mainPen = new Pen(Color.Black, 3);
            var bodyBrush = Brushes.DarkKhaki;

            var rect = new RectangleModel { X = -1, Y = 1, Width = 9, Height = 6.5 };
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            var brushChimney = new HatchBrush(HatchStyle.ForwardDiagonal, Color.LightGray);
            var chimneyPen = new Pen(Color.Black, 3);

            rect = new RectangleModel { X = -0.2, Y = 8, Width = 1, Height = 2 };
            painter.DrawRectangle(brushChimney, chimneyPen, rect);

            var windowBrush = Brushes.LightSkyBlue;
            var penWindow = Brushes.Black;

            rect = new RectangleModel { X = -0.5, Y = 3, Width = 3, Height = 3, };
            painter.DrawRectangle(windowBrush, mainPen, rect);

            rect = new RectangleModel { X = 1, Y = 3, Width = 0.1, Height = 3, };
            painter.DrawRectangle(penWindow, mainPen, rect);
            rect = new RectangleModel { X = -0.5, Y = 4.4, Width = 3, Height = 0.1, };
            painter.DrawRectangle(penWindow, mainPen, rect);

            var RoofBrush = Brushes.Red;
            var RoofPoints = new[]
            {
                new PointModel { X = -1, Y = 7.5, },
                new PointModel { X = 8, Y = 7.5, },
                new PointModel { X = 3.5, Y = 10.5, },
            };

            var brushDoor = Brushes.DarkOliveGreen;
            var penDoor = new Pen(Color.Black, 2);
            rect = new RectangleModel { X = 3.5, Y = 1, Width = 2, Height = 3.5 };
            painter.DrawRectangle(brushDoor, penDoor, rect);

            var penDoorknob = new Pen(Color.Gray, 1);
            var brushDoorknob = Brushes.Black;
            rect = new RectangleModel { X = 5, Y = 2.5, Width = 0.3, Height = 0.3 };
            painter.DrawEllipse(brushDoorknob, penDoorknob, rect);

            painter.DrawPolygon(RoofBrush, mainPen, RoofPoints);

            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };
        }
    }
}
