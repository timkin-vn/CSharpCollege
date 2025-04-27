using Drawing.Models;
using System;
using System.Drawing;
using System.Linq;
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
            
            var backgroundBrush = Brushes.SkyBlue; 
            var houseBrush = Brushes.BurlyWood;
            var roofBrush = Brushes.SaddleBrown;
            var sunBrush = Brushes.Gold;
            var grassPen = new Pen(Color.Green, 2);
            var cloudBrush = Brushes.WhiteSmoke;

            
            var thickPen = new Pen(Color.Black, 4);
            var thinPen = new Pen(Color.DarkSlateGray, 1);

           
            var fullRect = new RectangleModel
            {
                X = SourceBounds.X,
                Y = SourceBounds.Y,
                Width = SourceBounds.Width,
                Height = SourceBounds.Height,
            };
            painter.DrawRectangle(backgroundBrush, null, fullRect);

          
            for (double x = -5; x <= 14; x += 0.5)
            {
                var start = new PointModel { X = x, Y = -1 };
                var end = new PointModel { X = x, Y = -0.5 + 0.2 * Math.Sin(x * 2) };
                painter.DrawLine(grassPen, start, end);
            }

            
            var houseRect = new RectangleModel { X = 1, Y = 0, Width = 6, Height = 3.5 };
            painter.DrawRectangle(houseBrush, thickPen, houseRect);

           
            var roofPoints = new[]
            {
        new PointModel { X = 1, Y = 3.5 },
        new PointModel { X = 4, Y = 6 },
        new PointModel { X = 7, Y = 3.5 },
    };
            painter.DrawPolygon(roofBrush, thickPen, roofPoints);

            
            var doorRect = new RectangleModel { X = 3, Y = 0, Width = 1.2, Height = 2 };
            painter.DrawRectangle(Brushes.Sienna, thickPen, doorRect);

          
            var window1 = new RectangleModel { X = 1.5, Y = 1.5, Width = 1, Height = 1 };
            var window2 = new RectangleModel { X = 5.5, Y = 1.5, Width = 1, Height = 1 };
            painter.DrawRectangle(Brushes.LightSkyBlue, thinPen, window1);
            painter.DrawRectangle(Brushes.LightSkyBlue, thinPen, window2);

           
            var sunRect = new RectangleModel { X = 10, Y = 8, Width = 2.5, Height = 2.5 };
            painter.DrawEllipse(sunBrush, thinPen, sunRect);

            for (int i = 0; i < 8; i++)
            {
                double angle = Math.PI / 4 * i;
                var start = new PointModel
                {
                    X = 11.25 + 1.25 * Math.Cos(angle),
                    Y = 9.25 + 1.25 * Math.Sin(angle)
                };
                var end = new PointModel
                {
                    X = 11.25 + 2 * Math.Cos(angle),
                    Y = 9.25 + 2 * Math.Sin(angle)
                };
                painter.DrawLine(thinPen, start, end);
            }
 
            DrawCloud(painter, cloudBrush, new PointModel { X = -3, Y = 8 });
            DrawCloud(painter, cloudBrush, new PointModel { X = 3, Y = 9 });
            DrawCloud(painter, cloudBrush, new PointModel { X = 7, Y = 7.5 });
        }

        private void DrawCloud(Painter painter, Brush cloudBrush, PointModel center)
        {
            double size = 1.2;
            var offsets = new[]
            {
                new PointModel { X = 0, Y = 0 },
                new PointModel { X = -0.8, Y = 0.3 },
                new PointModel { X = 0.8, Y = 0.3 },
                new PointModel { X = -0.5, Y = -0.3 },
                new PointModel { X = 0.5, Y = -0.3 },
            };

            foreach (var offset in offsets)
            {
                var rect = new RectangleModel
                {
                    X = center.X + offset.X,
                    Y = center.Y + offset.Y,
                    Width = size,
                    Height = size * 0.7,
                };
                painter.DrawEllipse(cloudBrush, null, rect);
            }
        }
    }
}