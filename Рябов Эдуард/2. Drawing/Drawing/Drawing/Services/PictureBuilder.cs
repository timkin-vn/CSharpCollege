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


            var RoyalBlue = new Pen(Color.RoyalBlue, 6);
            var CadetBlue = new Pen(Color.CadetBlue, 3);
            var Blue = new Pen(Color.Blue, 4);


            Color DeepSkyBlue = Color.FromArgb(0, 191, 255); 
            SolidBrush rgbBrushWindow = new SolidBrush(DeepSkyBlue);

            Color Red = Color.FromArgb(255, 0, 0);
            SolidBrush rgbBrushflag = new SolidBrush(Red);


            Color DodgerBlue = Color.FromArgb(30, 144, 255);
            SolidBrush rgbBrushBody = new SolidBrush(DodgerBlue);

            Color SaddleBrown = Color.FromArgb(139, 69, 19);
            SolidBrush rgbBrushFlag = new SolidBrush(SaddleBrown);

            Color SteelBluee = Color.FromArgb(70, 130, 180);
            SolidBrush rgbBrushBort = new SolidBrush(SteelBluee);

            Color RosyBrown = Color.FromArgb(188, 143, 143);
            SolidBrush rgbBrushHead = new SolidBrush(RosyBrown);


            var mainPen = new Pen(Color.Black, 3);
            

            var BodyRectangle = new RectangleModel { X = 0, Y = 0, Width = 9, Height = 5, };
            painter.DrawRectangle(rgbBrushBody, RoyalBlue, BodyRectangle);

            var flagRectangle = new RectangleModel { X = 4, Y = 5, Width = 0.5, Height = 4, };
            painter.DrawRectangle(rgbBrushFlag, mainPen, flagRectangle);



            var windowRectangle = new RectangleModel { X = 1, Y = 3, Width = 1, Height = 1, };
            painter.DrawEllipse(rgbBrushWindow, mainPen, windowRectangle);

            windowRectangle.X += 2;
            painter.DrawEllipse(rgbBrushWindow, mainPen, windowRectangle);

            windowRectangle.X += 2;
            painter.DrawEllipse(rgbBrushWindow, mainPen, windowRectangle);

            windowRectangle.X += 2;
            painter.DrawEllipse(rgbBrushWindow, mainPen, windowRectangle);


            var headRectangle = new RectangleModel { X = 0, Y = 4, Width = 9, Height = 2, };
            painter.DrawPie(rgbBrushHead, Blue, headRectangle, 180, 180);


            var flagPoints = new[]
            {
                new PointModel { X = 4.5, Y = 9, },
                new PointModel { X = 10, Y = 9, },
                new PointModel { X = 10, Y = 8, },
                new PointModel { X = 4.5, Y = 7, },
            };
            painter.DrawPolygon(rgbBrushflag, mainPen, flagPoints);

            var rightPoints = new[]
            {
                new PointModel { X = 9, Y = 0, },
                new PointModel { X = 12, Y = 6, },
                new PointModel { X = 11, Y = 6, },
                new PointModel { X = 9, Y = 5, },
            };
            painter.DrawPolygon(rgbBrushBort, CadetBlue, rightPoints);


            var leftPoints = new[]
            {
                new PointModel { X = 0, Y = 0, },
                new PointModel { X = -3, Y = 6, },
                new PointModel { X = -2, Y = 6, },
                new PointModel { X = 0, Y = 5, },
            };
            painter.DrawPolygon(rgbBrushBort, CadetBlue, leftPoints);


        }
    }
}
