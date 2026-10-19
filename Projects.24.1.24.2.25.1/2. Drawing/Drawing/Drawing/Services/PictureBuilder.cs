using Drawing.Models;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -2,
            Y = -1,
            Width = 18,
            Height = 11,
        };

        public void DrawPicture(Painter painter)
        {
            var outline = new Pen(Color.FromArgb(35, 45, 55), 3);

            // Road
            painter.DrawRectangle(Brushes.Gainsboro, null,
                new RectangleModel { X = -2, Y = -1, Width = 18, Height = 0.35 });

            // Wheels and hubcaps
            painter.DrawEllipse(new SolidBrush(Color.FromArgb(35, 39, 45)), outline,
                new RectangleModel { X = 1, Y = 0, Width = 3.2, Height = 3.2 });
            painter.DrawEllipse(new SolidBrush(Color.FromArgb(35, 39, 45)), outline,
                new RectangleModel { X = 9.7, Y = 0, Width = 3.2, Height = 3.2 });
            painter.DrawEllipse(Brushes.LightGray, new Pen(Color.WhiteSmoke, 2),
                new RectangleModel { X = 1.85, Y = 0.85, Width = 1.5, Height = 1.5 });
            painter.DrawEllipse(Brushes.LightGray, new Pen(Color.WhiteSmoke, 2),
                new RectangleModel { X = 10.55, Y = 0.85, Width = 1.5, Height = 1.5 });
            painter.DrawEllipse(Brushes.DimGray, null,
                new RectangleModel { X = 2.35, Y = 1.35, Width = 0.5, Height = 0.5 });
            painter.DrawEllipse(Brushes.DimGray, null,
                new RectangleModel { X = 11.05, Y = 1.35, Width = 0.5, Height = 0.5 });

            // Main car body
            painter.DrawPolygon(Brushes.Crimson, outline, new[]
            {
                new PointModel { X = 0, Y = 2.0 },
                new PointModel { X = 0.2, Y = 3.5 },
                new PointModel { X = 1.1, Y = 4.2 },
                new PointModel { X = 4.2, Y = 4.35 },
                new PointModel { X = 5.7, Y = 6.5 },
                new PointModel { X = 9.3, Y = 6.5 },
                new PointModel { X = 11.1, Y = 4.3 },
                new PointModel { X = 13.2, Y = 3.8 },
                new PointModel { X = 13.6, Y = 2.1 },
                new PointModel { X = 12.7, Y = 1.8 },
                new PointModel { X = 11.8, Y = 1.8 },
                new PointModel { X = 11.3, Y = 2.5 },
                new PointModel { X = 3.2, Y = 2.5 },
                new PointModel { X = 2.6, Y = 1.8 },
                new PointModel { X = 1.4, Y = 1.8 },
            });

            // Windows
            painter.DrawPolygon(Brushes.LightSkyBlue, new Pen(Color.White, 2), new[]
            {
                new PointModel { X = 4.8, Y = 4.55 },
                new PointModel { X = 6.2, Y = 6.0 },
                new PointModel { X = 7.4, Y = 6.0 },
                new PointModel { X = 7.4, Y = 4.55 },
            });
            painter.DrawPolygon(Brushes.LightSkyBlue, new Pen(Color.White, 2), new[]
            {
                new PointModel { X = 7.8, Y = 4.55 },
                new PointModel { X = 7.8, Y = 6.0 },
                new PointModel { X = 9.0, Y = 6.0 },
                new PointModel { X = 10.1, Y = 4.55 },
            });

            // Door and handles
            painter.DrawRectangle(null, new Pen(Color.FromArgb(120, 20, 30), 2),
                new RectangleModel { X = 7.5, Y = 2.6, Width = 0.1, Height = 1.7 });
            painter.DrawRectangle(Brushes.WhiteSmoke, null,
                new RectangleModel { X = 8.2, Y = 3.9, Width = 0.8, Height = 0.12 });

            // Headlights and rear light
            painter.DrawEllipse(Brushes.LightYellow, new Pen(Color.Goldenrod, 1),
                new RectangleModel { X = 12.7, Y = 3.1, Width = 0.55, Height = 0.4 });
            painter.DrawRectangle(Brushes.OrangeRed, null,
                new RectangleModel { X = 0.15, Y = 3.0, Width = 0.25, Height = 0.55 });

            // Bumpers
            painter.DrawRectangle(Brushes.Silver, new Pen(Color.Gray, 1),
                new RectangleModel { X = 12.9, Y = 1.9, Width = 0.65, Height = 0.2 });
            painter.DrawRectangle(Brushes.Silver, new Pen(Color.Gray, 1),
                new RectangleModel { X = -0.05, Y = 1.9, Width = 0.65, Height = 0.2 });
        }
    }
}
