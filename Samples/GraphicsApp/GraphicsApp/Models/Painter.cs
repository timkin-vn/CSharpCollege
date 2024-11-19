using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -8,
            Right = 9,
            Bottom = -1,
            Top = 9,
        };

        private Random random = new Random();
        private Color color1;
        private Color color2;

        public Painter()
        {
            color1 = GetRandomBrightColor();
            color2 = GetRandomBrightColor();
        }

        public void Paint(PaintManager paintManager)
        {
            var mainPen = new Pen(Color.Black, 3);

            for (double x = LimitRectangle.Left; x < LimitRectangle.Right; x++)
            {
                for (double y = LimitRectangle.Bottom; y < LimitRectangle.Top; y++)
                {
                    var rectangle = new MathRectangle { Left = x, Right = x + 1, Bottom = y, Top = y + 1 };
                    var gradientColor = GetGradientColor(x, y, color1, color2, LimitRectangle);
                    var gradientBrush = new SolidBrush(gradientColor);
                    paintManager.DrawRectangle(mainPen, gradientBrush, rectangle);
                }
            }
        }

        private Color GetGradientColor(double x, double y, Color color1, Color color2, MathRectangle limitRectangle)
        {
            double width = limitRectangle.Right - limitRectangle.Left;
            double height = limitRectangle.Top - limitRectangle.Bottom;

            double xRatio = (x - limitRectangle.Left) / width;
            double yRatio = (y - limitRectangle.Bottom) / height;

            int r = (int)(color1.R * (1 - xRatio) * (1 - yRatio) + color2.R * xRatio * yRatio);
            int g = (int)(color1.G * (1 - xRatio) * (1 - yRatio) + color2.G * xRatio * yRatio);
            int b = (int)(color1.B * (1 - xRatio) * (1 - yRatio) + color2.B * xRatio * yRatio);

            return Color.FromArgb(r, g, b);
        }

        private Color GetRandomBrightColor()
        {
            var brightColors = new[]
            {
                Color.Purple,
                Color.Blue,
                Color.Red,
                Color.Green,
                Color.Yellow
            };

            return brightColors[random.Next(brightColors.Length)];
        }
    }
}