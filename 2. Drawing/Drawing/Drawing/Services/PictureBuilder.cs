using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Drawing.Services
{
    internal class PictureBuilder
    {
        public RectangleModel SourceBounds { get; } = new RectangleModel
        {
            X = -5,
            Y = -1,
            Width = 20,
            Height = 15
        };

        public void DrawPicture(Painter painter)
        {
            var penBlack = new Pen(Color.Black, 2); // обычно перо
            var penWindow = new Pen(Color.FromArgb(0, 120, 180), 1); // перо для окн
            var penStickMan = new Pen(Color.Black, 2); // перо для человека

            var houseBrush = new SolidBrush(Color.BurlyWood); // кисть для дома
            var roofBrush = new SolidBrush(Color.Red);  // кисть для крыши
            var doorBrush = new SolidBrush(Color.Goldenrod);    // кисть для двери
            var windowBrush = new SolidBrush(Color.LightBlue);  // кисть для окна
            var chimneyBrush = Brushes.Black;   // кисть для трубы
            var smokeBrush = Brushes.Gray;  // кисть для дыма

            // Дом
            var house = new RectangleModel { X = 0, Y = 0, Width = 6, Height = 6 };
            painter.DrawRectangle(houseBrush, penBlack, house);

            // Труба
            var chimney = new RectangleModel { X = 5.2, Y = 6.5, Width = 0.4, Height = 1.2 };
            painter.DrawRectangle(chimneyBrush, penBlack, chimney);

            // Дым 
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.2, Y = 7.8, Width = 0.3, Height = 0.3 });
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.3, Y = 8.2, Width = 0.4, Height = 0.4 });
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.4, Y = 8.7, Width = 0.5, Height = 0.5 });

            // Крыша
            var roof = new[]
            {
                new PointModel { X = 0, Y = 6 },
                new PointModel { X = 3, Y = 9 },
                new PointModel { X = 6, Y = 6 },
            };
            painter.DrawPolygon(roofBrush, penBlack, roof);

            // Маленькое круглое окно
            var roundWindow = new RectangleModel { X = 2.5, Y = 7.3, Width = 1, Height = 1 };
            painter.DrawEllipse(windowBrush, penBlack, roundWindow);
            painter.DrawLine(penBlack, new PointModel { X = 3, Y = 7.3 }, new PointModel { X = 3, Y = 8.3 });
            painter.DrawLine(penBlack, new PointModel { X = 2.5, Y = 7.8 }, new PointModel { X = 3.5, Y = 7.8 });

            // Окна первого этажа
            var leftWindow = new RectangleModel { X = 0.6, Y = 3.5, Width = 1.5, Height = 1.5 };
            var rightWindow = new RectangleModel { X = 3.9, Y = 3.5, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(windowBrush, penWindow, leftWindow);
            painter.DrawRectangle(windowBrush, penWindow, rightWindow);

            // Перекладины окон
            painter.DrawLine(penWindow, new PointModel { X = 0.6 + 0.75, Y = 3.5 }, new PointModel { X = 0.6 + 0.75, Y = 5 });
            painter.DrawLine(penWindow, new PointModel { X = 0.6, Y = 4.25 }, new PointModel { X = 2.1, Y = 4.25 });

            painter.DrawLine(penWindow, new PointModel { X = 3.9 + 0.75, Y = 3.5 }, new PointModel { X = 3.9 + 0.75, Y = 5 });
            painter.DrawLine(penWindow, new PointModel { X = 3.9, Y = 4.25 }, new PointModel { X = 5.4, Y = 4.25 });

            // Дверь
            var door = new RectangleModel { X = 2.4, Y = 0, Width = 1.2, Height = 2 };
            painter.DrawRectangle(doorBrush, penBlack, door);
            var handle = new RectangleModel { X = 3.4, Y = 1, Width = 0.1, Height = 0.1 };
            painter.DrawEllipse(Brushes.Black, null, handle);

            // Ступеньки справа от двери
            painter.DrawRectangle(houseBrush, penBlack, new RectangleModel { X = 6.1, Y = 0, Width = 0.6, Height = 0.6 });
            painter.DrawRectangle(houseBrush, penBlack, new RectangleModel { X = 6.7, Y = 0, Width = 0.6, Height = 0.3 });

            // Человечек
            var head = new RectangleModel { X = -3.2, Y = 3.5, Width = 0.8, Height = 0.8 };
            painter.DrawEllipse(null, penStickMan, head);
            painter.DrawLine(penStickMan, new PointModel { X = -2.8, Y = 3.5 }, new PointModel { X = -2.8, Y = 2.2 });
            painter.DrawLine(penStickMan, new PointModel { X = -2.8, Y = 2.8 }, new PointModel { X = -3.5, Y = 3.1 });
            painter.DrawLine(penStickMan, new PointModel { X = -2.8, Y = 2.8 }, new PointModel { X = -2.1, Y = 3.1 });
            painter.DrawLine(penStickMan, new PointModel { X = -2.8, Y = 2.2 }, new PointModel { X = -3.4, Y = 1.5 });
            painter.DrawLine(penStickMan, new PointModel { X = -2.8, Y = 2.2 }, new PointModel { X = -2.2, Y = 1.5 });
        }
    }
}