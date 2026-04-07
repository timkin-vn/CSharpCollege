using Drawing.Models;
using System.Drawing;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -3,
            Y = -1,
            Width = 14,
            Height = 6,
        };

        public void DrawPicture(Painter painter)
        {
            var bodyBrush = Brushes.DodgerBlue;
            var windowBrush = Brushes.LightSkyBlue;
            var wheelBrush = Brushes.Black;
            var rimBrush = Brushes.LightGray;
            var outlinePen = new Pen(Color.Black, 2);
            var thinPen = new Pen(Color.Black, 1);

            var body = new RectangleModel { X = -1.5, Y = 1, Width = 11, Height = 2.5 };
            painter.DrawRectangle(bodyBrush, outlinePen, body);

            var roof = new RectangleModel { X = 1.5, Y = 3, Width = 5, Height = 1.5 };
            painter.DrawRectangle(bodyBrush, outlinePen, roof);

            var frontWindow = new RectangleModel { X = 2, Y = 3.3, Width = 2, Height = 1 };
            painter.DrawRectangle(windowBrush, thinPen, frontWindow);
            var rearWindow = new RectangleModel { X = 4.5, Y = 3.3, Width = 1.8, Height = 1 };
            painter.DrawRectangle(windowBrush, thinPen, rearWindow);

            var headlight = new RectangleModel { X = 9.5, Y = 1.5, Width = 0.7, Height = 0.7 };
            painter.DrawEllipse(Brushes.Yellow, outlinePen, headlight);

            var taillight = new RectangleModel { X = -1.8, Y = 1.5, Width = 0.6, Height = 0.8 };
            painter.DrawRectangle(Brushes.Red, outlinePen, taillight);

            var rearWheel = new RectangleModel { X = 0.5, Y = -0.2, Width = 1.8, Height = 1.8 };
            painter.DrawEllipse(wheelBrush, outlinePen, rearWheel);
            var rearRim = new RectangleModel { X = 1, Y = 0.3, Width = 0.8, Height = 0.8 };
            painter.DrawEllipse(rimBrush, thinPen, rearRim);

            var frontWheel = new RectangleModel { X = 6.5, Y = -0.2, Width = 1.8, Height = 1.8 };
            painter.DrawEllipse(wheelBrush, outlinePen, frontWheel);
            var frontRim = new RectangleModel { X = 7, Y = 0.3, Width = 0.8, Height = 0.8 };
            painter.DrawEllipse(rimBrush, thinPen, frontRim);

            var handle = new RectangleModel { X = 3.5, Y = 2, Width = 1, Height = 0.2 };
            painter.DrawRectangle(null, thinPen, handle);
        }
    }
}