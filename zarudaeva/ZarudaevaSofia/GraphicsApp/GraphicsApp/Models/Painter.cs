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
            Left = -15,
            Right = 15,
            Bottom = -10,
            Top = 15,
        };

        public void Paint(PaintManager paintManager)
        {
            // перья и кисти
            var blackPen = new Pen(Color.Black, 2);
            var orangePen = new Pen(Color.OrangeRed, 2);
            var gradientBrush = new LinearGradientBrush(
                new Point(0, -5),
                new Point(0, 5),
                Color.OrangeRed,
                Color.White);

            var bodyBrush = new SolidBrush(Color.OrangeRed);
            var faceBrush = new SolidBrush(Color.White);
            var tailBrush = new SolidBrush(Color.Orange);
            var eyeBrush = new SolidBrush(Color.Black);

            //  тело 
            paintManager.DrawEllipse(blackPen, bodyBrush, new MathRectangle { Left = 5, Right = -5, Bottom = -5, Top = 3 });

            //  голова
            paintManager.DrawEllipse(blackPen, faceBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 3, Top = 7 });

            //  уши 
            paintManager.DrawPolygon(blackPen, bodyBrush,
                new[]
                {
                    new MathPoint { X = -2, Y = 6 },
                    new MathPoint { X = -3, Y = 8 },
                    new MathPoint { X = -1, Y = 7 }
                });

            paintManager.DrawPolygon(blackPen, bodyBrush,
                new[]
                {
                    new MathPoint { X = 2, Y = 6 },
                    new MathPoint { X = 3, Y = 8 },
                    new MathPoint { X = 1, Y = 7 }
                });

            //  хвост 
            paintManager.DrawRectangle(orangePen, gradientBrush, new MathRectangle { Left = 4, Right = 12, Bottom = -2, Top = 0 });

            //  лапы 
            paintManager.DrawRectangle(blackPen, bodyBrush, new MathRectangle { Left = -3, Right = -2, Bottom = -8, Top = -5 });
            paintManager.DrawRectangle(blackPen, bodyBrush, new MathRectangle { Left = 2, Right = 3, Bottom = -8, Top = -5 });

            //  глаза
            paintManager.DrawEllipse(blackPen, eyeBrush, new MathRectangle { Left = -1, Right = -0.5, Bottom = 4.5, Top = 5 });
            paintManager.DrawEllipse(blackPen, eyeBrush, new MathRectangle { Left = 0.5, Right = 1, Bottom = 4.5, Top = 5 });

            //  нос 
            paintManager.DrawEllipse(blackPen, eyeBrush, new MathRectangle { Left = -0.5, Right = 0.5, Bottom = 3, Top = 3.5 });
        }
    }
}

