using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicsApp.Services;

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
            // дорога
            var roadPen = new Pen(Color.Black, 6);
            var roadBrush = new SolidBrush(Color.Black);
            paintManager.DrawRectangle(roadPen, roadBrush, new MathRectangle { Left = -10, Right = 10, Bottom = -2, Top = 1 });

            // разметка 
            var dashPen = new Pen(Color.Yellow, 6);
            var dashBrush = new SolidBrush(Color.Yellow);
            paintManager.DrawRectangle(dashPen, dashBrush, new MathRectangle { Left = -8, Right = -6, Bottom = -0.5, Top = 0.5 });
            paintManager.DrawRectangle(dashPen, dashBrush, new MathRectangle { Left = -3, Right = -1, Bottom = -0.5, Top = 0.5 });
            paintManager.DrawRectangle(dashPen, dashBrush, new MathRectangle { Left = 2, Right = 4, Bottom = -0.5, Top = 0.5 });
            paintManager.DrawRectangle(dashPen, dashBrush, new MathRectangle { Left = 7, Right = 9, Bottom = -0.5, Top = 0.5 });

            // корпус
            var carPen = new Pen(Color.Black, 2);
            var carBrush = new SolidBrush(Color.Green);
            paintManager.DrawRectangle(carPen, carBrush, new MathRectangle { Left = -3, Right = 3, Bottom = 1, Top = 3 });

            // башня
            paintManager.DrawRectangle(carPen, carBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 3, Top = 5 });

            // дуло
            var windowBrush = new SolidBrush(Color.Gray);
            paintManager.DrawRectangle(carPen, windowBrush, new MathRectangle { Left = 2, Right = 4.5, Bottom = 3.5, Top = 4.5 });

            // Колёса
            var wheelBrush = new SolidBrush(Color.Gray);
            paintManager.DrawEllipse(carPen, wheelBrush, new MathRectangle { Left = -2.5, Right = -1, Bottom = 0, Top = 1.5 });
            paintManager.DrawEllipse(carPen, wheelBrush, new MathRectangle { Left = 1, Right = 2.5, Bottom = 0, Top = 1.5 });

            // дым из трубы
            var speedLineBrush = new SolidBrush(Color.Black);
            paintManager.DrawRectangle(new Pen(Color.Black, 4), speedLineBrush, new MathRectangle { Left = -4, Right = -3, Bottom = 2, Top = 2.1 });
            paintManager.DrawRectangle(new Pen(Color.Black, 4), speedLineBrush, new MathRectangle { Left = -5, Right = -4, Bottom = 2.5, Top = 2.6 });
            paintManager.DrawRectangle(new Pen(Color.Black, 4), speedLineBrush, new MathRectangle { Left = -6, Right = -5, Bottom = 3, Top = 3.1 });

            // столб знака
            var polePen = new Pen(Color.Gray, 2);
            var poleBrush = new SolidBrush(Color.Gray);
            paintManager.DrawRectangle(polePen, poleBrush, new MathRectangle { Left = 5.5, Right = 6, Bottom = 0, Top = 4 });

            // знак стоп
            var signPen = new Pen(Color.Red, 2);
            var signBrush = new SolidBrush(Color.Red);
            paintManager.DrawEllipse(signPen, signBrush, new MathRectangle { Left = 4.5, Right = 7, Bottom = 4, Top = 6.5 });

            // белая полоса на занке
            var whiteBarBrush = new SolidBrush(Color.White);
            paintManager.DrawRectangle(signPen, whiteBarBrush, new MathRectangle { Left = 5, Right = 6.5, Bottom = 5, Top = 5.5 });

            // солнце
            var sunPen = new Pen(Color.Yellow, 2);
            var sunBrush = new SolidBrush(Color.Yellow);
            paintManager.DrawEllipse(sunPen, sunBrush, new MathRectangle { Left = -9, Right = -7, Bottom = 7, Top = 9 });

            // облака
            var cloudBrush = new SolidBrush(Color.White);
            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = -2, Right = 0, Bottom = 7, Top = 8 });
            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = -1, Right = 1, Bottom = 7.5, Top = 8.5 });
            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = 0, Right = 2, Bottom = 7, Top = 8 });

            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = 5, Right = 7, Bottom = 7, Top = 8 });
            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = 6, Right = 8, Bottom = 7.5, Top = 8.5 });
            paintManager.DrawEllipse(new Pen(Color.White, 2), cloudBrush, new MathRectangle { Left = 7, Right = 9, Bottom = 7, Top = 8 });
        }
    }
}