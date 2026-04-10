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
            X = -16,
            Y = -5,
            Width = 32,
            Height = 20,
        };

        public void DrawPicture(Painter painter)
        {
            using (var outlinePen = new Pen(Color.FromArgb(255, 15, 18, 24), 2.2f))
            using (var thinOutlinePen = new Pen(Color.FromArgb(220, 15, 18, 24), 1.2f))
            using (var skyBrush = new SolidBrush(Color.FromArgb(255, 8, 10, 24)))
            using (var waterBrush = new SolidBrush(Color.FromArgb(255, 8, 18, 46)))
            using (var waterFrontBrush = new SolidBrush(Color.FromArgb(210, 10, 24, 58)))
            using (var starBrush = new SolidBrush(Color.FromArgb(255, 235, 240, 255)))
            using (var starWarmBrush = new SolidBrush(Color.FromArgb(255, 255, 242, 210)))
            using (var moonBrush = new SolidBrush(Color.FromArgb(255, 252, 245, 220)))
            using (var hullBrush = new SolidBrush(Color.FromArgb(255, 52, 62, 72)))
            using (var hullShadeBrush = new SolidBrush(Color.FromArgb(255, 38, 46, 56)))
            using (var hullHighlightBrush = new SolidBrush(Color.FromArgb(255, 80, 94, 110)))
            using (var deckBrush = new SolidBrush(Color.FromArgb(255, 78, 64, 50)))
            using (var deckDarkBrush = new SolidBrush(Color.FromArgb(255, 58, 48, 38)))
            using (var structureBrush = new SolidBrush(Color.FromArgb(255, 120, 132, 144)))
            using (var structureDarkBrush = new SolidBrush(Color.FromArgb(255, 95, 106, 116)))
            using (var glassBrush = new SolidBrush(Color.FromArgb(255, 170, 215, 240)))
            using (var gunBrush = new SolidBrush(Color.FromArgb(255, 70, 78, 86)))
            using (var foamBrush = new SolidBrush(Color.FromArgb(220, 140, 175, 220)))
            using (var reflectionBrush = new SolidBrush(Color.FromArgb(110, 220, 235, 255)))
            using (var craterBrush1 = new SolidBrush(Color.FromArgb(45, 210, 200, 175)))
            using (var craterBrush2 = new SolidBrush(Color.FromArgb(38, 210, 200, 175)))
            using (var craterBrush3 = new SolidBrush(Color.FromArgb(30, 210, 200, 175)))
            using (var waterHazeBrush = new SolidBrush(Color.FromArgb(60, 80, 120, 170)))
            using (var reflectionBrush2 = new SolidBrush(Color.FromArgb(80, 200, 220, 255)))
            using (var reflectionBrush3 = new SolidBrush(Color.FromArgb(55, 200, 220, 255)))
            using (var reflectionBrush4 = new SolidBrush(Color.FromArgb(40, 200, 220, 255)))
            using (var funnelTopBrush = new SolidBrush(Color.FromArgb(255, 30, 34, 40)))
            using (var flagRedBrush = new SolidBrush(Color.FromArgb(255, 200, 30, 35)))
            using (var flagWhiteBrush = new SolidBrush(Color.FromArgb(255, 230, 210, 210)))
            using (var portholeBrush = new SolidBrush(Color.FromArgb(210, 200, 210, 225)))
            {
                var left = SourceBounds.Left;
                var bottom = SourceBounds.Bottom;
                var top = SourceBounds.Top;

                var waterline = -0.6;

                painter.DrawRectangle(skyBrush, null, new RectangleModel { X = left, Y = waterline, Width = SourceBounds.Width, Height = top - waterline, });
                var waterRect = new RectangleModel { X = left, Y = bottom, Width = SourceBounds.Width, Height = waterline - bottom, };
                painter.DrawRectangle(waterBrush, null, waterRect);

                void Star(double x, double y, double r, Brush b)
                {
                    painter.DrawEllipse(b, null, new RectangleModel { X = x - r, Y = y - r, Width = r * 2, Height = r * 2, });
                }

                Star(-12.8, 13.2, 0.10, starBrush);
                Star(-10.4, 11.8, 0.12, starBrush);
                Star(-9.1, 13.8, 0.08, starWarmBrush);
                Star(-7.7, 10.9, 0.10, starBrush);
                Star(-6.1, 12.6, 0.09, starBrush);
                Star(-4.3, 14.1, 0.12, starWarmBrush);
                Star(-2.0, 11.5, 0.08, starBrush);
                Star(0.7, 12.9, 0.10, starBrush);
                Star(2.4, 10.7, 0.08, starWarmBrush);
                Star(4.8, 13.6, 0.10, starBrush);
                Star(6.9, 11.4, 0.12, starBrush);
                Star(8.6, 14.4, 0.09, starWarmBrush);
                Star(11.8, 12.3, 0.10, starBrush);
                Star(13.3, 10.9, 0.08, starBrush);

                var moonRect = new RectangleModel { X = 8.8, Y = 10.0, Width = 4.6, Height = 4.6, };
                var moonCenterX = moonRect.X + moonRect.Width / 2;

                painter.DrawEllipse(moonBrush, null, moonRect);
                painter.DrawEllipse(craterBrush1, null, new RectangleModel { X = 10.2, Y = 12.9, Width = 0.55, Height = 0.55, });
                painter.DrawEllipse(craterBrush2, null, new RectangleModel { X = 11.3, Y = 11.9, Width = 0.45, Height = 0.45, });
                painter.DrawEllipse(craterBrush3, null, new RectangleModel { X = 9.7, Y = 11.2, Width = 0.35, Height = 0.35, });

                painter.DrawRectangle(waterHazeBrush, null, new RectangleModel { X = left, Y = waterline - 0.18, Width = SourceBounds.Width, Height = 0.18, });

                var reflectionHeight = 3.2;
                var reflectionBaseWidth = SourceBounds.Width * 0.06;
                var reflectionX = moonCenterX - reflectionBaseWidth / 2;

                painter.DrawRectangle(reflectionBrush, null, new RectangleModel { X = reflectionX, Y = bottom, Width = reflectionBaseWidth, Height = reflectionHeight, });

                var glintH1 = 0.11;
                var glintH2 = 0.09;
                var glintH3 = 0.08;

                painter.DrawRectangle(reflectionBrush2, null, new RectangleModel
                {
                    X = moonCenterX - reflectionBaseWidth * 1.25,
                    Y = bottom + reflectionHeight * 0.22,
                    Width = reflectionBaseWidth * 2.5,
                    Height = glintH1,
                });
                painter.DrawRectangle(reflectionBrush3, null, new RectangleModel
                {
                    X = moonCenterX - reflectionBaseWidth * 1.05,
                    Y = bottom + reflectionHeight * 0.44,
                    Width = reflectionBaseWidth * 2.1,
                    Height = glintH2,
                });
                painter.DrawRectangle(reflectionBrush4, null, new RectangleModel
                {
                    X = moonCenterX - reflectionBaseWidth * 0.85,
                    Y = bottom + reflectionHeight * 0.66,
                    Width = reflectionBaseWidth * 1.7,
                    Height = glintH3,
                });

                var shipDy = 0.65;

                PointModel PS(double x, double y) => new PointModel { X = x, Y = y + shipDy, };
                RectangleModel RS(double x, double y, double w, double h) => new RectangleModel { X = x, Y = y + shipDy, Width = w, Height = h, };

                var hull = new[]
                {
                    PS(-12.8, 0.55),
                    PS(-12.2, 0.95),
                    PS(-9.2, 1.05),
                    PS(-6.2, 1.05),
                    PS(-3.2, 1.10),
                    PS(3.8, 1.10),
                    PS(7.6, 1.00),
                    PS(10.6, 0.85),
                    PS(12.6, 0.30),
                    PS(12.1, -0.95),
                    PS(10.0, -1.95),
                    PS(-10.6, -1.95),
                    PS(-12.8, -0.85),
                };
                painter.DrawPolygon(hullBrush, outlinePen, hull);

                painter.DrawPolygon(hullShadeBrush, null, new[]
                {
                    PS(-12.6, 0.30),
                    PS(12.2, 0.10),
                    PS(11.4, -0.75),
                    PS(-11.4, -0.85),
                });

                painter.DrawRectangle(deckBrush, null, RS(-11.4, 1.05, 23.0, 0.25));
                painter.DrawRectangle(deckDarkBrush, null, RS(-11.4, 0.90, 23.0, 0.18));

                for (var i = 0; i < 15; i++)
                {
                    var x = -10.4 + i * 1.55;
                    painter.DrawEllipse(portholeBrush, null, RS(x, -0.35, 0.22, 0.22));
                }

                painter.DrawRectangle(structureDarkBrush, outlinePen, RS(-9.2, 1.05, 3.0, 0.85));
                painter.DrawRectangle(structureBrush, outlinePen, RS(-8.8, 1.90, 2.2, 0.55));

                painter.DrawRectangle(structureBrush, outlinePen, RS(-2.8, 1.05, 8.2, 1.05));
                painter.DrawRectangle(structureDarkBrush, outlinePen, RS(-1.6, 2.10, 3.4, 0.95));

                painter.DrawRectangle(structureBrush, outlinePen, RS(-1.2, 3.05, 2.7, 0.85));
                painter.DrawRectangle(glassBrush, thinOutlinePen, RS(-1.05, 3.35, 2.4, 0.40));

                painter.DrawPolygon(structureDarkBrush, outlinePen, new[]
                {
                    PS(1.7, 2.05),
                    PS(3.7, 2.05),
                    PS(3.3, 4.30),
                    PS(2.1, 4.30),
                });
                painter.DrawRectangle(funnelTopBrush, null, RS(2.1, 4.10, 1.2, 0.25));

                painter.DrawRectangle(structureDarkBrush, null, RS(4.9, 2.15, 0.25, 2.85));
                painter.DrawPolygon(structureDarkBrush, null, new[]
                {
                    PS(5.02, 5.00),
                    PS(6.10, 4.10),
                    PS(6.00, 4.35),
                });

                void Turret(double x, double y, bool forward)
                {
                    painter.DrawRectangle(gunBrush, outlinePen, RS(x, y, 2.2, 0.68));
                    painter.DrawPolygon(gunBrush, outlinePen, new[]
                    {
                        PS(x + 0.15, y + 0.68),
                        PS(x + 2.05, y + 0.68),
                        PS(x + 1.85, y + 0.92),
                        PS(x + 0.35, y + 0.92),
                    });
                    painter.DrawRectangle(hullHighlightBrush, null, RS(x + 0.15, y + 0.52, 1.9, 0.10));

                    void Barrel(double bx, double by, double len, double t0, double t1)
                    {
                        var x1 = forward ? (bx + len) : (bx - len);
                        var t0h = t0 / 2;
                        var t1h = t1 / 2;
                        painter.DrawPolygon(gunBrush, thinOutlinePen, new[]
                        {
                            PS(bx, by - t0h),
                            PS(bx, by + t0h),
                            PS(x1, by + t1h),
                            PS(x1, by - t1h),
                        });
                    }

                    var baseX = forward ? (x + 1.95) : (x + 0.25);
                    Barrel(baseX, y + 0.36, 3.2, 0.10, 0.06);
                    Barrel(baseX, y + 0.28, 3.25, 0.10, 0.06);
                    Barrel(baseX, y + 0.44, 3.15, 0.10, 0.06);
                }

                Turret(6.8, 1.15, true);
                Turret(3.9, 1.15, true);
                Turret(-7.2, 1.15, false);
                Turret(-10.0, 1.15, false);

                painter.DrawRectangle(flagRedBrush, outlinePen, RS(5.7, 4.45, 0.9, 0.55));
                painter.DrawPolygon(flagWhiteBrush, outlinePen, new[]
                {
                    PS(6.6, 4.95),
                    PS(8.4, 4.65),
                    PS(6.6, 4.35),
                });

                painter.DrawPolygon(foamBrush, null, new[]
                {
                    PS(12.0, -0.15),
                    PS(13.2, -0.65),
                    PS(12.6, -1.10),
                    PS(11.5, -0.55),
                });

                var deepWaterTop = waterline - 1.8;
                painter.DrawRectangle(waterFrontBrush, null, new RectangleModel { X = left, Y = bottom, Width = SourceBounds.Width, Height = deepWaterTop - bottom, });

                void WaveRow(double y, double baseLenFrac, double stepFrac, double startFrac)
                {
                    var step = SourceBounds.Width * stepFrac;
                    var baseLen = SourceBounds.Width * baseLenFrac;
                    var h = 0.10;

                    var j = 0;
                    for (var x = left + SourceBounds.Width * startFrac; x < left + SourceBounds.Width + step; x += step)
                    {
                        var shift = step * 0.12 * Math.Sin(j * 0.95 + y * 0.7);
                        var len = baseLen * (0.75 + 0.25 * Math.Sin(j * 1.35 + y * 0.55));
                        painter.DrawRectangle(foamBrush, null, new RectangleModel { X = x + shift, Y = y, Width = len, Height = h, });
                        j++;
                    }
                }

                WaveRow(waterline - 0.35, 0.055, 0.095, 0.04);
                WaveRow(waterline - 1.15, 0.042, 0.090, 0.08);
                WaveRow(waterline - 1.95, 0.060, 0.102, 0.02);
            }
        }
    }
}
