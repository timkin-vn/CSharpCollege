using Drawing.Models;
using Drawing.ViewServices;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = 0,
            Y = 0,
            Width = 20,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            using (var solidPen = new Pen(Color.MidnightBlue, 0.12f))
            using (var dashedPen = new Pen(Color.SaddleBrown, 0.1f))
            using (var dottedPen = new Pen(Color.DarkOliveGreen, 0.08f))
            using (var accentPen = new Pen(Color.DarkRed, 0.12f))
            using (var skyBrush = new LinearGradientBrush(
                new PointF(0, 4),
                new PointF(0, 12),
                Color.LightSkyBlue,
                Color.LemonChiffon))
            using (var grassBrush = new HatchBrush(HatchStyle.DiagonalBrick, Color.ForestGreen, Color.YellowGreen))
            using (var sunBrush = new PathGradientBrush(new[]
            {
                new PointF(16.5f, 8.8f),
                new PointF(18.7f, 8.8f),
                new PointF(18.7f, 11f),
                new PointF(16.5f, 11f),
            }))
            using (var cloudBrush = new SolidBrush(Color.FromArgb(220, Color.White)))
            using (var houseBrush = new HatchBrush(HatchStyle.LargeGrid, Color.BurlyWood, Color.Bisque))
            using (var roofBrush = new LinearGradientBrush(
                new PointF(3.5f, 6.1f),
                new PointF(9.5f, 8.3f),
                Color.Firebrick,
                Color.Maroon))
            using (var windowBrush = new LinearGradientBrush(
                new PointF(0, 0),
                new PointF(1, 1),
                Color.AliceBlue,
                Color.DeepSkyBlue))
            using (var doorBrush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Sienna, Color.Peru))
            using (var trunkBrush = new SolidBrush(Color.SaddleBrown))
            using (var crownBrush = new HatchBrush(HatchStyle.ZigZag, Color.DarkGreen, Color.LimeGreen))
            using (var pondBrush = new LinearGradientBrush(
                new PointF(13.9f, 0.9f),
                new PointF(17.1f, 2.3f),
                Color.LightCyan,
                Color.RoyalBlue))
            using (var pathBrush = new HatchBrush(HatchStyle.DashedHorizontal, Color.Khaki, Color.Goldenrod))
            {
                dashedPen.DashStyle = DashStyle.Dash;
                dottedPen.DashStyle = DashStyle.Dot;
                accentPen.DashStyle = DashStyle.DashDot;

                sunBrush.CenterColor = Color.Gold;
                sunBrush.SurroundColors = new[] { Color.Orange };

                painter.DrawRectangle(null, skyBrush, new RectangleModel { X = 0, Y = 4, Width = 20, Height = 8 });
                painter.DrawRectangle(null, grassBrush, new RectangleModel { X = 0, Y = 0, Width = 20, Height = 4 });

                painter.DrawEllipse(accentPen, sunBrush, new RectangleModel { X = 16.5, Y = 8.8, Width = 2.2, Height = 2.2 });

                painter.DrawEllipse(solidPen, cloudBrush, new RectangleModel { X = 2.0, Y = 9.0, Width = 2.0, Height = 1.1 });
                painter.DrawEllipse(solidPen, cloudBrush, new RectangleModel { X = 3.2, Y = 9.3, Width = 2.1, Height = 1.2 });
                painter.DrawEllipse(solidPen, cloudBrush, new RectangleModel { X = 4.5, Y = 9.0, Width = 2.0, Height = 1.0 });

                painter.DrawRectangle(dashedPen, houseBrush, new RectangleModel { X = 4, Y = 2.5, Width = 5, Height = 3.6 });
                painter.DrawPolygon(accentPen, roofBrush, new[]
                {
                    new PointModel { X = 3.5, Y = 6.1 },
                    new PointModel { X = 6.5, Y = 8.3 },
                    new PointModel { X = 9.5, Y = 6.1 },
                });

                painter.DrawRectangle(solidPen, doorBrush, new RectangleModel { X = 5.9, Y = 2.5, Width = 1.2, Height = 2.0 });
                painter.DrawRectangle(solidPen, windowBrush, new RectangleModel { X = 4.7, Y = 4.0, Width = 1.0, Height = 1.0 });
                painter.DrawRectangle(solidPen, windowBrush, new RectangleModel { X = 7.2, Y = 4.0, Width = 1.0, Height = 1.0 });

                painter.DrawRectangle(dottedPen, trunkBrush, new RectangleModel { X = 12.7, Y = 2.5, Width = 0.9, Height = 2.2 });
                painter.DrawEllipse(solidPen, crownBrush, new RectangleModel { X = 11.7, Y = 4.1, Width = 3.0, Height = 2.7 });
                painter.DrawEllipse(solidPen, crownBrush, new RectangleModel { X = 12.4, Y = 5.2, Width = 2.2, Height = 1.9 });

                painter.DrawEllipse(dashedPen, pondBrush, new RectangleModel { X = 13.9, Y = 0.9, Width = 3.2, Height = 1.4 });
                painter.DrawPie(dottedPen, pathBrush, new RectangleModel { X = 8.2, Y = 1.0, Width = 4.0, Height = 2.4 }, 0, 180);
            }
        }
    }
}
