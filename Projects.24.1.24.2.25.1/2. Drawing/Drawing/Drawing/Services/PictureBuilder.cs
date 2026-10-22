using Drawing.Models;
using System.Drawing;

namespace Drawing.Services
{
    public enum PictureType
    {
        Car,
        House,
        Ship
    }

    internal class PictureBuilder
    {
        public PictureType CurrentPicture { get; set; } = PictureType.Car;

        public RectangleModel SourceBounds { get; private set; } = new RectangleModel();

        public void PreparePicture()
        {
            switch (CurrentPicture)
            {
                case PictureType.Car:
                    SourceBounds = new RectangleModel { X = -2, Y = -3, Width = 14, Height = 12 };
                    break;
                case PictureType.House:
                    SourceBounds = new RectangleModel { X = -3, Y = -3, Width = 16, Height = 18 };
                    break;
                case PictureType.Ship:
                    SourceBounds = new RectangleModel { X = -3, Y = -3, Width = 16, Height = 16 };
                    break;
            }
        }

        public void DrawPicture(Painter painter)
        {

            switch (CurrentPicture)
            {
                case PictureType.Car:
                    DrawCar(painter);
                    break;
                case PictureType.House:
                    DrawHouse(painter);
                    break;
                case PictureType.Ship:
                    DrawShip(painter);
                    break;
            }
        }

        private void DrawCar(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = Brushes.DarkSlateGray;
            var bodyBrush = Brushes.Crimson;

            var rect = new RectangleModel { X = 0, Y = 1, Width = 10, Height = 3 };
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = 2, Y = 4, Width = 5, Height = 2.5 };
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = 2.5, Y = 4.5, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(Brushes.LightSkyBlue, mainPen, rect);

            rect = new RectangleModel { X = 4.5, Y = 4.5, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(Brushes.LightSkyBlue, mainPen, rect);

            rect = new RectangleModel { X = 1.5, Y = -1, Width = 2, Height = 2 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect = new RectangleModel { X = 6.5, Y = -1, Width = 2, Height = 2 };
            painter.DrawEllipse(wheelBrush, mainPen, rect);

            rect = new RectangleModel { X = 9.5, Y = 2, Width = 0.5, Height = 1 };
            painter.DrawRectangle(Brushes.Yellow, mainPen, rect);
        }

        private void DrawHouse(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            var wallBrush = Brushes.Beige;
            var roofBrush = Brushes.Brown;

            var rect = new RectangleModel { X = 0, Y = 0, Width = 10, Height = 8 };
            painter.DrawRectangle(wallBrush, mainPen, rect);

            var roofPoints = new[]
            {
                new PointModel { X = -1, Y = 8 },
                new PointModel { X = 11, Y = 8 },
                new PointModel { X = 5, Y = 13 }
            };
            painter.DrawPolygon(roofBrush, mainPen, roofPoints);

            rect = new RectangleModel { X = 4, Y = 0, Width = 2, Height = 4 };
            painter.DrawRectangle(Brushes.SaddleBrown, mainPen, rect);

            rect = new RectangleModel { X = 1, Y = 4, Width = 2, Height = 2 };
            painter.DrawRectangle(Brushes.LightSkyBlue, mainPen, rect);

            rect = new RectangleModel { X = 7, Y = 4, Width = 2, Height = 2 };
            painter.DrawRectangle(Brushes.LightSkyBlue, mainPen, rect);

            rect = new RectangleModel { X = 7, Y = 10, Width = 1.5, Height = 2 };
            painter.DrawRectangle(Brushes.DarkRed, mainPen, rect);
        }

        private void DrawShip(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 3);
            var hullBrush = Brushes.SaddleBrown;

            var hullPoints = new[]
            {
                new PointModel { X = 0, Y = 0 },
                new PointModel { X = 10, Y = 0 },
                new PointModel { X = 8, Y = 3 },
                new PointModel { X = 2, Y = 3 },
            };
            painter.DrawPolygon(hullBrush, mainPen, hullPoints);

            var mastPoints = new[]
            {
                new PointModel { X = 4.8, Y = 3 },
                new PointModel { X = 5.2, Y = 3 },
                new PointModel { X = 5.2, Y = 11 },
                new PointModel { X = 4.8, Y = 11 },
            };
            painter.DrawPolygon(Brushes.DarkGray, mainPen, mastPoints);

            var sail1Points = new[]
            {
                new PointModel { X = 5.2, Y = 3 },
                new PointModel { X = 5.2, Y = 10.5 },
                new PointModel { X = 9, Y = 3 },
            };
            painter.DrawPolygon(Brushes.White, mainPen, sail1Points);

            var sail2Points = new[]
            {
                new PointModel { X = 4.8, Y = 3 },
                new PointModel { X = 4.8, Y = 10 },
                new PointModel { X = 1.5, Y = 3 },
            };
            painter.DrawPolygon(Brushes.WhiteSmoke, mainPen, sail2Points);

            var flagPoints = new[]
            {
                new PointModel { X = 5.2, Y = 11 },
                new PointModel { X = 7, Y = 10.5 },
                new PointModel { X = 5.2, Y = 10 },
            };
            painter.DrawPolygon(Brushes.Red, mainPen, flagPoints);
        }
    }
}