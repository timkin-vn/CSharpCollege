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
            Left = -19,
            Right = 20,
            Bottom = -7,
            Top = 9,
        };

        public void Paint(PaintManager paintManager)
        {
            var mainPen = new Pen(Color.Black, 3);
            var bodyBrush = new SolidBrush(Color.BlanchedAlmond); 
            var WindunPen = new Pen(Color.Lime, 3);
            var LegBrush = new SolidBrush(Color.OldLace);
            var NOvePen = new Pen(Color.Peru, 3);
            var GetBrush = new SolidBrush(Color.AliceBlue); 
            var MafnPen = new Pen(Color.DarkKhaki, 3);
            var LuctyBrush = new SolidBrush(Color.DarkBlue); 
            var MуfnPen = new Pen(Color.DarkGreen, 3);
            var LsetyBrush = new SolidBrush(Color.DarkOliveGreen);
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -18, Right = -15,  Bottom = -4,Top = 6, });  
            paintManager.DrawRectangleGrad(mainPen, Color.DarkGreen,Color.DarkSalmon, new MathRectangle { Left = -19, Right = 20,  Bottom = -7,Top = -4, });   
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -11, Right = -8,  Bottom = -4,Top = 4, });  
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 15, Right = 19,  Bottom = -4,Top =5, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = 16, Right = 18, Bottom = -1, Top = 3, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = 16, Right = 18, Bottom = -1, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -10, Right = -9, Bottom = -3.7, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -10, Right = -9, Bottom = 1.3, Top = 2, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -17, Right = -16, Bottom = -3.7, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -17, Right = -16, Bottom = 1, Top = 3, });

            paintManager.DrawEllipse(mainPen, LegBrush, new MathRectangle { Left = 1, Right = 5, Bottom = 5, Top = 9, });
                   
                        
            
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = -18, Y=  6.1,},
                    new MathPoint{X = -16.5, Y= 9,},
                    new MathPoint{X = -15, Y= 6.1,},
                    
                }); 
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = -11, Y=  4,},
                    new MathPoint{X = -9.5, Y= 6,},
                    new MathPoint{X = -8, Y= 4,},
                    
                }); 
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = 15, Y=  5,},
                    new MathPoint{X = 17, Y= 8,},
                    new MathPoint{X = 19, Y= 5,},
                    
                });
        }
    }
}
