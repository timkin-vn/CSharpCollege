using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -2,
            Right = 2,
            Bottom = 0,
            Top = 3,
        };

        public void Paint(PaintManager paintManager)
        {
            var grassBrush = new SolidBrush(Color.Green);
            var skyBrush = new SolidBrush(Color.SkyBlue);
            var mountainBrush = new SolidBrush(Color.Gray);
            var riverBrush = new SolidBrush(Color.Blue);
            var riverPen = new Pen(Color.Blue, 2);

            // Draw sky
            paintManager.DrawRectangle(null, skyBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 0, Top = 2 });

            // Draw grass
            paintManager.DrawRectangle(null, grassBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 2, Top = 3 });

            // Draw mountains
            paintManager.DrawPolygon(null, mountainBrush,
               new[]
               {
                    new MathPoint { X = -2, Y = 2 },
                    new MathPoint { X = -1, Y = 1.5 },
                    new MathPoint { X = 0, Y = 2 },
                    new MathPoint { X = 1, Y = 1.5 },
                    new MathPoint { X = 2, Y = 2 },
               });

            // Draw river
            paintManager.DrawPath(riverPen, riverBrush,
               new[]
               {
                    new MathPoint { X = -2, Y = 2 },
                    new MathPoint { X = -1, Y = 1.8 },
                    new MathPoint { X = 0, Y = 1.9 },
                    new MathPoint { X = 1, Y = 1.8 },
                    new MathPoint { X = 2, Y = 2 },
               });
        }
    }
}