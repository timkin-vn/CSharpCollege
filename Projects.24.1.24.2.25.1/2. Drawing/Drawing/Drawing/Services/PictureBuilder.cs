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
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 19,
            Height = 12,
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 2);

            
            var bodyBrush = Brushes.LightBlue;
            var fuselage = new RectangleModel { X = 0, Y = 3, Width = 12, Height = 2.5 };
            painter.DrawRectangle(bodyBrush, mainPen, fuselage);

         
            var cockpitBrush = Brushes.LightSkyBlue;
            var cockpit = new RectangleModel { X = 9, Y = 4, Width = 2, Height = 1.5 };
            painter.DrawRectangle(cockpitBrush, mainPen, cockpit);

           
            var windowBrush = Brushes.White;
            var window = new RectangleModel { X = 9.5, Y = 4.3, Width = 0.8, Height = 0.8 };
            painter.DrawRectangle(windowBrush, mainPen, window);

           
            var nosePoints = new[]
            {
                new PointModel { X = 14, Y = 4.25 },
                new PointModel { X = 12, Y = 4 },
                new PointModel { X = 12, Y = 4.5 },
                new PointModel { X = 14, Y = 4.25 },
            };
            painter.DrawPolygon(bodyBrush, mainPen, nosePoints);


          
            var tailHorizontalLeftPoints = new[]
            {
                new PointModel { X = -1, Y = 2 },
                new PointModel { X = 1, Y = 3 },
                new PointModel { X = 0, Y = 3 },
            };
            painter.DrawPolygon(bodyBrush, mainPen, tailHorizontalLeftPoints);

            var tailHorizontalRightPoints = new[]
            {
                new PointModel { X = -1, Y = 6.5 },
                new PointModel { X = 1, Y = 5.5 },
                new PointModel { X = 0, Y = 5.5 },
            };
            painter.DrawPolygon(bodyBrush, mainPen, tailHorizontalRightPoints);

 
            var wingBrush = Brushes.SteelBlue;


            var leftWingPoints = new[]
            {
                new PointModel { X = 4, Y = 3 },
                new PointModel { X = 2, Y = 1 },
                new PointModel { X = 5, Y = 2.5 },
                new PointModel { X = 6, Y = 3 },
            };
            painter.DrawPolygon(wingBrush, mainPen, leftWingPoints);

 
            var rightWingPoints = new[]
            {
                new PointModel { X = 4, Y = 5.5 },
                new PointModel { X = 2, Y = 7.5 },
                new PointModel { X = 5, Y = 6 },
                new PointModel { X = 6, Y = 5.5 },
            };
            painter.DrawPolygon(wingBrush, mainPen, rightWingPoints);

       
            var engineBrush = Brushes.Gray;

  
            var leftEngine = new RectangleModel { X = -1.5, Y = 3, Width = 1.5, Height = 0.8 };
            painter.DrawRectangle(engineBrush, mainPen, leftEngine);


            var rightEngine = new RectangleModel { X = -1.5, Y = 4.7, Width = 1.5, Height = 0.8 };
            painter.DrawRectangle(engineBrush, mainPen, rightEngine);


            var windowSmallBrush = Brushes.White;
            for (int i = 0; i < 4; i++)
            {
                var windowX = 2 + i * 1.8;
                var windowSmall = new RectangleModel { X = windowX, Y = 3.8, Width = 0.6, Height = 0.6 };
                painter.DrawRectangle(windowSmallBrush, mainPen, windowSmall);
            }
        }
    }
}