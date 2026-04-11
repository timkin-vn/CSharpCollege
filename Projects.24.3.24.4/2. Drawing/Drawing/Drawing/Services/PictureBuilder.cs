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
                X = -5,
                Y = -1,
                Width = 20,
                Height = 16,
            };
        }

        public void DrawPicture(Painter painter)
        {
            switch (_pictureNumber)
            {
                case 1:
                    DrawHouse(painter);
                    break;
                case 2:
                    DrawTree(painter);
                    break;
                case 3:
                    DrawRocket(painter);
                    break;
                default:
                    DrawBoat(painter);
                    break;
            }
        }

        private void DrawHouse(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.Bisque, new RectangleModel { X = 0, Y = 0, Width = 8, Height = 6 });
            painter.DrawPolygon(pen, Brushes.Firebrick, new[]
            {
                new PointModel { X = -1, Y = 6 },
                new PointModel { X = 4, Y = 10 },
                new PointModel { X = 9, Y = 6 },
            });
            painter.DrawRectangle(pen, Brushes.SaddleBrown, new RectangleModel { X = 3, Y = 0, Width = 2, Height = 3 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 1, Y = 3, Width = 2, Height = 2 });
            painter.DrawRectangle(pen, Brushes.LightSkyBlue, new RectangleModel { X = 5, Y = 3, Width = 2, Height = 2 });
        }

        private void DrawTree(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.SaddleBrown, new RectangleModel { X = 3.5, Y = 0, Width = 2, Height = 5 });
            painter.DrawEllipse(pen, Brushes.ForestGreen, new RectangleModel { X = 1, Y = 4, Width = 7, Height = 5 });
            painter.DrawEllipse(pen, Brushes.ForestGreen, new RectangleModel { X = 0, Y = 7, Width = 5, Height = 4 });
            painter.DrawEllipse(pen, Brushes.ForestGreen, new RectangleModel { X = 4, Y = 7, Width = 5, Height = 4 });
        }

        private void DrawRocket(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = 3, Y = 2, Width = 4, Height = 8 });
            painter.DrawPolygon(pen, Brushes.Red, new[]
            {
                new PointModel { X = 3, Y = 10 },
                new PointModel { X = 5, Y = 13 },
                new PointModel { X = 7, Y = 10 },
            });
            painter.DrawPolygon(pen, Brushes.OrangeRed, new[]
            {
                new PointModel { X = 3, Y = 2 },
                new PointModel { X = 2, Y = 0 },
                new PointModel { X = 3.5, Y = 2 },
            });
            painter.DrawPolygon(pen, Brushes.OrangeRed, new[]
            {
                new PointModel { X = 7, Y = 2 },
                new PointModel { X = 8, Y = 0 },
                new PointModel { X = 6.5, Y = 2 },
            });
            painter.DrawEllipse(pen, Brushes.SkyBlue, new RectangleModel { X = 4, Y = 6, Width = 2, Height = 2 });
        }

        private void DrawBoat(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawPolygon(pen, Brushes.Peru, new[]
            {
                new PointModel { X = 0, Y = 2 },
                new PointModel { X = 10, Y = 2 },
                new PointModel { X = 8, Y = 0 },
                new PointModel { X = 2, Y = 0 },
            });
            painter.DrawRectangle(pen, Brushes.SaddleBrown, new RectangleModel { X = 4.8, Y = 2, Width = 0.4, Height = 8 });
            painter.DrawPolygon(pen, Brushes.White, new[]
            {
                new PointModel { X = 5, Y = 9.5 },
                new PointModel { X = 5, Y = 3 },
                new PointModel { X = 9, Y = 6.2 },
            });
            painter.DrawRectangle(null, Brushes.LightBlue, new RectangleModel { X = -5, Y = -1, Width = 20, Height = 1 });
        }
    }
}
