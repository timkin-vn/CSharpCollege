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
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 2);

            var bodyBrush = Brushes.Orange;
            var earBrush = Brushes.Chocolate;
            var eyeBrush = Brushes.White;
            var pupilBrush = Brushes.Black;
            var noseBrush = Brushes.HotPink;
            var tailBrush = Brushes.DarkOrange;

            painter.DrawRectangle(bodyBrush, mainPen, new RectangleModel { X = 0, Y = 1, Width = 9, Height = 5 });

            painter.DrawEllipse(bodyBrush, mainPen, new RectangleModel { X = 7, Y = 5, Width = 5, Height = 4.5 });

            painter.DrawPolygon(earBrush, mainPen, new[] {
                new PointModel { X = 7.5, Y = 8.5 },
                new PointModel { X = 7.0, Y = 10.5 },
                new PointModel { X = 9.0, Y = 9.0 }
            });

            painter.DrawPolygon(earBrush, mainPen, new[] {
                new PointModel { X = 10.0, Y = 9.0 },
                new PointModel { X = 12.0, Y = 10.5 },
                new PointModel { X = 11.5, Y = 8.5 }
            });

            painter.DrawEllipse(eyeBrush, mainPen, new RectangleModel { X = 8.2, Y = 7.0, Width = 1.2, Height = 1.2 });
            painter.DrawEllipse(eyeBrush, mainPen, new RectangleModel { X = 10.2, Y = 7.0, Width = 1.2, Height = 1.2 });

            painter.DrawEllipse(pupilBrush, mainPen, new RectangleModel { X = 8.6, Y = 7.1, Width = 0.4, Height = 1.0 });
            painter.DrawEllipse(pupilBrush, mainPen, new RectangleModel { X = 10.6, Y = 7.1, Width = 0.4, Height = 1.0 });

            painter.DrawPolygon(noseBrush, mainPen, new[] {
                new PointModel { X = 9.2, Y = 6.5 },
                new PointModel { X = 10.2, Y = 6.5 },
                new PointModel { X = 9.7, Y = 5.8 }
            });

            painter.DrawPie(tailBrush, mainPen, new RectangleModel { X = -4, Y = 3, Width = 6, Height = 5 }, 90, 180);

            painter.DrawRectangle(bodyBrush, mainPen, new RectangleModel { X = 1, Y = -1, Width = 1.5, Height = 2 });
            painter.DrawRectangle(bodyBrush, mainPen, new RectangleModel { X = 6, Y = -1, Width = 1.5, Height = 2 });

            painter.DrawPolygon(bodyBrush, mainPen, new[] {
                new PointModel { X = 8.5, Y = 1.5 },
                new PointModel { X = 10.5, Y = 0.0 },
                new PointModel { X = 11.5, Y = 1.0 },
                new PointModel { X = 9.5, Y = 2.5 }
            });
        }
    }

}
