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
            X = -9,
            Y = -7,
            Width = 20,
            Height = 17
        };

        public void DrawPicture(Painter painter)
        {
            var firstPen = new Pen(Color.Black, 2);
            var secondPen = new Pen(Color.Black, 6);
            var thirdPen = new Pen(Color.DarkGreen, 6);

            var baseBrush = new SolidBrush(Color.FromArgb(255, 206, 172, 0));
            var roofBrush = new SolidBrush(Color.FromArgb(255, 183, 0, 0));
            var windowBrush = Brushes.RoyalBlue;
            var brownBrush = Brushes.SaddleBrown;
            var treeBrush = Brushes.Green;
            var yellowBrush = Brushes.Yellow;

            var rect = new RectangleModel { X = -5, Y = -6, Width = 10, Height = 8 };

            painter.DrawRectangle(baseBrush, firstPen, rect);

            var sweeperPoints = new[]
            {
                 new PointModel { X = -6, Y = 2},
                 new PointModel { X = -2, Y = 6},
                 new PointModel { X = 2, Y = 6},
                 new PointModel { X = 6, Y = 2},
            };
            painter.DrawPolygon(roofBrush, firstPen, sweeperPoints);

            rect = new RectangleModel { X = -4, Y = -2, Width = 3, Height = 3 };
            painter.DrawEllipse(windowBrush, secondPen, rect);

            rect = new RectangleModel { X = 1, Y = -2, Width = 3, Height = 3 };
            painter.DrawEllipse(windowBrush, secondPen, rect);

            rect = new RectangleModel { X = -1, Y = -6, Width = 2, Height = 3 };
            painter.DrawRectangle(brownBrush, firstPen, rect);

            rect = new RectangleModel { X = 8, Y = -6, Width = 1, Height = 3 };
            painter.DrawRectangle(brownBrush, firstPen, rect);

            rect = new RectangleModel { X = 3, Y = 4, Width = 1, Height = 3 };
            painter.DrawRectangle(yellowBrush, firstPen, rect);

            rect = new RectangleModel { X = -7, Y = -6, Width = 2, Height = 2 };
            painter.DrawRectangle(brownBrush, firstPen, rect);

            rect = new RectangleModel { X = -9, Y = -6, Width = 2, Height = 1 };
            painter.DrawRectangle(brownBrush, firstPen, rect);

            var sweeperPointsTriangular = new[]
            {
                 new PointModel { X = 7, Y = -3},
                 new PointModel { X = 8.5, Y = 2},
                 new PointModel { X = 10, Y = -3},
            };
            painter.DrawTriangular(treeBrush, thirdPen, sweeperPointsTriangular);

            PointModel point1 = new PointModel { X = -4, Y = -0.5 };
            PointModel point2 = new PointModel { X = -1, Y = -0.5 }; 
            PointModel point3 = new PointModel { X = -2.5, Y = 1 };
            PointModel point4 = new PointModel { X = -2.5, Y = -2 }; 
            PointModel point5 = new PointModel { X = 1, Y = -0.5 };
            PointModel point6 = new PointModel { X = 4, Y = -0.5 }; 
            PointModel point7 = new PointModel { X = 2.5, Y = 1 };
            PointModel point8 = new PointModel { X = 2.5, Y = -2 }; 
            painter.DrawLine(secondPen, point1, point2);
            painter.DrawLine(secondPen, point3, point4);
            painter.DrawLine(secondPen, point5, point6);
            painter.DrawLine(secondPen, point7, point8);
        }
    }
}