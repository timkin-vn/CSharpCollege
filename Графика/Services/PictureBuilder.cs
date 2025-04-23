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
            var mainPen = new Pen(Color.Black, 3);
            var bodyBrush = Brushes.DarkGray;
            var eyeBrush = Brushes.PaleGoldenrod;
            var pupilBrush = Brushes.Black;
            var noseBrush = Brushes.Brown;
            var earBrush = Brushes.Gray;
            var earHoleBrush = Brushes.HotPink;

            var rect = new RectangleModel { X = 0.25, Y = 2.75, Width = 2, Height = 2 };
            painter.DrawEllipse(bodyBrush, mainPen, rect);
            rect = new RectangleModel { X = 3.45, Y = 1.7, Width = 2, Height = 2 };
            painter.DrawEllipse(bodyBrush, mainPen, rect);
            rect = new RectangleModel { X = 6.25, Y = 2.8, Width = 2, Height = 2 };
            painter.DrawEllipse(bodyBrush, mainPen, rect);

            var tailPoints = new[]
{
                new PointModel { X = -1.75, Y = 9.25 },
                new PointModel { X = -1.7, Y = 9.75 },
                new PointModel { X = -1.5, Y = 10.25 },
                new PointModel { X = -1, Y = 10.15 },
                new PointModel { X = -0.5, Y = 10 },
                new PointModel { X = -0, Y = 9.75 },
                new PointModel { X = 1.75, Y = 7.15 },
                new PointModel { X = 1, Y = 6.6 }
            };
            painter.DrawPolygon(bodyBrush, mainPen, tailPoints);

            rect = new RectangleModel { X = 0.25, Y = 2.75, Width = 8, Height = 5 };
            painter.DrawEllipse(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = 3, Y = 4.5, Width = 6, Height = 4.5 };
            painter.DrawEllipse(bodyBrush, mainPen, rect);

            var earPoints = new[]
            {
                new PointModel { X = 3.6, Y = 8 },
                new PointModel { X = 4, Y = 10 },
                new PointModel { X = 5.6, Y = 8 }
            };
            painter.DrawPolygon(earBrush, mainPen, earPoints);

            earPoints = new[]
            {
                new PointModel { X = 6.4, Y = 8 },
                new PointModel { X = 8, Y = 10 },
                new PointModel { X = 8.4, Y = 8 }
            };
            painter.DrawPolygon(earBrush, mainPen, earPoints);

            var earHolePoints = new[]
{
                new PointModel { X = 3.85, Y = 8.2 },
                new PointModel { X = 4.1, Y = 9.6 },
                new PointModel { X = 5.2, Y = 8.2 }
            };
            painter.DrawPolygon(earHoleBrush, mainPen, earHolePoints);

            earHolePoints = new[]
            {
                new PointModel { X = 6.8, Y = 8.2 },
                new PointModel { X = 7.9, Y = 9.6 },
                new PointModel { X = 8.15, Y = 8.2 }
            };
            painter.DrawPolygon(earHoleBrush, mainPen, earHolePoints);

            rect = new RectangleModel { X = 3.8, Y = 6.65, Width = 1.5, Height = 1 };
            painter.DrawEllipse(eyeBrush, mainPen, rect);
            rect.X = 6.8;
            painter.DrawEllipse(eyeBrush, mainPen, rect);

            rect = new RectangleModel { X = 4, Y = 6.9, Width = 0.8, Height = 0.6 };
            painter.DrawEllipse(pupilBrush, mainPen, rect);
            rect.X = 7;
            painter.DrawEllipse(pupilBrush, mainPen, rect);

            painter.DrawLine(mainPen, new PointModel { X = 2.5, Y = 6.6 }, new PointModel { X = 6, Y = 6.1 });
            painter.DrawLine(mainPen, new PointModel { X = 2.25, Y = 6.1 }, new PointModel { X = 6, Y = 6.1 });
            painter.DrawLine(mainPen, new PointModel { X = 2.5, Y = 5.6 }, new PointModel { X = 6, Y = 6.1 });
            painter.DrawLine(mainPen, new PointModel { X = 9.5, Y = 6.6 }, new PointModel { X = 6, Y = 6.1 });
            painter.DrawLine(mainPen, new PointModel { X = 9.75, Y = 6.1 }, new PointModel { X = 6, Y = 6.1 });
            painter.DrawLine(mainPen, new PointModel { X = 9.5, Y = 5.6 }, new PointModel { X = 6, Y = 6.1 });

            var nosePoints = new[]
            {
                new PointModel { X = 5.5, Y = 6.5 },
                new PointModel { X = 6, Y = 6 },
                new PointModel { X = 6.5, Y = 6.5 }
            };
            painter.DrawPolygon(noseBrush, mainPen, nosePoints);

            painter.DrawLine(mainPen, new PointModel { X = 5, Y = 5.75 }, new PointModel { X = 5.5, Y = 5.25 });
            painter.DrawLine(mainPen, new PointModel { X = 5.5, Y = 5.25 }, new PointModel { X = 6, Y = 5.75 });
            painter.DrawLine(mainPen, new PointModel { X = 6, Y = 5.75 }, new PointModel { X = 6, Y = 6 });
            painter.DrawLine(mainPen, new PointModel { X = 6, Y = 5.75 }, new PointModel { X = 6.5, Y = 5.25 });
            painter.DrawLine(mainPen, new PointModel { X = 6.5, Y = 5.25 }, new PointModel { X = 7, Y = 5.75 });
        }
    }
}