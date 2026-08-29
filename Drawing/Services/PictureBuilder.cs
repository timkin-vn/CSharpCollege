using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 18, Y = -1, Height = 12, };

        public void BuildPicture(Painter painter)
        {

            var mainPen = new Pen(Color.Black, 3);
            var wheelBrush = Brushes.LightGray;
            var BtnBrush = Brushes.Green;
            var BtnBrush2 = Brushes.Red;
            var BtnBrush3 = Brushes.Black;
            var EyeBrush = Brushes.Red;

            var thinRedPen = new Pen(Color.Red, 1);
            var thickBluePen = new Pen(Color.Blue, 5);

            var customColor = Color.FromArgb(120, 80, 200);
            var customPen = new Pen(customColor, 2);
            var headPen = new Pen(Color.Black, 3);

            var mouthBrush = Brushes.Orange;
            var detailPen = new Pen(Color.DarkGray, 1);
            var wheelPen = new Pen(Color.DarkSlateGray, 2);
            var patternBrush = Brushes.Yellow;



            var legRectangle = new RectangleModel { X = 1, Y = 0, Width = 1, Height = 3, };
            painter.DrawRectangle(wheelBrush, mainPen, legRectangle);

            var legRectangle2 = new RectangleModel { X = 3, Y = 0, Width = 1, Height = 3, };
            painter.DrawRectangle(wheelBrush, mainPen, legRectangle2);

            var Body = new RectangleModel { X = 0.5, Y = 3, Width = 4, Height = 5, };
            painter.DrawRectangle(wheelBrush, mainPen, Body);

            var armRectangle = new RectangleModel { X = -0.5, Y = 4, Width = 1, Height = 3, };
            painter.DrawRectangle(wheelBrush, mainPen, armRectangle);

            var armRectangle2 = new RectangleModel { X = 4.5, Y = 4, Width = 1, Height = 3, };
            painter.DrawRectangle(wheelBrush, mainPen, armRectangle2);

            var Head = new RectangleModel { X = 0.5, Y = 7, Width = 4, Height = 2, };
            painter.DrawRectangle(wheelBrush, headPen, Head);

            var eye = new RectangleModel { X = 3, Y = 8, Width = 0.5, Height = 0.5, };
            painter.DrawEllipse(EyeBrush, thinRedPen, eye);

            var eye2 = new RectangleModel { X = 1, Y = 8, Width = 0.5, Height = 0.5, };
            painter.DrawEllipse(EyeBrush, thinRedPen, eye2);

            var antenna = new RectangleModel { X = 2.5, Y = 9, Width = 0.1, Height = 1 };
            var start = new PointModel { X = antenna.X + antenna.Width / 2, Y = antenna.Y };
            var end = new PointModel { X = antenna.X + antenna.Width / 2, Y = antenna.Y + antenna.Height };
            painter.DrawLine(customPen, start, end);

            var point = new RectangleModel { X = 2.3, Y = 10, Width = 0.5, Height = 0.5, };
            painter.DrawEllipse(EyeBrush, mainPen, point);

            var mouth = new RectangleModel { X = 1.8, Y = 7.2, Width = 1.5, Height = 0.8 };
            painter.DrawPie(mouthBrush, customPen, mouth, 0, 180);

            var Button = new RectangleModel { X = 3.8, Y = 6, Width = 0.5, Height = 0.5, };
            painter.DrawRectangle(BtnBrush, thickBluePen, Button);

            var Button2 = new RectangleModel { X = 3.8, Y = 5, Width = 0.5, Height = 0.5, };
            painter.DrawRectangle(BtnBrush3, thickBluePen, Button2);

            var Button3 = new RectangleModel { X = 3.8, Y = 4, Width = 0.5, Height = 0.5, };
            painter.DrawRectangle(BtnBrush2, thickBluePen, Button3);

            
            

            
            var antenna2 = new RectangleModel { X = 1.9, Y = 9, Width = 0.1, Height = 1 };
            var start2 = new PointModel { X = antenna2.X + antenna2.Width / 2, Y = antenna2.Y };
            var end2 = new PointModel { X = antenna2.X + antenna2.Width / 2, Y = antenna2.Y + antenna2.Height };
            painter.DrawLine(customPen, start2, end2);

            
            var point2 = new RectangleModel { X = 1.7, Y = 10, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(EyeBrush, mainPen, point2);

            
            var wheel1 = new RectangleModel { X = 0.5, Y = -0.5, Width = 2, Height = 1 };
            painter.DrawEllipse(Brushes.DarkGray, wheelPen, wheel1);

            var wheel2 = new RectangleModel { X = 2.5, Y = -0.5, Width = 2, Height = 1 };
            painter.DrawEllipse(Brushes.DarkGray, wheelPen, wheel2);

            
            var pattern1 = new RectangleModel { X = 1, Y = 4, Width = 0.5, Height = 0.5 };
            painter.DrawRectangle(patternBrush, detailPen, pattern1);

            var pattern2 = new RectangleModel { X = 1, Y = 5, Width = 0.5, Height = 0.5 };
            painter.DrawRectangle(patternBrush, detailPen, pattern2);

            var pattern3 = new RectangleModel { X = 3, Y = 4, Width = 0.5, Height = 0.5 };
            painter.DrawRectangle(patternBrush, detailPen, pattern3);

            var pattern4 = new RectangleModel { X = 3, Y = 5, Width = 0.5, Height = 0.5 };
            painter.DrawRectangle(patternBrush, detailPen, pattern4);

            
            var screen = new RectangleModel { X = 1.5, Y = 4, Width = 1.5, Height = 2 };
            painter.DrawRectangle(Brushes.Cyan, new Pen(Color.DarkBlue, 1), screen);

            
            

            

        }
    }
}
