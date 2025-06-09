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
            //основание дома
            var mainPen = new Pen(Color.Black, 6);
            var bodyBrush = new SolidBrush(Color.RosyBrown);
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -4, Right = 4, Bottom = 1, Top = 7 });
          
            //окна дома
            var glassPen = new Pen(Color.Aqua, 2);
            var glassBrush = new SolidBrush(Color.LightCyan);
            paintManager.DrawEllipse(glassPen, glassBrush, new MathRectangle { Left = -2.5, Right = -0.5, Bottom = 3, Top = 5, });


            //дверь
            var mainPen2 = new Pen(Color.Black, 6);
            var bodyBrush2 = new SolidBrush(Color.BurlyWood);
            paintManager.DrawRectangle(mainPen2, bodyBrush2, new MathRectangle { Left = 2, Right = 4, Bottom = 1, Top = 4 });

            //крыша
            var triangleBrush = new SolidBrush(Color.DarkRed);

            var trianglePoints = new[]
            {
               new MathPoint { X = -4.5, Y = 7 }, //точка находится в левой верхней части треугольника
               new MathPoint { X = 4.5, Y = 7 }, //точка находится в правой верхней части треугольника.
               new MathPoint { X = 0, Y = 8.9 } //нижней части треугольника, в точке пересечения его боковых сторон
             };
            paintManager.DrawPolygon(mainPen, triangleBrush, trianglePoints);

            //землля 1й ур
            var groungPen = new Pen(Color.Black, 6);
            var groungBrush = new SolidBrush(Color.DarkOliveGreen);
            paintManager.DrawRectangle(groungPen, groungBrush, new MathRectangle { Left = -7, Right = 8, Bottom = -0.001, Top = 1 });

            //землля 2й ур
            var groungPen2 = new Pen(Color.Black, 6);
            var groungBrush2 = new SolidBrush(Color.SaddleBrown);
            paintManager.DrawRectangle(groungPen2, groungBrush2, new MathRectangle { Left = -6, Right = 7, Bottom = -0, Top = -2 });

            //дерево
            var TreePen = new Pen(Color.Black, 6);
            var treeBrush = new SolidBrush(Color.SaddleBrown);
            paintManager.DrawRectangle(TreePen, treeBrush, new MathRectangle { Left = 7, Right = 7.6, Bottom = 1, Top = 5 });
            paintManager.DrawRectangle(TreePen, treeBrush, new MathRectangle { Left = -5.9, Right = -6.5, Bottom = 1, Top = 5 });

            //листья 
            var trBrush = new SolidBrush(Color.DarkSeaGreen);
            var trianglePoints2 = new[]
           {
               new MathPoint { X = 7.3, Y = 7 }, //точка находится в левой верхней части треугольника
               new MathPoint { X = 6, Y = 3.5 }, //точка находится в правой верхней части треугольника.
               new MathPoint { X = 8.5, Y = 3.5 } //нижней части треугольника, в точке пересечения его боковых сторон
             };
            paintManager.DrawPolygon(mainPen, trBrush, trianglePoints2);

            
            var trianglePoints22 = new[]
           {
               new MathPoint { X = -6.2, Y = 7 }, //точка находится в левой верхней части треугольника
               new MathPoint { X = -4.9, Y = 3.5 }, //точка находится в правой верхней части треугольника.
               new MathPoint { X = -7.4, Y = 3.5 } //нижней части треугольника, в точке пересечения его боковых сторон
             };
            paintManager.DrawPolygon(mainPen, trBrush, trianglePoints22);
        }
    }
}
