using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -10,
            Right = 10,
            Bottom = -2,
            Top = 10,
        };

        public void Paint(PaintManager paintManager)
        {
            var blackPen = new Pen(Color.Black, 2);
            var solidBrush = new SolidBrush(Color.DarkGray);
            var gradientBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(6, 6), Color.Gray, Color.White);

            // Main castle building
            paintManager.DrawRectangle(blackPen, solidBrush, new MathRectangle { Left = -4, Right = 4, Bottom = 0, Top = 6 });

            // Towers
            paintManager.DrawRectangle(blackPen, gradientBrush, new MathRectangle { Left = -7, Right = -5, Bottom = 0, Top = 7 });
            paintManager.DrawRectangle(blackPen, gradientBrush, new MathRectangle { Left = 5, Right = 7, Bottom = 0, Top = 7 });

            // Castle roof
            paintManager.DrawPolygon(blackPen, new SolidBrush(Color.Brown), new[]
            {
                new MathPoint { X = -4, Y = 6 },
                new MathPoint { X = 0, Y = 8 },
                new MathPoint { X = 4, Y = 6 },
            });

            // Tower roofs
            paintManager.DrawPolygon(blackPen, new SolidBrush(Color.DarkRed), new[]
            {
                new MathPoint { X = -7, Y = 7 },
                new MathPoint { X = -6, Y = 9 },
                new MathPoint { X = -5, Y = 7 },
            });

            paintManager.DrawPolygon(blackPen, new SolidBrush(Color.DarkRed), new[]
            {
                new MathPoint { X = 5, Y = 7 },
                new MathPoint { X = 6, Y = 9 },
                new MathPoint { X = 7, Y = 7 },
            });

            // Door
            paintManager.DrawRectangle(blackPen, new SolidBrush(Color.Brown), new MathRectangle { Left = -1, Right = 1, Bottom = 0, Top = 3 });

            // Windows
            paintManager.DrawEllipse(blackPen, new SolidBrush(Color.LightBlue), new MathRectangle { Left = -3, Right = -2, Bottom = 2, Top = 3 });
            paintManager.DrawEllipse(blackPen, new SolidBrush(Color.LightBlue), new MathRectangle { Left = 2, Right = 3, Bottom = 2, Top = 3 });

            // Flags on towers
            paintManager.DrawPolygon(blackPen, new SolidBrush(Color.Red), new[]
            {
                new MathPoint { X = -6, Y = 9 },
                new MathPoint { X = -5.5, Y = 9.5 },
                new MathPoint { X = -6, Y = 10 },
            });

            paintManager.DrawPolygon(blackPen, new SolidBrush(Color.Red), new[]
            {
                new MathPoint { X = 6, Y = 9 },
                new MathPoint { X = 5.5, Y = 9.5 },
                new MathPoint { X = 6, Y = 10 },
            });
        }
    }
}
