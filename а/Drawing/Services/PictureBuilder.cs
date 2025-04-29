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
            var backgroundBrush = new SolidBrush(Color.FromArgb(10, 10, 30)); 
            var background = new RectangleModel { X = -1000, Y = -1000, Width = 2000, Height = 2000 };
            painter.DrawRectangle(Brushes.Black, new Pen(Color.Black, 1), background);

            var mainPen = new Pen(Color.Black, 2);
            var flamePen = new Pen(Color.Red, 1);
            var customPen = new Pen(Color.FromArgb(100, 100, 255), 3); 

            
            var bodyBrush = Brushes.Gray;
            var windowBrush = Brushes.LightBlue;
            var flameBrush = Brushes.Orange;
            var noseBrush = Brushes.DarkRed;

            
            var body = new RectangleModel { X = 2, Y = 2, Width = 2, Height = 6 };
            painter.DrawRectangle(bodyBrush, mainPen, body);

            
            var nose = new PointModel[]
            {
new PointModel { X = 2, Y = 8 },
new PointModel { X = 4, Y = 8 },
new PointModel { X = 3, Y = 10 }
            };
            painter.DrawPolygon(noseBrush, customPen, nose);

            
            var window = new RectangleModel { X = 2.75, Y = 5.5, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(windowBrush, mainPen, window);

            var window2 = new RectangleModel { X = 2.75, Y = 6.5, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(windowBrush, mainPen, window2);

            var window3 = new RectangleModel { X = 2.75, Y = 4.5, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(windowBrush, mainPen, window3);

            var window4 = new RectangleModel { X = 2.75, Y = 3.5, Width = 0.5, Height = 0.5 };
            painter.DrawEllipse(windowBrush, mainPen, window4);

            
            var leftWing = new RectangleModel { X = 1.5, Y = 2, Width = 0.5, Height = 2 };
            painter.DrawRectangle(bodyBrush, mainPen, leftWing);

            
            var rightWing = new RectangleModel { X = 4, Y = 2, Width = 0.5, Height = 2 };
            painter.DrawRectangle(bodyBrush, mainPen, rightWing);

            
            var flame = new PointModel[]
            {
new PointModel { X = 2, Y = 2 },
new PointModel { X = 2.5, Y = 0.5 },
new PointModel { X = 3, Y = 2 },
new PointModel { X = 3.5, Y = 0.5 },
new PointModel { X = 4, Y = 2 }
            };
            painter.DrawPolygon(flameBrush, flamePen, flame);

            
            painter.DrawLine(mainPen, new PointModel { X = 2, Y = 2 }, new PointModel { X = 2, Y = 8 });
            painter.DrawLine(mainPen, new PointModel { X = 4, Y = 2 }, new PointModel { X = 4, Y = 8 });

            var rand = new Random();

            
            var starBrush = Brushes.White;
            var starPen = new Pen(Color.White, 1);


            int starCount = 200; 
            for (int i = 0; i < starCount; i++)
            {
                double x, y;

                
                do
                {
                    double minX = -10;
                    double maxX = 20;
                    double minY = -10;
                    double maxY = 20;

                    x = minX + rand.NextDouble() * (maxX - minX);
                    y = minY + rand.NextDouble() * (maxY - minY);

                } while (x >= 1.5 && x <= 4.5 && y >= 0 && y <= 10); 

                double size = 0.1 + rand.NextDouble() * 0.1; 
                var star = new RectangleModel { X = x, Y = y, Width = size, Height = size };
                painter.DrawEllipse(starBrush, starPen, star);
            }


        }
    }
}
