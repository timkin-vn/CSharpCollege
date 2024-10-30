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

        public void Paint(PaintManager paintManager)
        {
            var mainPen = new Pen(Color.Black, 3);
            var bodyBrush = new SolidBrush(Color.DarkGray);
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -4, Right = 7,  Bottom = 2,Top = 5, });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -7, Right = -4, Bottom = 2, Top = 8, });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 5, Right = 6, Bottom = 5, Top = 6.5, });
            paintManager.DrawPie(mainPen, bodyBrush, new MathRectangle { Left = 6, Right = 8, Bottom = 2, Top = 5 }, 270, 180);

            var glassPen = new Pen(Color.Blue, 2);
            var glassBrush = new SolidBrush(Color.LightCyan);
            paintManager.DrawRectangle(glassPen, glassBrush, new MathRectangle { Left = -6, Right = -4.5, Bottom = 5, Top = 7, });

            var wheelBrush = new SolidBrush(Color.Brown);
            paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = -5, Right = -3, Bottom = 0, Top = 2, });
            paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = -2, Right = 0, Bottom = 0, Top = 2, });
            paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = 1, Right = 3, Bottom = 0, Top = 2, });
            paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = 4, Right = 6, Bottom = 0, Top = 2, });

            var pilotBrush = new SolidBrush(Color.DarkRed);
            paintManager.DrawPolygon(mainPen, pilotBrush,
                new[]
                {
                    new MathPoint{X = 6.5, Y= 2,},
                    new MathPoint{X = 7, Y= 2,},
                    new MathPoint{X = 8, Y= 0.5,},
                    new MathPoint{X = 6.5, Y= 0.5,},
                });
        }
    }
}
