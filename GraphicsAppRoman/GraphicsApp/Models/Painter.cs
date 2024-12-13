﻿using GraphicsApp.Services;
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
            paintManager.DrawRectangle(NOvePen, GetBrush, new MathRectangle { Left = 10, Right = 12,  Bottom = -4,Top =5, }); 
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 15, Right = 19,  Bottom = -4,Top =5, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = 16, Right = 18, Bottom = -1, Top = 3, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = 16, Right = 18, Bottom = -1, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -10, Right = -9, Bottom = -3.7, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -10, Right = -9, Bottom = 1.3, Top = 2, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -17, Right = -16, Bottom = -3.7, Top = 1, });
            paintManager.DrawRectangle(MafnPen, LuctyBrush, new MathRectangle { Left = -17, Right = -16, Bottom = 1, Top = 3, });

            paintManager.DrawEllipse(mainPen, LegBrush, new MathRectangle { Left = 1, Right = 5, Bottom = 5, Top = 9, })
                ;paintManager.DrawEllipse(mainPen, LegBrush, new MathRectangle { Left = 3, Right = 7, Bottom = -4, Top = -2, });   
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = 10, Y=  7,},
                    new MathPoint{X = 10, Y= 5,},
                    
                    
                });  
            paintManager.DrawPolygon(MуfnPen, LsetyBrush,
                new[]
                {
                    new MathPoint{X = 10, Y=  7,},
                    new MathPoint{X = 10, Y= 6,}, 
                    new MathPoint{X = 9, Y= 6,},
                    
                    
                });  
            paintManager.DrawPolygon(MуfnPen, LsetyBrush,
                new[]
                {
                    new MathPoint{X = 12, Y=  7,},
                    new MathPoint{X = 12, Y= 6,}, 
                    new MathPoint{X = 14, Y= 6,},
                    
                    
                });  
            paintManager.DrawPolygon(MуfnPen, LsetyBrush,
                new[]
                {
                   
                    new MathPoint{X = 1, Y= -2,}, 
                    new MathPoint{X = 2, Y= -3,},
                     new MathPoint{X = 3, Y=  -1,},


                });
            paintManager.DrawPolygon(MуfnPen, LsetyBrush,
                new[]
                {
                    new MathPoint{X = 12, Y=  7,},
                    new MathPoint{X = 12, Y= 6,}, 
                    new MathPoint{X = 14, Y= 6,},
                    
                    
                });
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = 12, Y=  7,},
                    new MathPoint{X = 12, Y= 5,},
                    
                    
                }); 
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = -19, Y=  6.1,},
                    new MathPoint{X = -15, Y= 9,},
                    new MathPoint{X = -13, Y= 6.1,},
                    
                }); 
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = -12, Y=  4,},
                    new MathPoint{X = -9, Y= 6,},
                    new MathPoint{X = -5, Y= 4,},
                    
                }); 
            paintManager.DrawPolygon(WindunPen, LegBrush,
                new[]
                {
                    new MathPoint{X = 15, Y=  5,},
                    new MathPoint{X = 17, Y= 9,},
                    new MathPoint{X = 20, Y= 5,},
                    
                });
        }
    }
}
