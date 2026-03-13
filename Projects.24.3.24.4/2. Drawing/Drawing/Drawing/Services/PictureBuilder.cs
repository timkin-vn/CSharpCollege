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
            var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = Brushes.LightGray;

            var rect = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2, };
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 2;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 5;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            rect.X = 8;
            painter.DrawEllipse(mainPen, wheelBrush, rect);

            var bodyBrush = Brushes.DarkGray;

            rect = new RectangleModel { X = 0, Y = 2, Width = 12, Height = 4, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            rect = new RectangleModel { X = -4, Y = 2, Width = 4, Height = 8, };
            painter.DrawRectangle(mainPen, bodyBrush, rect);

            var windowBrush = Brushes.LightSkyBlue;

            rect = new RectangleModel { X = -2, Y = 6, Width = 1.5, Height = 3, };
            painter.DrawRectangle(mainPen, windowBrush, rect);

            rect = new RectangleModel { X = 11, Y = 2, Width = 2, Height = 4, };
            painter.DrawPie(mainPen, bodyBrush, rect, 270, 180);

            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 12, Y = 2, },
                new PointModel { X = 13, Y = 0.5, },
                new PointModel { X = 11, Y = 0.5, },
            };

            painter.DrawPolygon(mainPen, sweeperBrush, sweeperPoints);

            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };

            painter.DrawPolygon(mainPen, bodyBrush, pipePoints);
        }
    }
}
