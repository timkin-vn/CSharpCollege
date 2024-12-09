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
            // Перо для рисования контуров
            var mainPen = new Pen(Color.Black, 2);
            var wheelPen = new Pen(Color.DarkGray, 2);

            // Заливка для кузова машины
            var bodyBrush = new SolidBrush(Color.LightCoral);

            // Рисуем кузов машины
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -5, Right = 5, Bottom = 0, Top = 3 });

        

            // Заливка для окон с градиентом
            var windowBrush = new LinearGradientBrush(
                new Rectangle(-2, 3, 5, 2),
                Color.LightSkyBlue,
                Color.White,
                LinearGradientMode.Horizontal);
            paintManager.DrawRectangle(mainPen, windowBrush, new MathRectangle { Left = -2.5, Right = 2.5, Bottom = 3, Top = 5 });

            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -3, Right = 2.5, Bottom = 4.5, Top = 5 });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -3, Right = -2.4, Bottom = 3, Top = 4.53 });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 2.2, Right = 2.5, Bottom = 3, Top = 4.53 });

            // Рисуем передние зеркала
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 2.1, Right = 2.3, Bottom = 2.6, Top = 3.6 });

            // Передний бампер
            var bumperBrush = new SolidBrush(Color.DarkGray);
            paintManager.DrawRectangle(mainPen, bumperBrush, new MathRectangle { Left = -5, Right = -4.2, Bottom = 0, Top = 1 });

            // Задний бампер
            paintManager.DrawRectangle(mainPen, bumperBrush, new MathRectangle { Left = 4.3, Right = 5, Bottom = 0, Top = 1 });

            // Рисуем колеса
            var wheelBrush = new SolidBrush(Color.Black);
            paintManager.DrawEllipse(wheelPen, wheelBrush, new MathRectangle { Left = -4.5, Right = -2.5, Bottom = -1, Top = 1 });
            paintManager.DrawEllipse(wheelPen, wheelBrush, new MathRectangle { Left = 2.5, Right = 4.5, Bottom = -1, Top = 1 });

            // Фары
            var lightBrush = new SolidBrush(Color.Yellow);
            paintManager.DrawEllipse(mainPen, lightBrush, new MathRectangle { Left = -5, Right = -4.2, Bottom = 3, Top = 2.5 });
            paintManager.DrawEllipse(mainPen, lightBrush, new MathRectangle { Left = 4.2, Right = 5, Bottom = 3, Top = 2.5 });

            var pilotBrush = new SolidBrush(Color.DarkGray);
            paintManager.DrawPolygon(mainPen, pilotBrush,
                new[]
                {
                    new MathPoint{X = 5, Y= 1,},
                    new MathPoint{X = 5, Y= 1,},
                    new MathPoint{X = 5.5, Y= 0,},
                    new MathPoint{X = 5, Y= 0,},
                });

        
        }
    }
}

