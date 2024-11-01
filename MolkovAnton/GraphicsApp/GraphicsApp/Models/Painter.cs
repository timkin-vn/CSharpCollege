using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -15,
            Right = 15,
            Bottom = -2,
            Top = 21,
        };

        public void Paint(PaintManager paintManager)
        {
            // Корпус 
            var mainColor = Color.FromArgb(94, 94, 94); 

            var mainPen = new Pen(Color.Black, 3);
            var mainBrush = new SolidBrush(mainColor);

            paintManager.DrawRectangle(mainPen, mainBrush, new MathRectangle { Left = -9 , Right = 9 , Bottom = 0 , Top = 4});

            // Боковые вставки
           
            paintManager.DrawPolygon(mainPen, mainBrush,
                new[]
                {
                    new MathPoint {X  = -9, Y = 4},
                    new MathPoint {X  = -9, Y = 0},
                    new MathPoint {X  = -12, Y = 4},
                });

            paintManager.DrawPolygon(mainPen, mainBrush,
               new[]
               {
                   new MathPoint { X = 9, Y = 4 },
                   new MathPoint { X = 9, Y = 0 },
                   new MathPoint { X = 12, Y = 4 },
               });

            // Верхний уровень

            paintManager.DrawRectangle(mainPen, mainBrush, new MathRectangle { Left = -7, Right = 7, Bottom = 4, Top = 8 });

            // Крыша корабля

            var roofColor = Color.FromArgb(124, 149, 166);

            var roofPen = new Pen(Color.Black, 3);
            var roofBrush = new SolidBrush(roofColor);
            paintManager.DrawRectangle(roofPen, roofBrush, new MathRectangle { Left = -6, Right = -3 , Bottom = 8, Top = 10 });
            paintManager.DrawRectangle(roofPen, roofBrush, new MathRectangle { Left = -2, Right = 6, Bottom = 9, Top = 8 });

            Color roofColor1 = Color.FromArgb(255, 0, 0);
            Color roofColor2 = Color.FromArgb(148, 108, 255);

            var roofGradientBrush = new LinearGradientBrush( 
                new Point(4,12),
                new Point(1, 9),
                roofColor1,
                roofColor2
            );
          
            paintManager.DrawRectangle(roofPen, roofGradientBrush, new MathRectangle { Left = 1, Right = 4, Bottom = 9, Top = 12});

            // Дым из крыши

            Color smokeColor = Color.FromArgb(47,61,52); 

            var smokePen = new Pen(smokeColor, 3);
            var smokeBrush = new SolidBrush(smokeColor);

            paintManager.DrawEllipse(smokePen, smokeBrush, new MathRectangle { Left = 1, Right = 3, Bottom = 13, Top = 15 });
            paintManager.DrawEllipse(smokePen, smokeBrush, new MathRectangle { Left = -2, Right = 0, Bottom = 14, Top = 16 });
            paintManager.DrawEllipse(smokePen, smokeBrush, new MathRectangle { Left = -5, Right = -3, Bottom = 15, Top = 17 });

            // Иллюминаторы

            var windowColor = Color.FromArgb(49, 127, 240); 

            var windowPen = new Pen(Color.Black, 3);
            var windowBrush = new SolidBrush(windowColor);

            // Корпус

            paintManager.DrawRectangle(windowPen, windowBrush, new MathRectangle { Left = -7, Right = -5, Bottom = 1, Top = 3 });
            paintManager.DrawRectangle(windowPen, windowBrush, new MathRectangle { Left = -3, Right = -1, Bottom = 1, Top = 3 });
            paintManager.DrawRectangle(windowPen, windowBrush, new MathRectangle { Left = 1, Right = 3, Bottom = 1, Top = 3 });
            paintManager.DrawRectangle(windowPen, windowBrush, new MathRectangle { Left = 7, Right = 5, Bottom = 1, Top = 3 });

            // Верхний уровень

            paintManager.DrawEllipse(windowPen, windowBrush, new MathRectangle { Left = 3, Right = 6, Bottom = 5, Top = 7});

            // Вода 
            var waterColor = Color.FromArgb(0, 14, 97);

            var waterPen = new Pen(waterColor, 3);
            var waterBrush = new SolidBrush(waterColor);

            paintManager.DrawRectangle(waterPen, waterBrush, new MathRectangle { Left = -21, Right = 21, Bottom = -2, Top = 0 });
        }
    }
}
