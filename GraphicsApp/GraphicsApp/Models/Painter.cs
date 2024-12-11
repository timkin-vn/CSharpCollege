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
    internal class BoatPainter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -10,
            Right = 10,
            Bottom = -5,
            Top = 10,
        };

        public void Paint(PaintManager paintManager)
        {
            
            var mainPen = new Pen(Color.Black, 2);
            var hullBrush = new SolidBrush(Color.Brown);
            var sailBrush = new SolidBrush(Color.White);
            var waterBrush = new SolidBrush(Color.LightBlue);
            var gradientBrush = new LinearGradientBrush(new Rectangle(-5, -5, 10, 10), Color.LightYellow, Color.DarkOrange, 45);

            // Вода
            paintManager.DrawRectangle(mainPen, waterBrush, new MathRectangle { Left = -10, Right = 10, Bottom = -5, Top = 0 });

            // Основание 
            paintManager.DrawPolygon(mainPen, hullBrush, new[]
            {
                new MathPoint { X = -3, Y = 0 },
                new MathPoint { X = 3, Y = 0 },
                new MathPoint { X = 4, Y = -1 },
                new MathPoint { X = -4, Y = -1 }
            });

            // Паруса
            paintManager.DrawPolygon(mainPen, sailBrush, new[]
            {
                new MathPoint { X = 0, Y = 0 },
                new MathPoint { X = 1, Y = 3 },
                new MathPoint { X = 0, Y = 3 }
            });

            paintManager.DrawPolygon(mainPen, sailBrush, new[]
            {
                new MathPoint { X = 0, Y = 0 },
                new MathPoint { X = -1, Y = 3 },
                new MathPoint { X = 0, Y = 3 }
            });

            // Солнце с градиентом
            paintManager.DrawEllipse(mainPen, gradientBrush, new MathRectangle { Left = -5, Right = 5, Bottom = 5, Top = 10 });

            // Облака
            var cloudBrush = new SolidBrush(Color.White);
            paintManager.DrawEllipse(mainPen, cloudBrush, new MathRectangle { Left = -8, Right = -6, Bottom = 7, Top = 9 });
            paintManager.DrawEllipse(mainPen, cloudBrush, new MathRectangle { Left = -7, Right = -5, Bottom = 7, Top = 9 });
            paintManager.DrawEllipse(mainPen, cloudBrush, new MathRectangle { Left = -6.5, Right = -4.5, Bottom = 8, Top = 9 });
            paintManager.DrawEllipse(mainPen, cloudBrush, new MathRectangle { Left = -3, Right = -4.5, Bottom = 7, Top = 9 });

            // Флаг
            var flagBrush = new SolidBrush(Color.Red);
            paintManager.DrawPolygon(mainPen, flagBrush, new[]
            {
                new MathPoint { X = 0, Y = 3 },
                new MathPoint { X = 0.5, Y = 3.5 },
                new MathPoint { X = 0, Y = 4 }
            });
        }
    }
}
