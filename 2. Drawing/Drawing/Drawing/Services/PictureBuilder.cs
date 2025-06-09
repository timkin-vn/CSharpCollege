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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = 0 , Width = 18, Y = -1, Height = 12, };

        public void BuildPicture(Painter painter)
        {
            
            var orbitPen = new Pen(Color.Black, 1);
            var satellitePen = new Pen(Color.Blue, 3);
            var planetPen = new Pen(Color.FromArgb(123, 45, 200), 2);

          
            var bigPlanetBrush = Brushes.Red;
            var starBrush = Brushes.Yellow;
            var satelliteBrush = Brushes.Gray;
            var smallPlanetBrush = new SolidBrush(Color.FromArgb(250, 80, 180));

           
            var bigPlanet = new RectangleModel { X = 5.5, Y = 4.5, Width = 3, Height = 3 };
            painter.DrawEllipse(bigPlanetBrush, planetPen, bigPlanet);

           
            var orbit1 = new RectangleModel { X = 4, Y = 3, Width = 6, Height = 6 };
            var orbit2 = new RectangleModel { X = 2.5, Y = 1.5, Width = 9, Height = 9 };
            painter.DrawEllipse(null, orbitPen, orbit1);
            painter.DrawEllipse(null, orbitPen, orbit2);

          
            var smallPlanet1 = new RectangleModel { X = 4, Y = 7.5, Width = 1, Height = 1 };
            painter.DrawEllipse(smallPlanetBrush, planetPen, smallPlanet1);

        
            var smallPlanet2 = new RectangleModel { X = 10.5, Y = 5, Width = 0.8, Height = 0.8 };
            painter.DrawEllipse(smallPlanetBrush, planetPen, smallPlanet2);

        
            var satellite = new RectangleModel { X = 9, Y = 2, Width = 1.2, Height = 0.5 };
            painter.DrawRectangle(satelliteBrush, satellitePen, satellite);

  
            painter.DrawPolygon(starBrush, null, CreateStar(0.5, 1, 0.5, 5));
            painter.DrawPolygon(starBrush, null, CreateStar(15, 2, 0.4, 6));
            painter.DrawPolygon(starBrush, null, CreateStar(1, 10, 0.6, 5));
            painter.DrawPolygon(starBrush, null, CreateStar(16, 10, 0.5, 6));

      
            var meteorPen = new Pen(Color.OrangeRed, 2);
            var meteorBrush = new SolidBrush(Color.FromArgb(220, 120, 30));
            var meteorRect = new RectangleModel { X = 12, Y = 3, Width = 1.5, Height = 0.5 };
            painter.DrawEllipse(meteorBrush, meteorPen, meteorRect);

         
            var flameColors = new[] { Color.Yellow, Color.Orange, Color.Red };
            var flameBaseX = 12;
            var flameBaseY = 3.25;
            var flameLength = 1.8;

            for (int i = 0; i < flameColors.Length; i++)
            {
                var flamePen = new Pen(flameColors[i], 2 - i * 0.5f);
                var angle = -0.2 + i * 0.2; 
                painter.DrawLine(flamePen,
                    new PointModel { X = flameBaseX, Y = flameBaseY },
                    new PointModel
                    {
                        X = flameBaseX - flameLength * Math.Cos(angle),
                        Y = flameBaseY + flameLength * Math.Sin(angle)
                    });
            }

       
            painter.DrawPolygon(new SolidBrush(Color.Yellow), null, new[]
            {
                new PointModel { X = flameBaseX, Y = flameBaseY },
                new PointModel { X = flameBaseX - 1.2, Y = flameBaseY + 0.25 },
                new PointModel { X = flameBaseX - 0.8, Y = flameBaseY - 0.3 }
            });
            painter.DrawPolygon(new SolidBrush(Color.Orange), null, new[]
            {
                new PointModel { X = flameBaseX, Y = flameBaseY },
                new PointModel { X = flameBaseX - 1.0, Y = flameBaseY + 0.15 },
                new PointModel { X = flameBaseX - 0.7, Y = flameBaseY - 0.2 }
            });

       
            var tailPen = new Pen(Color.Gold, 2);
            painter.DrawLine(tailPen,
                new PointModel { X = 12, Y = 3.25 },
                new PointModel { X = 10.2, Y = 4.1 });
        }

   
        private PointModel[] CreateStar(double cx, double cy, double r, int points)
        {
            var result = new List<PointModel>();
            double angleStep = Math.PI / points;
            for (int i = 0; i < points * 2; i++)
            {
                double angle = i * angleStep;
                double radius = (i % 2 == 0) ? r : r / 2.2;
                result.Add(new PointModel
                {
                    X = cx + Math.Cos(angle) * radius,
                    Y = cy + Math.Sin(angle) * radius
                });
            }
            return result.ToArray();
        }
    }
}
