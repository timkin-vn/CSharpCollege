using Drawing.Models;
using Drawing.ViewServices;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        private readonly int _pictureNumber;

        public RectangleModel SourceBounds { get; }

        public PictureBuilder(int pictureNumber)
        {
            _pictureNumber = pictureNumber;
            SourceBounds = new RectangleModel
            {
                X = -6,
                Y = -1,
                Width = 22,
                Height = 16,
            };
        }

        public void DrawPicture(Painter painter)
        {
            switch (_pictureNumber)
            {
                case 1:
                    DrawCastle(painter);
                    break;
                case 2:
                    DrawCatFace(painter);
                    break;
                case 3:
                    DrawDumbbell(painter);
                    break;
                default:
                    DrawBus(painter);
                    break;
            }
        }

        private void DrawCastle(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = 1, Y = 0, Width = 10, Height = 7 });
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = -1, Y = 4, Width = 2, Height = 6 });
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = 11, Y = 4, Width = 2, Height = 6 });
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = 4.2, Y = 7, Width = 3.6, Height = 4 });
            painter.DrawRectangle(pen, Brushes.SaddleBrown, new RectangleModel { X = 5, Y = 0, Width = 2, Height = 4 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 2, Y = 3, Width = 1.5, Height = 2 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 8.5, Y = 3, Width = 1.5, Height = 2 });
        }

        private void DrawCatFace(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawEllipse(pen, Brushes.Goldenrod, new RectangleModel { X = 2, Y = 2, Width = 8, Height = 8 });
            painter.DrawPolygon(pen, Brushes.Goldenrod, new[]
            {
                new PointModel { X = 3, Y = 9 },
                new PointModel { X = 4.5, Y = 12 },
                new PointModel { X = 5.5, Y = 9.2 },
            });
            painter.DrawPolygon(pen, Brushes.Goldenrod, new[]
            {
                new PointModel { X = 8.5, Y = 9.2 },
                new PointModel { X = 9.5, Y = 12 },
                new PointModel { X = 11, Y = 9 },
            });
            painter.DrawEllipse(pen, Brushes.White, new RectangleModel { X = 4, Y = 7, Width = 1.2, Height = 1.2 });
            painter.DrawEllipse(pen, Brushes.White, new RectangleModel { X = 6.8, Y = 7, Width = 1.2, Height = 1.2 });
            painter.DrawPolygon(pen, Brushes.Pink, new[]
            {
                new PointModel { X = 6, Y = 6.1 },
                new PointModel { X = 5.3, Y = 5.3 },
                new PointModel { X = 6.7, Y = 5.3 },
            });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 2, Y = 5.3, Width = 2.5, Height = 0.1 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 7.5, Y = 5.3, Width = 2.5, Height = 0.1 });
        }

        private void DrawDumbbell(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.DarkGray, new RectangleModel { X = 2, Y = 4.2, Width = 8, Height = 1.1 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 0.5, Y = 3.3, Width = 1, Height = 3 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 1.7, Y = 3.6, Width = 0.8, Height = 2.4 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 9.5, Y = 3.6, Width = 0.8, Height = 2.4 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 10.7, Y = 3.3, Width = 1, Height = 3 });
        }

        private void DrawBus(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.Orange, new RectangleModel { X = 0, Y = 2, Width = 12, Height = 5 });
            painter.DrawEllipse(pen, Brushes.Gray, new RectangleModel { X = 2, Y = 0, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.Gray, new RectangleModel { X = 8, Y = 0, Width = 2, Height = 2 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 1, Y = 4.2, Width = 2, Height = 1.8 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 3.4, Y = 4.2, Width = 2, Height = 1.8 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 5.8, Y = 4.2, Width = 2, Height = 1.8 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 8.2, Y = 4.2, Width = 2, Height = 1.8 });
            painter.DrawRectangle(pen, Brushes.LightYellow, new RectangleModel { X = 10.5, Y = 3, Width = 1, Height = 3 });
        }
    }
}
