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
            Left = -8,
            Right = 9,
            Bottom = -1,
            Top = 9,
        };

        public void Paint(PaintManager paintManager)
        {
            var blackPen = new Pen(Color.Black, 2);
            var solidBrush = new SolidBrush(Color.DarkGray);
            var gradientBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(1, 1),Color.Red, Color.Yellow);

            //House
            paintManager.DrawRectangle(blackPen, solidBrush, new MathRectangle { Left = -4, Right = 4, Bottom = 0, Top = 4 });

            //Roof
            paintManager.DrawPolygon(blackPen, gradientBrush, new[]
            {
                new MathPoint { X = -4, Y = 4 },
                new MathPoint { X = 0, Y = 7 },
                new MathPoint { X = 4, Y = 4 },
            });

            //Sun
            paintManager.DrawEllipse(blackPen, new SolidBrush(Color.Yellow), new MathRectangle { Left = 6, Right = 9, Bottom = 6, Top = 9 });

            //Tree
            paintManager.DrawRectangle(blackPen, new SolidBrush(Color.Brown), new MathRectangle { Left = -7, Right = -6, Bottom = 0, Top = 3 });
            paintManager.DrawEllipse(blackPen, new SolidBrush(Color.Green), new MathRectangle { Left = -8, Right = -5, Bottom = 3, Top = 6 });

            //Window
            paintManager.DrawRectangle(blackPen, new SolidBrush(Color.LightBlue), new MathRectangle { Left = -2, Right = 0, Bottom = 1, Top = 3 });

            //Door
            paintManager.DrawRectangle(blackPen, new SolidBrush(Color.Brown), new MathRectangle { Left = 1, Right = 3, Bottom = 0, Top = 2.5 });
        }
    }
}
