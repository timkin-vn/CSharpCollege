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
            Bottom = -3,
            Top = 9,
        };

        public void Paint(PaintManager paintManager)
        {
            var mainPen = new Pen(Color.Black, 3);
            var bodyBrush = new SolidBrush(Color.DarkGray);
            Color smokeColor = Color.FromArgb(47, 61, 52);
            var smokeBrush = new SolidBrush(smokeColor);
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -3.5, Right = 6.5,  Bottom = -1.5,Top = -0.5, });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -3.5, Right = 0.5, Bottom = -0.5, Top = 0.5, });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 1, Right = 6.5, Bottom = -0.5, Top = 0, });
            paintManager.DrawPie(mainPen, bodyBrush, new MathRectangle { Left = -1.5, Right = 0.5, Bottom = 3, Top = 4 }, 180,180);
            var pilotBrush = new SolidBrush(Color.DarkRed);
            paintManager.DrawPolygon(mainPen, pilotBrush,
                new[]
                {
                    new MathPoint{X = -2.5, Y= 0.5,},
                    new MathPoint{X = -1.5, Y= 3.5,},
                    new MathPoint{X = 0.5, Y= 3.5,},
                    new MathPoint{X = 0.5, Y= 0.5,},
                    new MathPoint{X = -2.5, Y= 0.5,},
                });
            paintManager.DrawPolygon(mainPen, smokeBrush,
                new[]
                {
                    new MathPoint{X = 1.5, Y= 0,},
                    new MathPoint{X = 5.5, Y= 4,},
                    new MathPoint{X = 5.7, Y= 4,},
                    new MathPoint{X = 6.7, Y= 5,},
                    new MathPoint{X = 5.5, Y= 5,},
                    new MathPoint{X = 5.5, Y = 8},
                    new MathPoint{X = 8.5, Y = 5},
                    new MathPoint{X = 7.2, Y = 5},
                    new MathPoint{X = 6.2, Y = 4},
                    new MathPoint{X = 6.5, Y = 4},
                    new MathPoint{X = 4.5, Y = 2}, 
                    new MathPoint{X = 6, Y = 0},
                    new MathPoint{X = 1.5, Y= 0,}
                });
            var brush = new LinearGradientBrush(new Point { X = -1 ,Y = 1 }, new Point { X = 0, Y = 25 }, Color.Yellow, Color.Red);
            paintManager.DrawRectangle(mainPen, brush, new MathRectangle { Left = -1.5, Right = -0.5, Bottom = 1, Top = 2, });
            paintManager.DrawEllipse(mainPen, pilotBrush, new MathRectangle { Left = -2, Right = 0, Bottom = -2.5, Top = -0.5 });
            paintManager.DrawEllipse(mainPen, pilotBrush, new MathRectangle { Left = 0, Right = 2, Bottom = -2.5, Top = -0.5 });
            paintManager.DrawEllipse(mainPen, pilotBrush, new MathRectangle { Left = 2, Right = 4, Bottom = -2.5, Top = -0.5 });
            paintManager.DrawEllipse(mainPen, pilotBrush, new MathRectangle { Left = 4, Right = 6, Bottom = -2.5, Top = -0.5 });

        }
    }
}
