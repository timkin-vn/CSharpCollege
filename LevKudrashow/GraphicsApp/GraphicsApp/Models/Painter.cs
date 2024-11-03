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
            Left = -2,
            Right = 2,
            Bottom = 0,
            Top = 3,
        };

        public void Paint(PaintManager paintManager)
        {

            var mainPen = new Pen(Color.Black, 3);
            var dashPen = new Pen(Color.Black, 3);
            dashPen.DashStyle = DashStyle.Dot;
            var whiteSmokeBrush = new SolidBrush(Color.WhiteSmoke);
            var lightGrayBrush = new SolidBrush(Color.LightGray);

            var waterPen = new Pen(Color.FromArgb(10, 0, 0, 255), 3);
            var waterBrush = new LinearGradientBrush(new System.Drawing.PointF(0, 0), new System.Drawing.PointF(10, 0), Color.FromArgb(50, 0, 0, 255), Color.FromArgb(10, 0, 0, 255));



            paintManager.DrawRectangle(mainPen, lightGrayBrush, new MathRectangle { Left = -2, Right = +2, Bottom = 0, Top = 3, });
            paintManager.DrawRectangle(mainPen, whiteSmokeBrush, new MathRectangle { Left = +1, Right = -1, Bottom = 1, Top = 3, });
            paintManager.DrawPolygon(mainPen, whiteSmokeBrush,
               new[]
               {
                    new MathPoint{X = -2, Y= 0,},
                    new MathPoint{X = -1, Y= 1,},
                    new MathPoint{X = 1, Y= 1,},
                    new MathPoint{X = 2, Y= 0,},
               });
            paintManager.DrawEllipse(mainPen, lightGrayBrush, new MathRectangle { Left = -0.2, Right = 0.2, Bottom = 0.5, Top = 0.75 });
            paintManager.DrawEllipse(dashPen, lightGrayBrush, new MathRectangle { Left = -0.1, Right = 0.1, Bottom = 0.57, Top = 0.7 });

            paintManager.DrawRectangle(mainPen, lightGrayBrush, new MathRectangle { Left = -0.1, Right = 0.1, Bottom = 2.6, Top = 3 });
            paintManager.DrawEllipse(mainPen, lightGrayBrush, new MathRectangle { Left = -0.4, Right = 0.4, Bottom = 2.5, Top = 2.75 });
            paintManager.DrawEllipse(dashPen, lightGrayBrush, new MathRectangle { Left = -0.3, Right = 0.3, Bottom = 2.57, Top = 2.7 });

            paintManager.DrawPolygon(waterPen, waterBrush,
               new[]
               {
                    new MathPoint{X = -0.4, Y= 2.65,},
                    new MathPoint{X = 0, Y= 2.75,},
                    new MathPoint{X = 0.4, Y= 2.65,},
                    new MathPoint{X = 0.6, Y= 0.5,},
                    new MathPoint{X = -0.6, Y= 0.5,}
               });
            paintManager.DrawEllipse(waterPen, waterBrush, new MathRectangle { Left = -0.6, Right = 0.6, Bottom = 0.3, Top = 0.8 });
        }
    }
}
