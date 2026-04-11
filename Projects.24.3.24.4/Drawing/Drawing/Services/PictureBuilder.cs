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
                    DrawSnowman(painter);
                    break;
                case 2:
                    DrawTruck(painter);
                    break;
                case 3:
                    DrawTrafficLight(painter);
                    break;
                default:
                    DrawSun(painter);
                    break;
            }
        }

        private void DrawSnowman(Painter painter)
        {
            var pen = new Pen(Color.Black, 2);
          
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 2, Y = 0, Width = 6, Height = 5 });
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 3, Y = 4.5, Width = 4, Height = 4 });
            painter.DrawEllipse(pen, Brushes.WhiteSmoke, new RectangleModel { X = 3.5, Y = 8, Width = 3, Height = 3 });

            
            painter.DrawPolygon(pen, Brushes.Orange, new[]
            {
                new PointModel { X = 5, Y = 9.5 },
                new PointModel { X = 7, Y = 9.2 },
                new PointModel { X = 5, Y = 9 }
            });

            
            painter.DrawPolygon(pen, Brushes.DarkSlateGray, new[]
            {
                new PointModel { X = 4, Y = 10.5 },
                new PointModel { X = 6, Y = 10.5 },
                new PointModel { X = 5.8, Y = 12 },
                new PointModel { X = 4.2, Y = 12 }
            });
        }

        private void DrawTruck(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
            
            painter.DrawRectangle(pen, Brushes.SteelBlue, new RectangleModel { X = 0, Y = 2, Width = 7, Height = 5 });
           
            painter.DrawRectangle(pen, Brushes.Firebrick, new RectangleModel { X = 7, Y = 2, Width = 3, Height = 4 });
            
            painter.DrawRectangle(pen, Brushes.LightCyan, new RectangleModel { X = 8, Y = 4, Width = 1.5, Height = 1.5 });
           
            painter.DrawEllipse(pen, Brushes.Black, new RectangleModel { X = 1, Y = 1, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.Black, new RectangleModel { X = 4, Y = 1, Width = 2, Height = 2 });
            painter.DrawEllipse(pen, Brushes.Black, new RectangleModel { X = 7.5, Y = 1, Width = 2, Height = 2 });
        }

        private void DrawTrafficLight(Painter painter)
        {
            var pen = new Pen(Color.Black, 3);
           
            painter.DrawRectangle(pen, Brushes.DimGray, new RectangleModel { X = 3.5, Y = 2, Width = 3, Height = 9 });
            
            painter.DrawRectangle(pen, Brushes.Black, new RectangleModel { X = 4.7, Y = 0, Width = 0.6, Height = 2 });

          
            painter.DrawEllipse(pen, Brushes.Red, new RectangleModel { X = 4.2, Y = 8.2, Width = 1.6, Height = 1.6 });
            painter.DrawEllipse(pen, Brushes.Yellow, new RectangleModel { X = 4.2, Y = 5.7, Width = 1.6, Height = 1.6 });
            painter.DrawEllipse(pen, Brushes.LimeGreen, new RectangleModel { X = 4.2, Y = 3.2, Width = 1.6, Height = 1.6 });
        }

        private void DrawSun(Painter painter)
        {
            var pen = new Pen(Color.Gold, 2);
            
            painter.DrawRectangle(null, Brushes.DeepSkyBlue, new RectangleModel { X = -5, Y = -1, Width = 20, Height = 16 });
           
            painter.DrawEllipse(pen, Brushes.Yellow, new RectangleModel { X = 10, Y = 10, Width = 4, Height = 4 });
        }
    }
}
