using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

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
            var mainPen = new Pen(Color.Black, 2);
            var whiteSmokeBrush = new SolidBrush(Color.WhiteSmoke);
            var lightGrayBrush = new SolidBrush(Color.LightGray);
            var darkBlueBrush = new SolidBrush(Color.DarkBlue);
            var greenBrush = new SolidBrush(Color.Green);
            var brownBrush = new SolidBrush(Color.Brown);
            var skyBlueBrush = new SolidBrush(Color.SkyBlue);

            // Нарисуем небо
            paintManager.DrawRectangle(mainPen, skyBlueBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 1.5, Top = 3 });

            // Нарисуем землю
            paintManager.DrawRectangle(mainPen, lightGrayBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 0, Top = 1.5 });

            // Нарисуем дом
            paintManager.DrawRectangle(mainPen, brownBrush, new MathRectangle { Left = -0.5, Right = 0.5, Bottom = 0.5, Top = 1.2 });
            paintManager.DrawPolygon(mainPen, lightGrayBrush,
                new[]
                {
                    new MathPoint { X = -0.6, Y = 1.2 },
                    new MathPoint { X = 0, Y = 1.5 },
                    new MathPoint { X = 0.6, Y = 1.2 },
                });

            // Нарисуем дерево
            paintManager.DrawRectangle(mainPen, brownBrush, new MathRectangle { Left = 1.2, Right = 1.3, Bottom = 0.5, Top = 1 });

            paintManager.DrawEllipse(mainPen, greenBrush, new MathRectangle { Left = 1, Right = 1.5, Bottom = 1, Top = 1.5 });

            // Нарисуем реку
            var waterPen = new Pen(Color.FromArgb(10, 0, 0, 255), 2);
            var waterBrush = new LinearGradientBrush(new System.Drawing.PointF(0, 0), new System.Drawing.PointF(10, 0), Color.FromArgb(50, 0, 0, 255), Color.FromArgb(10, 0, 0, 255));

            paintManager.DrawPolygon(waterPen, waterBrush,
                new[]
                {
                    new MathPoint { X = -2, Y = 0.5 },
                    new MathPoint { X = -1, Y = 0.6 },
                    new MathPoint { X = 1, Y = 0.6 },
                    new MathPoint { X = 2, Y = 0.5 },
                });

            // Нарисуем облако
            paintManager.DrawEllipse(mainPen, whiteSmokeBrush, new MathRectangle { Left = -1.5, Right = -0.5, Bottom = 2.5, Top = 2.8 });
            paintManager.DrawEllipse(mainPen, whiteSmokeBrush, new MathRectangle { Left = -1, Right = 0, Bottom = 2.6, Top = 2.9 });
        }
    }
}