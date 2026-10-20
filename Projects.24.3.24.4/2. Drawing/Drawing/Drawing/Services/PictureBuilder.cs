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
                Width = 20,
                Height = 16,
            };
        }

        public void DrawPicture(Painter painter)
        {
            switch (_pictureNumber)
            {
                case 1:
                    DrawRobot(painter);
                    break;
                case 2:
                    DrawSnowman(painter);
                    break;
                case 3:
                    DrawFlower(painter);
                    break;
                default:
                    DrawTrafficLight(painter);
                    break;
            }
        }

        private void DrawRobot(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.LightGray, new RectangleModel { X = 2, Y = 5, Width = 6, Height = 5 });
            painter.DrawRectangle(pen, Brushes.Gray, new RectangleModel { X = 2.5, Y = 1, Width = 5, Height = 4 });
            painter.DrawEllipse(pen, Brushes.White, new RectangleModel { X = 3, Y = 7, Width = 1.2, Height = 1.2 });
            painter.DrawEllipse(pen, Brushes.White, new RectangleModel { X = 5.8, Y = 7, Width = 1.2, Height = 1.2 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 3.5, Y = 3, Width = 3, Height = 0.6 });
            painter.DrawRectangle(pen, Brushes.DarkGray, new RectangleModel { X = 0.5, Y = 6.5, Width = 1.5, Height = 0.6 });
            painter.DrawRectangle(pen, Brushes.DarkGray, new RectangleModel { X = 8, Y = 6.5, Width = 1.5, Height = 0.6 });
            painter.DrawRectangle(pen, Brushes.DarkGray, new RectangleModel { X = 3, Y = -0.5, Width = 0.7, Height = 1.5 });
            painter.DrawRectangle(pen, Brushes.DarkGray, new RectangleModel { X = 6.3, Y = -0.5, Width = 0.7, Height = 1.5 });
        }

        private void DrawSnowman(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 2, Y = 0, Width = 6, Height = 5 });
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 2.7, Y = 4.2, Width = 4.6, Height = 4.2 });
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 3.3, Y = 8, Width = 3.4, Height = 3 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 3.7, Y = 11, Width = 2.6, Height = 0.6 });
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 4.2, Y = 11.6, Width = 1.6, Height = 1.4 });
            painter.DrawPolygon(pen, Brushes.Orange, new[]
            {
                new PointModel { X = 5, Y = 9.2 },
                new PointModel { X = 7.5, Y = 8.8 },
                new PointModel { X = 5, Y = 8.6 },
            });
        }

        private void DrawFlower(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.Green, new RectangleModel { X = 4.8, Y = 0, Width = 0.4, Height = 7 });
            painter.DrawEllipse(pen, Brushes.Yellow, new RectangleModel { X = 4, Y = 7, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.DeepPink, new RectangleModel { X = 4, Y = 9, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.DeepPink, new RectangleModel { X = 4, Y = 5, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.DeepPink, new RectangleModel { X = 2, Y = 7, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.DeepPink, new RectangleModel { X = 6, Y = 7, Width = 2, Height = 2 });
            painter.DrawPolygon(pen, Brushes.LightGreen, new[]
            {
                new PointModel { X = 5, Y = 4.5 },
                new PointModel { X = 7, Y = 5.5 },
                new PointModel { X = 5, Y = 6.2 },
            });
            painter.DrawPolygon(pen, Brushes.LightGreen, new[]
            {
                new PointModel { X = 5, Y = 3.8 },
                new PointModel { X = 3, Y = 4.8 },
                new PointModel { X = 5, Y = 5.4 },
            });
        }

        private void DrawTrafficLight(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            painter.DrawRectangle(pen, Brushes.DarkSlateGray, new RectangleModel { X = 3, Y = 4, Width = 4, Height = 9 });
            painter.DrawEllipse(pen, Brushes.Red, new RectangleModel { X = 4, Y = 10.5, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.Gold, new RectangleModel { X = 4, Y = 7.5, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.LimeGreen, new RectangleModel { X = 4, Y = 4.5, Width = 2, Height = 2 });
            painter.DrawRectangle(pen, Brushes.Gray, new RectangleModel { X = 4.7, Y = 0, Width = 0.6, Height = 4 });
        }
    }
}
