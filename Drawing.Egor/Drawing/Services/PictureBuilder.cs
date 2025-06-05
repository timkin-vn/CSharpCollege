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
        public RectangleModel PictureBounds { get; } = new RectangleModel { X = -5, Width = 25, Y = 0, Height = 15 };

        public void BuildPicture(Painter painter)
        {
            // Фон (небо и земля)
            painter.DrawRectangle(Brushes.SkyBlue, null,
                new RectangleModel { X = -5, Y = 10, Width = 30, Height = 5 }); // Небо 
            painter.DrawRectangle(Brushes.LightGreen, null,
                new RectangleModel { X = -5, Y = 0, Width = 30, Height = 10 });  // Земля 

            // Солнце 
            var sunBrush = Brushes.Gold;
            var sunPen = new Pen(Color.Goldenrod, 1);
            painter.DrawEllipse(sunBrush, sunPen,
                new RectangleModel { X = 18, Y = 11, Width = 3, Height = 3 });

            // Лучи солнца 
            for (int i = 0; i < 8; i++)
            {
                double angle = i * Math.PI / 4;
                double x1 = 19.5 + Math.Cos(angle) * 2;
                double y1 = 12.5 + Math.Sin(angle) * 2;
                double x2 = 19.5 + Math.Cos(angle) * 3;
                double y2 = 12.5 + Math.Sin(angle) * 3;
                painter.DrawLine(sunPen, new PointModel { X = x1, Y = y1 }, new PointModel { X = x2, Y = y2 });
            }

            // Горы 
            var mountainBrush = Brushes.DarkGray;
            var mountainPen = new Pen(Color.DarkSlateGray, 2);

            var mountainPoints1 = new[]
            {
                new PointModel { X = 0, Y = 5 },  // Основание
                new PointModel { X = 5, Y = 10 },   // Вершина
                new PointModel { X = 10, Y = 5 } // Основание
            };
            painter.DrawPolygon(mountainBrush, mountainPen, mountainPoints1);

            var mountainPoints2 = new[]
            {
                new PointModel { X = 8, Y = 5 },
                new PointModel { X = 13, Y = 11 },
                new PointModel { X = 18, Y = 5 }
            };
            painter.DrawPolygon(mountainBrush, mountainPen, mountainPoints2);

            // Озеро
            painter.DrawEllipse(Brushes.DodgerBlue, null,
                new RectangleModel { X = -3, Y = 2, Width = 20, Height = 3 });

            // Кораблик
            var shipBrush = Brushes.SaddleBrown;
            var shipPen = new Pen(Color.Black, 1);

            // Корпус корабля
            var shipPoints = new[]
            {
                new PointModel { X = 7, Y = 4 },
                new PointModel { X = 12, Y = 4 },
                new PointModel { X = 11, Y = 3 },
                new PointModel { X = 8, Y = 3 }
            };
            painter.DrawPolygon(shipBrush, shipPen, shipPoints);

            // Мачта
            painter.DrawLine(shipPen, new PointModel { X = 9.5, Y = 4 }, new PointModel { X = 9.5, Y = 7 });

            // Парус
            painter.DrawPolygon(Brushes.White, shipPen, new[]
            {
                new PointModel { X = 9.5, Y = 4.2 },
                new PointModel { X = 11.5, Y = 4.2 },
                new PointModel { X = 9.5, Y = 7 }
            });

            // Птички (символы "V" в небе)
            var birdPen = new Pen(Color.Black, 1);
            DrawBird(painter, birdPen, 10, 10);
            DrawBird(painter, birdPen, 12, 13);
            DrawBird(painter, birdPen, 14, 13);
            DrawBird(painter, birdPen, 12, 11);
            // Облака
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = 2, Y = 13, Width = 3, Height = 1.5 });
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = 5.5, Y = 11.5, Width = 3, Height = 1.5 });
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = -3, Y = 12, Width = 3, Height = 1.5 });
            painter.DrawEllipse(Brushes.White, null, new RectangleModel { X = 13, Y = 13.5, Width = 3, Height = 1.5 });
        }
        private void DrawBird(Painter painter, Pen pen, double x, double y)
        {
            
            painter.DrawLine(pen, new PointModel { X = x, Y = y }, new PointModel { X = x - 0.5, Y = y + 0.5 });
            painter.DrawLine(pen, new PointModel { X = x, Y = y }, new PointModel { X = x + 0.5, Y = y + 0.5 });
        }
    }
}