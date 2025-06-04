using Drawing.Models;
using System.Drawing;
using System;
using System.Drawing.Drawing2D;

namespace Drawing.Services {
    internal class PictureBuilder {
        public RectangleModel SourceBounds { get; } = new RectangleModel {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };
        public static Random Random { get => random; set => random = value; }

        private static Random random = new Random();

        public void DrawPicture(Painter painter) {
            DrawSky(painter);
            DrawUFO(painter);
            DrawUFOLight(painter);
        }

        private void DrawSky(Painter painter) {
            var skyBrush = new LinearGradientBrush(
                new PointF(-5f, -1f), new PointF(14f, 11f),
                Color.FromArgb(255, 8, 15, 40),
                Color.FromArgb(255, 8, 15, 40)
            );

            var sky = new RectangleModel { X = -5f, Y = -1f, Width = 19f, Height = 12f };
            painter.DrawRectangle(skyBrush, null, sky);
        }

        private void DrawUFO(Painter painter) {
            var bodyGradient = new LinearGradientBrush(
                new PointF(2f, 8.3f), new PointF(2f, 6.3f),
                Color.FromArgb(255, 190, 190, 220),
                Color.FromArgb(255, 110, 110, 140)
            );
            var bodyPen = new Pen(Color.FromArgb(255, 70, 70, 100), 0.1f);
            var ufoBody = new RectangleModel { X = 1f, Y = 6.3f, Height = 2f, Width = 6f };
            painter.DrawEllipse(bodyGradient, bodyPen, ufoBody);

            var lowerBodyGradient = new LinearGradientBrush(
                new PointF(1f, 6.8f), new PointF(7f, 6.8f),
                Color.FromArgb(255, 160, 160, 190),
                Color.FromArgb(255, 100, 100, 130)
            );
            var lowerBody = new RectangleModel { X = 1f, Y = 6.3f, Height = 0.5f, Width = 6f };
            painter.DrawEllipse(lowerBodyGradient, bodyPen, lowerBody);
            _ = new LinearGradientBrush(
                new PointF(3f, 8.5f), new PointF(3f, 6.8f),
                Color.FromArgb(180, 240, 240, 255),
                Color.FromArgb(120, 180, 180, 220)
            );

            var windowPen = new Pen(Color.FromArgb(200, 20, 20, 40), 0.05f);
            for (int i = 0; i < 5; i++) {
                var window = new RectangleModel {
                    X = 1.5f + i * 1f,
                    Y = 7.5f,
                    Width = 0.6f,
                    Height = 0.3f
                };

                var windowGradient = new LinearGradientBrush(
                    new PointF((float)window.X, (float)window.Y),
                    new PointF((float)window.X, (float)window.Y + (float)window.Height),
                    Color.FromArgb(255, 80 + i * 30, 120 + i * 20, 220),
                    Color.FromArgb(255, 40 + i * 15, 60 + i * 10, 180)
                );

                painter.DrawEllipse(windowGradient, windowPen, window);
            }
            DrawAntenna(painter);
        }

        private void DrawAntenna(Painter painter) {
            var antennaBrush = new LinearGradientBrush(
                new PointF(4f, 8.3f), new PointF(4f, 8.8f),
                Color.FromArgb(255, 200, 200, 220),
                Color.FromArgb(255, 150, 150, 170)
            );
            var antennaPen = new Pen(Color.FromArgb(255, 100, 100, 120), 0.05f);

            var antennaBase = new RectangleModel { X = 3.9f, Y = 8.3f, Width = 0.2f, Height = 0.2f };
            painter.DrawEllipse(antennaBrush, antennaPen, antennaBase);

            PointModel[] antennaRod =
            {
                new PointModel { X = 4f, Y = 8.3f },
                new PointModel { X = 4f, Y = 9.1f },
                new PointModel { X = 4.2f, Y = 9.3f }
            };
            painter.DrawLines(antennaPen, antennaRod);

            var antennaTip = new RectangleModel { X = 4.15f, Y = 9.35f, Width = 0.15f, Height = 0.15f };
            painter.DrawEllipse(new SolidBrush(Color.FromArgb(255, 255, 220, 100)), antennaPen, antennaTip);

            var glowBrush = new SolidBrush(Color.FromArgb(30, 255, 255, 200));
            var glow = new RectangleModel { X = 4f, Y = 9.5f, Width = 0.5f, Height = 0.5f };
            painter.DrawEllipse(glowBrush, null, glow);
        }

        private void DrawUFOLight(Painter painter) {

            var rayPen = new Pen(Color.FromArgb(100, 255, 255, 180), 0.08f);
            for (int i = 0; i < 9; i++) {
                float xPos = 2.1f + i * 0.5f;
                float variation = (float)(Random.NextDouble() * 0.8f - 0.4f);

                PointModel[] ray =
                {
                    new PointModel { X = xPos, Y = 6.3f },
                    new PointModel { X = xPos + variation, Y = 3.8f }
                };
                painter.DrawLines(rayPen, ray);
            }
        }
    }
}