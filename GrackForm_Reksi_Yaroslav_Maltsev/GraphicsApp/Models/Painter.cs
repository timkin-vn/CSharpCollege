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
            Left = -11,
            Right = 10,
            Bottom = -6,
            Top = 11,
        };

        public void Paint(PaintManager paintManager)
        {
          
            var mainPen = new Pen(Color.Black, 3);
            var physiquePen = new Pen(Color.CornflowerBlue, 3); 
            var SuitPen = new Pen(Color.DarkGoldenrod, 3);
            var SuitBrush = new SolidBrush(Color.DarkViolet);
            var trousersBrush = new SolidBrush(Color.DodgerBlue);
            var  physiqueBrush =new SolidBrush( Color.FromArgb(255, 86, 123));
                
          
            paintManager.DrawRectangle(mainPen,SuitBrush, new MathRectangle { Left = -2, Right = 2,  Bottom = -1,Top = 4, });
            //Создание рук 
            paintManager.DrawRectangle(SuitPen, SuitBrush, new MathRectangle { Left = -9, Right = -2, Bottom = 1, Top = 3, });
            paintManager.DrawRectangle(SuitPen, SuitBrush, new MathRectangle { Left = 2, Right = 9, Bottom = 1, Top = 3, }); 
            //Создание ног  
            paintManager.DrawRectangle(SuitPen, trousersBrush, new MathRectangle { Left = -2, Right = 0, Bottom = -5, Top = -1, });
            paintManager.DrawRectangle(SuitPen, trousersBrush, new MathRectangle { Left = 0, Right = 2, Bottom = -5, Top = -1, });
           
            var glassPen = new Pen(Color.Blue, 2);
            var glassBrush = new SolidBrush(Color.LightCyan);
           

            var wheelBrush = new SolidBrush(Color.Brown);
            var numbBrush =new SolidBrush(Color.YellowGreen);
            //Создание рук 
            paintManager.DrawEllipse(physiquePen, physiqueBrush, new MathRectangle { Left = -11, Right = -9, Bottom = 1, Top = 3, });
            paintManager.DrawEllipse(physiquePen, physiqueBrush, new MathRectangle { Left = 9, Right = 11, Bottom = 1, Top = 3, });
            //Создание головы 
            paintManager.DrawEllipse(physiquePen, physiqueBrush, new MathRectangle { Left = -2, Right = 2, Bottom = 4, Top = 8.25, });
            //Создание ниба 
            paintManager.DrawEllipse(mainPen, numbBrush, new MathRectangle { Left = -3, Right = 3, Bottom = 8.25, Top = 10.2, });
              //Создание глаз                                                                                                                   
             paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = -2, Right = -1, Bottom = 6, Top = 7, });
            paintManager.DrawEllipse(mainPen, wheelBrush, new MathRectangle { Left = 1, Right = 2, Bottom = 6, Top = 7, });

            var pilotBrush = new SolidBrush(Color.DarkRed);
            var botBrush = new SolidBrush(Color.LightGoldenrodYellow);
            //Создание ступень 
            paintManager.DrawPolygon(mainPen, botBrush,
                new[]
                {
                    new MathPoint{X = -2, Y= -3,},
                    new MathPoint{X = -2, Y= -5,},
                    new MathPoint{X = -5, Y= -5,},
                   
                });paintManager.DrawPolygon(mainPen, botBrush,
                new[]
                {
                    new MathPoint{X = 2, Y= -3,},
                    new MathPoint{X = 2, Y= -5,},
                    new MathPoint{X = 5, Y= -5,},
                   
                });
            //Создание артефакта 
            ;paintManager.DrawPolygon(mainPen, pilotBrush,
                new[]
                {
                    new MathPoint{X = -10, Y= 5,},
                    new MathPoint{X = -9, Y=3,},
                    new MathPoint{X = -11, Y= 3,},
                   
                });
        }
    }
}
