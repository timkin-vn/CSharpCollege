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
            var mainPen = new Pen(Color.Black, 3);
        

            var rect = new RectangleModel { X = -1, Y = 0, Width = 40, Height = 20, };
            

            var bodyBrush = Brushes.DarkGray;


            rect = new RectangleModel { X = -2, Y = 2, Width = 3, Height = 6, };// башня1
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = -6, Y = 2, Width = 3, Height = 6, };// башня2
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            

            rect = new RectangleModel { X = 3, Y = 2, Width = 6, Height = 5, }; // полукруг
            painter.DrawPie(bodyBrush, mainPen, rect, 180, 90);

            rect = new RectangleModel { X = 3.1, Y = 3.50, Width = 3.6, Height = 3.3, }; // полукруг окно кабины
            painter.DrawPie(Brushes.LightSkyBlue, mainPen, rect, 179, 97);

            rect = new RectangleModel { X = 6, Y = 4.5, Width = 5, Height = 2.5 };// корпус
            painter.DrawRectangle(bodyBrush, mainPen, rect);

            rect = new RectangleModel { X = -10, Y = -1, Width = 100, Height = 2 };// дорога
            painter.DrawRectangle(Brushes.RosyBrown, mainPen, rect);

            var sweeperBrush = Brushes.Red;
            var sweeperPoints = new[]
            {
                new PointModel { X = 11, Y = 2, },
                new PointModel { X = 12, Y = 2, },
                new PointModel { X = 13, Y = 0.5, },
                new PointModel { X = 11, Y = 0.5, },
            };

         

            var pipePoints = new[]
            {
                new PointModel { X = 8.5, Y = 9, },
                new PointModel { X = 10.5, Y = 9, },
                new PointModel { X = 10, Y = 6, },
                new PointModel { X = 9, Y = 6, },
            };

            

            var triangle1 = new[] // треугольник корпуса
            {
            new PointModel { X = 8, Y = 4.50 },
            new PointModel { X = 12, Y = 4.50 }, 
            new PointModel { X = 13, Y = 9 },
            
            
            };
            painter.DrawPolygon(Brushes.DarkGray, mainPen, triangle1);

            var triangle2 = new[]  // треугольник дальнего крыла
            {
            new PointModel { X = 7, Y = 7 },
            new PointModel { X = 9, Y = 7 },
            new PointModel { X = 9.5, Y = 9 },
            };
            painter.DrawPolygon(Brushes.DarkGray, mainPen, triangle2);

            var triangle3 = new[] // треугольник ближнего крыла
           {
            new PointModel { X = 10, Y = 3 },
            new PointModel { X = 6.8, Y = 5.8 },
            new PointModel { X = 9, Y = 5.8 },
            };
            painter.DrawPolygon(Brushes.DarkGray, mainPen, triangle3);
          ;

            var circleRect = new RectangleModel
            {
                X = -5.8,
                Y = 8.6,
                Width = 2.2,
                Height = 2.2
            };

            // Рисуем круг с жёлтой заливкой и чёрным контуром
            painter.DrawEllipse(Brushes.Yellow, mainPen, circleRect);

        }
    }
}
