using Drawing.Models;
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
            Height = 15,
        };

        public void DrawPicture(Painter painter)
        {
            // Пера
            var blackPen = new Pen(Color.Black, 2);
            var redPen = new Pen(Color.Red, 3);
            var customPen = new Pen(Color.FromArgb(128, 64, 0), 1.5f); // нестандартный цвет

            // Кисти
            var brownBrush = new SolidBrush(Color.SaddleBrown);
            var skinBrush = new SolidBrush(Color.FromArgb(255, 220, 177)); // нестандартный цвет
            var darkBrush = Brushes.DarkSlateGray;

            // === ЛОШАДЬ ===
            // Тело
            var body = new RectangleModel { X = 0, Y = 2, Width = 6, Height = 2 };
            painter.DrawRectangle(brownBrush, blackPen, body);

            // Голова
            var head = new RectangleModel { X = -1.5, Y = 2.5, Width = 1.5, Height = 1 };
            painter.DrawRectangle(brownBrush, blackPen, head);

            // Ноги
            for (int i = 0; i < 4; i++)
            {
                var leg = new RectangleModel
                {
                    X = i * 1.5,
                    Y = 0,
                    Width = 0.5,
                    Height = 2
                };
                painter.DrawRectangle(darkBrush, blackPen, leg);
            }

            // Грива
            var manePoints = new[]
            {
                new PointModel { X = -1.5, Y = 3.5 },
                new PointModel { X = -1.2, Y = 4.5 },
                new PointModel { X = -1.8, Y = 4.5 }
            };
            painter.DrawPolygon(Brushes.Black, null, manePoints);

            // Хвост
            var tail = new[]
            {
                new PointModel { X = 6, Y = 2 },
                new PointModel { X = 6.5, Y = 2 },
                new PointModel { X = 6.3, Y = 1 },
            };
            painter.DrawPolygon(Brushes.Black, null, tail);


            // === САМСОН ===
            // Туловище
            var torso = new RectangleModel { X = 10, Y = 2, Width = 2.5, Height = 4 };
            painter.DrawRectangle(skinBrush, redPen, torso);

            // Голова
            var headSamson = new RectangleModel { X = 10.75, Y = 6, Width = 1, Height = 1 };
            painter.DrawEllipse(skinBrush, blackPen, headSamson);

            // Руки (мускулы)
            var bicepL = new RectangleModel { X = 9, Y = 4, Width = 1, Height = 1 };
            var bicepR = new RectangleModel { X = 12, Y = 4, Width = 1, Height = 1 };
            painter.DrawRectangle(skinBrush, blackPen, bicepL);
            painter.DrawRectangle(skinBrush, blackPen, bicepR);

            // Ноги
            var legLeft = new RectangleModel { X = 10.2, Y = 0, Width = 0.5, Height = 2 };
            var legRight = new RectangleModel { X = 11.3, Y = 0, Width = 0.5, Height = 2 };
            painter.DrawRectangle(darkBrush, customPen, legLeft);
            painter.DrawRectangle(darkBrush, customPen, legRight);

            // Волосы
            var hairPoints = new[]
            {
                new PointModel { X = 10.7, Y = 7 },
                new PointModel { X = 11.3, Y = 7 },
                new PointModel { X = 11.5, Y = 7.3 },
                new PointModel { X = 10.5, Y = 7.3 }
            };
            painter.DrawPolygon(Brushes.Black, null, hairPoints);

            // Меч в руке (линия)
            painter.DrawLine(customPen,
                new PointModel { X = 12, Y = 4.5 },
                new PointModel { X = 13.5, Y = 6 });
        }
    }
}
