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
            Left = -20,
            Right = 19,
            Bottom = -4,
            Top = 15,
        };

        public void Paint(PaintManager paintManager)
        {
            var HomePen = new Pen(Color.Red, 3);
            var bodyBrush = new SolidBrush(Color.BurlyWood);  
            var HomePenLeft = new Pen(Color.BlueViolet, 3);
            var Lordbrush = new SolidBrush(Color.LemonChiffon);
            var mainPen = new Pen(Color.Purple, 3);  
            var GrdientPen = new Pen(Color.GreenYellow, 3);
            var solidBrush = new SolidBrush(Color.BlanchedAlmond); 
            var EmPen = new Pen(Color.DarkOliveGreen, 3);
            var recdBrush = new SolidBrush(Color.Bisque);  
            var MerPen = new Pen(Color.Chartreuse, 3);
            var OlerBrush = new SolidBrush(Color.Purple);
            var LorePen = new Pen(Color.Black, 3);
            var KerBrush = new SolidBrush(Color.Brown);
            paintManager.DrawRectangle(HomePen, bodyBrush, new MathRectangle { Left = -18, Right = -12, Bottom = 1, Top = 13 }); 
            paintManager.DrawRectangle(HomePen, bodyBrush, new MathRectangle { Left = -9, Right = -8, Bottom = 1, Top = 3.5 });  
            paintManager.DrawRectangle(HomePen, bodyBrush, new MathRectangle { Left = 6, Right = 8, Bottom = 1, Top = 5 });   
            paintManager.DrawRectangleGrad(GrdientPen, Color.Green,Color.Brown, new MathRectangle { Left = -20, Right = 19, Bottom = -4, Top = 0.96 });
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = -16, Right = -14, Bottom = 5, Top = 7 });
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = -16, Right = -14, Bottom = 9, Top = 11 });
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = -16, Right = -14, Bottom = 1.2, Top = 4 });
            paintManager.DrawRectangle(HomePen, bodyBrush, new MathRectangle { Left = -6, Right = -2, Bottom = 1, Top = 12 });
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = -5, Right = -3, Bottom = 1, Top = 4 }); 
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = -5, Right = -3, Bottom = 6, Top = 8 });
            paintManager.DrawRectangle(HomePenLeft, Lordbrush, new MathRectangle { Left = 6, Right = 8, Bottom = 1, Top = 5 });
            paintManager.DrawRectangle(HomePenLeft, Lordbrush, new MathRectangle { Left = 12, Right = 16, Bottom = 1, Top = 9 });   
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = 13, Right = 15, Bottom = 5, Top = 7 }); 
            paintManager.DrawRectangle(mainPen, solidBrush, new MathRectangle { Left = 13, Right = 15, Bottom = 1, Top = 4 });

            paintManager.DrawEllipse(EmPen, recdBrush, new MathRectangle { Left = 12, Right = 16, Bottom = 13, Top = 15, });
            paintManager.DrawEllipse(EmPen, recdBrush, new MathRectangle { Left = -5, Right = -3, Bottom = 9, Top = 11, });
            paintManager.DrawPolygon(MerPen, OlerBrush,
                new[]
                {
                    new MathPoint{X = -19, Y= 13,},
                    new MathPoint{X = -15, Y= 15,},
                    new MathPoint{X = -10, Y= 13,},

                });
                     paintManager.DrawPolygon(MerPen, OlerBrush,
                new[]
                {
                    new MathPoint{X = -6, Y= 12,},
                    new MathPoint{X = -4, Y= 15,},
                    new MathPoint{X = -2, Y= 12,},

                });  
            paintManager.DrawPolygon(MerPen, OlerBrush,
                new[]
                {
                    new MathPoint{X = -11, Y= 4,},
                    new MathPoint{X = -9, Y= 4,},
                    new MathPoint{X = -9, Y= 6,},

                });
            paintManager.DrawPolygon(LorePen, KerBrush,
                new[]
                {
                    
                    new MathPoint{X = -9, Y= 3,},
                    new MathPoint{X = -9, Y= 5,},

                });  
            paintManager.DrawPolygon(LorePen, KerBrush,
                new[]
                {
                    
                    new MathPoint{X = -15, Y= 9,},
                    new MathPoint{X = -15, Y= 11,},

                }); 
            

        }
    }
}
