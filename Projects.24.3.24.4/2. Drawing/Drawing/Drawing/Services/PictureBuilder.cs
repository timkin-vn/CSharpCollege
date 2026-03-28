using Drawing.Models;
using Drawing.ViewServices;
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
            X = -0.5,
            Y = -0.5,
            Width = 26,
            Height = 11
        };

        public void DrawPicture(Painter painter)
        {
            var mainPen = new Pen(Color.Black, 1.5f); 

            int count = 0;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    var box = new RectangleModel { X = i * 5, Y = j * 5, Width = 5, Height = 5 };

                    painter.DrawRectangle(Pens.LightGray, null, box);

                    DrawArtByNumber(painter, count, box, mainPen);
                    count++;

                    if (count >= 10) return;
                }
            }
        }

        private void DrawArtByNumber(Painter painter, int num, RectangleModel b, Pen pen)
        {
            switch (num)
            {
                case 0: DrawAirplane(painter, b, pen); break;
                case 1: DrawHelicopter(painter, b, pen); break;
                case 2: DrawClock(painter, b, pen); break;
                case 3: DrawCar(painter, b, pen); break;
                case 4: DrawCloud(painter, b, pen); break;
                case 5: DrawTank(painter, b, pen); break;
                case 6: DrawLadder(painter, b, pen); break;
                case 7: DrawCamera(painter, b, pen); break;
                case 8: DrawLightBulb(painter, b, pen); break;
                case 9: DrawWindow(painter, b, pen); break;
            }
        }


        private void DrawAirplane(Painter painter, RectangleModel b, Pen p)
        {
            var fuselage = new RectangleModel { X = b.X + 0.5, Y = b.Y + 1.8, Width = 4, Height = 1.2 };
            painter.DrawEllipse(p, Brushes.WhiteSmoke, fuselage);

            painter.DrawPolygon(p, Brushes.LightGray, new[] {
                new PointModel { X = b.X + 1.5, Y = b.Y + 2.8 },
                new PointModel { X = b.X + 3.5, Y = b.Y + 2.8 },
                new PointModel { X = b.X + 2.8, Y = b.Y + 1 },
                new PointModel { X = b.X + 1.2, Y = b.Y + 1 }
            });


            var tail = new RectangleModel { X = b.X + 0.5, Y = b.Y + 2.8, Width = 0.8, Height = 1 };
            painter.DrawEllipse(p, Brushes.DarkRed, tail);
        }

        private void DrawHelicopter(Painter painter, RectangleModel b, Pen p)
        {

            var cabin = new RectangleModel { X = b.X + 1, Y = b.Y + 1.5, Width = 2.5, Height = 2 };
            painter.DrawEllipse(p, Brushes.LightSkyBlue, cabin);


            var tailBoom = new RectangleModel { X = b.X + 3, Y = b.Y + 2.2, Width = 1.5, Height = 0.5 };
            painter.DrawRectangle(p, Brushes.Gray, tailBoom);

            var rotorBase = new RectangleModel { X = b.X + 2, Y = b.Y + 3.4, Width = 0.5, Height = 0.2 };
            painter.DrawRectangle(p, Brushes.Black, rotorBase);
            painter.DrawEllipse(p, Brushes.DarkSlateGray, new RectangleModel { X = b.X + 0.5, Y = b.Y + 3.5, Width = 3.5, Height = 0.3 });


            painter.DrawRectangle(p, Brushes.Black, new RectangleModel { X = b.X + 1.2, Y = b.Y + 1, Width = 2.1, Height = 0.3 });
        }


        private void DrawClock(Painter painter, RectangleModel b, Pen p)
        {

            var frame = new RectangleModel { X = b.X + 0.5, Y = b.Y + 0.5, Width = 4, Height = 4 };
            painter.DrawEllipse(p, Brushes.Snow, frame);


            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 2.4, Y = b.Y + 3.8, Width = 0.2, Height = 0.2 }); // 12
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 3.8, Y = b.Y + 2.4, Width = 0.2, Height = 0.2 }); // 3
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 2.4, Y = b.Y + 1.0, Width = 0.2, Height = 0.2 }); // 6
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 1.0, Y = b.Y + 2.4, Width = 0.2, Height = 0.2 }); // 9

            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 2.4, Y = b.Y + 2.5, Width = 0.2, Height = 1.5 }); // Часовая
            painter.DrawEllipse(p, Brushes.Firebrick, new RectangleModel { X = b.X + 2.5, Y = b.Y + 2.4, Width = 1.5, Height = 0.2 }); // Минутная
        }


        private void DrawCar(Painter painter, RectangleModel b, Pen p)
        {
            var body = new RectangleModel { X = b.X + 0.5, Y = b.Y + 1.2, Width = 4, Height = 1.5 };
            painter.DrawRectangle(p, Brushes.Firebrick, body);

          
            var cabin = new RectangleModel { X = b.X + 1.2, Y = b.Y + 2.7, Width = 2.5, Height = 1.2 };
            painter.DrawRectangle(p, Brushes.LightCyan, cabin);

          
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 0.8, Y = b.Y + 0.5, Width = 1, Height = 1 });
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 3.2, Y = b.Y + 0.5, Width = 1, Height = 1 });

           
            painter.DrawPie(p, Brushes.Yellow, new RectangleModel { X = b.X + 4, Y = b.Y + 1.7, Width = 0.8, Height = 0.8 }, -30, 60);
        }

       
        private void DrawCloud(Painter painter, RectangleModel b, Pen p)
        {
           
            var brush = Brushes.White;
            painter.DrawEllipse(p, brush, new RectangleModel { X = b.X + 1, Y = b.Y + 1.5, Width = 2, Height = 2 });
            painter.DrawEllipse(p, brush, new RectangleModel { X = b.X + 2, Y = b.Y + 2, Width = 2, Height = 2 });
            painter.DrawEllipse(p, brush, new RectangleModel { X = b.X + 2.5, Y = b.Y + 1.5, Width = 1.5, Height = 1.5 });
            painter.DrawEllipse(p, brush, new RectangleModel { X = b.X + 1.5, Y = b.Y + 2.5, Width = 1.5, Height = 1.2 });
        }

       
        private void DrawTank(Painter painter, RectangleModel b, Pen p)
        {
            
            var tracks = new RectangleModel { X = b.X + 0.5, Y = b.Y + 0.5, Width = 4, Height = 1 };
            painter.DrawRectangle(p, Brushes.DarkOliveGreen, tracks);

           
            var body = new RectangleModel { X = b.X + 1, Y = b.Y + 1.5, Width = 3, Height = 1.2 };
            painter.DrawRectangle(p, Brushes.Olive, body);

           
            var turret = new RectangleModel { X = b.X + 1.5, Y = b.Y + 2.7, Width = 2, Height = 0.8 };
            painter.DrawRectangle(p, Brushes.DarkGreen, turret);

           
            painter.DrawRectangle(p, Brushes.DimGray, new RectangleModel { X = b.X + 3.5, Y = b.Y + 2.9, Width = 1.3, Height = 0.4 });
        }

 
        private void DrawLadder(Painter painter, RectangleModel b, Pen p)
        {
            var rail1 = new RectangleModel { X = b.X + 1.5, Y = b.Y + 0.5, Width = 0.3, Height = 4 };
            var rail2 = new RectangleModel { X = b.X + 3.2, Y = b.Y + 0.5, Width = 0.3, Height = 4 };
            painter.DrawRectangle(p, Brushes.Sienna, rail1);
            painter.DrawRectangle(p, Brushes.Sienna, rail2);

            for (float y = 1.0f; y <= 4.0f; y += 0.8f)
            {
                var step = new RectangleModel { X = b.X + 1.8, Y = b.Y + y, Width = 1.4, Height = 0.2 };
                painter.DrawRectangle(p, Brushes.SaddleBrown, step);
            }
        }


        private void DrawCamera(Painter painter, RectangleModel b, Pen p)
        {
            var body = new RectangleModel { X = b.X + 1, Y = b.Y + 1, Width = 3, Height = 2 };
            painter.DrawRectangle(p, Brushes.DarkSlateGray, body);

            var lensFrame = new RectangleModel { X = b.X + 1.8, Y = b.Y + 1.3, Width = 1.4, Height = 1.4 };
            painter.DrawEllipse(p, Brushes.Silver, lensFrame);
            painter.DrawEllipse(p, Brushes.Black, new RectangleModel { X = b.X + 2.1, Y = b.Y + 1.6, Width = 0.8, Height = 0.8 }); // Стекло

            painter.DrawRectangle(p, Brushes.LightGray, new RectangleModel { X = b.X + 1.2, Y = b.Y + 3, Width = 0.8, Height = 0.4 }); // Вспышка
            painter.DrawEllipse(p, Brushes.Red, new RectangleModel { X = b.X + 3.2, Y = b.Y + 3, Width = 0.4, Height = 0.4 }); // Кнопка
        }

     
        private void DrawLightBulb(Painter painter, RectangleModel b, Pen p)
        {
      
            var bulb = new RectangleModel { X = b.X + 1.5, Y = b.Y + 1.5, Width = 2, Height = 2.5 };
            painter.DrawEllipse(p, Brushes.LemonChiffon, bulb);

            painter.DrawRectangle(p, Brushes.Silver, new RectangleModel { X = b.X + 2, Y = b.Y + 0.5, Width = 1, Height = 1 });

            painter.DrawEllipse(p, Brushes.Orange, new RectangleModel { X = b.X + 2.2, Y = b.Y + 2.2, Width = 0.6, Height = 0.6 });
        }

        private void DrawWindow(Painter painter, RectangleModel b, Pen p)
        {
            var frame = new RectangleModel { X = b.X + 0.8, Y = b.Y + 0.8, Width = 3.4, Height = 3.4 };
            painter.DrawRectangle(p, Brushes.BurlyWood, frame);

            var glassBrush = Brushes.LightCyan;
            var paneSize = 1.4;
            painter.DrawRectangle(p, glassBrush, new RectangleModel { X = b.X + 1.1, Y = b.Y + 2.5, Width = paneSize, Height = paneSize }); // Топ-лев
            painter.DrawRectangle(p, glassBrush, new RectangleModel { X = b.X + 2.5, Y = b.Y + 2.5, Width = paneSize, Height = paneSize }); // Топ-прав
            painter.DrawRectangle(p, glassBrush, new RectangleModel { X = b.X + 1.1, Y = b.Y + 1.1, Width = paneSize, Height = paneSize }); // Бот-лев
            painter.DrawRectangle(p, glassBrush, new RectangleModel { X = b.X + 2.5, Y = b.Y + 1.1, Width = paneSize, Height = paneSize }); // Бот-прав
        }
    }
}