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
            var wheelBrush = Brushes.LightGray;

            var wheelRectangle = new RectangleModel { X = -1, Y = 0, Width = 2, Height = 2, };
            painter.DrawEllipse(wheelBrush, mainPen, wheelRectangle);

            wheelRectangle.X += 3;
            painter.DrawEllipse(wheelBrush, mainPen, wheelRectangle);

            wheelRectangle.X += 3;
            painter.DrawEllipse(wheelBrush, mainPen, wheelRectangle);

            wheelRectangle.X += 3;
            painter.DrawEllipse(wheelBrush, mainPen, wheelRectangle);

            var bodyBrush = Brushes.DarkGray;
            var bodyRectangle = new RectangleModel { X = 0, Y = 2, Width = 11, Height = 4, };
            painter.DrawRectangle(bodyBrush, mainPen, bodyRectangle);

            bodyRectangle = new RectangleModel { X = -4, Y = 2, Width = 4, Height = 8, };
            painter.DrawRectangle(bodyBrush, mainPen, bodyRectangle);

            var glassBrush = Brushes.LightSkyBlue;
            var glassRectangle = new RectangleModel { X = -2, Y = 6, Width = 1.5, Height = 3, };
            painter.DrawRectangle(glassBrush, mainPen, glassRectangle);

            var headRectangle = new RectangleModel { X = 10, Y = 2, Width = 2, Height = 4, };
            painter.DrawPie(bodyBrush, mainPen, headRectangle, 270, 180);

            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 10.5, Y = 2, },
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 11.5, Y = 0.5, },
                new PointModel { X = 10.5, Y = 0.5, },
            };
            painter.DrawPolygon(sweeperBrush, mainPen, sweeperPoints);

            var tubePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };
            painter.DrawPolygon(bodyBrush, mainPen, tubePoints);
        }
    }
}
