using DrawingProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D; // для HatchBrush
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
            // Перья (3 разных)
            var penBlack = new Pen(Color.Black, 2); // обычное перо
            var penWindow = new Pen(Color.FromArgb(0, 120, 180), 1); // перо для окон
            var penStickMan = new Pen(Color.Black, 2); // перо для человека
            var penGrass = new Pen(Color.Green, 1.5f); // новое перо для травы
            var penCloud = new Pen(Color.FromArgb(150, 50, 200), 1); // перо с нестандартным RGB-цветом

            // Кисти (3 разные)
            var houseBrush = new SolidBrush(Color.BurlyWood); // кисть для дома
            var roofBrush = new SolidBrush(Color.Red);  // кисть для крыши
            var doorBrush = new SolidBrush(Color.Goldenrod);    // кисть для двери
            var windowBrush = new SolidBrush(Color.LightBlue);  // кисть для окна
            var chimneyBrush = Brushes.Black;   // кисть для трубы
            var smokeBrush = Brushes.Gray;  // кисть для дыма
            var sunBrush = new SolidBrush(Color.Yellow); // кисть для солнца
            var grassBrush = new HatchBrush(HatchStyle.DashedHorizontal, Color.Green, Color.LightGreen); // новая кисть (штриховка)

            // ==== 1. Дом (прямоугольник) ====
            var house = new RectangleModel { X = 0, Y = 0, Width = 6, Height = 6 };
            painter.DrawRectangle(houseBrush, penBlack, house);

            // ==== 2. Труба (прямоугольник) ====
            var chimney = new RectangleModel { X = 5.2, Y = 6.5, Width = 0.4, Height = 1.2 };
            painter.DrawRectangle(chimneyBrush, penBlack, chimney);

            // ==== 3. Дым (3 эллипса) ====
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.2, Y = 7.8, Width = 0.3, Height = 0.3 });
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.3, Y = 8.2, Width = 0.4, Height = 0.4 });
            painter.DrawEllipse(smokeBrush, null, new RectangleModel { X = 5.4, Y = 8.7, Width = 0.5, Height = 0.5 });

            // ==== 4. Крыша (многоугольник) ====
            var roof = new[]
            {
                new PointModels { X = 0, Y = 6 },
                new PointModels { X = 3, Y = 9 },
                new PointModels { X = 6, Y = 6 },
            };
            painter.DrawPolygon(roofBrush, penBlack, roof);

            // ==== 5. Окна (прямоугольники + линии) ====
            var leftWindow = new RectangleModel { X = 0.6, Y = 3.5, Width = 1.5, Height = 1.5 };
            var rightWindow = new RectangleModel { X = 3.9, Y = 3.5, Width = 1.5, Height = 1.5 };
            painter.DrawRectangle(windowBrush, penWindow, leftWindow);
            painter.DrawRectangle(windowBrush, penWindow, rightWindow);

            // ==== 6. Дверь (прямоугольник + круг-ручка) ====
            var door = new RectangleModel { X = 2.4, Y = 0, Width = 1.2, Height = 2 };
            painter.DrawRectangle(doorBrush, penBlack, door);
            var handle = new RectangleModel { X = 3.4, Y = 1, Width = 0.1, Height = 0.1 };
            painter.DrawEllipse(Brushes.Black, null, handle);

            // ==== 7. Человечек (линии + эллипс) ====
            var head = new RectangleModel { X = -3.2, Y = 3.5, Width = 0.8, Height = 0.8 };
            painter.DrawEllipse(null, penStickMan, head);
            painter.DrawLine(penStickMan, new PointModels { X = -2.8, Y = 3.5 }, new PointModels { X = -2.8, Y = 2.2 });
            painter.DrawLine(penStickMan, new PointModels { X = -2.8, Y = 2.8 }, new PointModels { X = -3.5, Y = 3.1 });
            painter.DrawLine(penStickMan, new PointModels { X = -2.8, Y = 2.8 }, new PointModels { X = -2.1, Y = 3.1 });
            painter.DrawLine(penStickMan, new PointModels { X = -2.8, Y = 2.2 }, new PointModels { X = -3.4, Y = 1.5 });
            painter.DrawLine(penStickMan, new PointModels { X = -2.8, Y = 2.2 }, new PointModels { X = -2.2, Y = 1.5 });

            // ==== 8. Солнце (эллипс + линии) ====
            var sun = new RectangleModel { X = -4, Y = 9, Width = 2, Height = 2 };
            painter.DrawEllipse(sunBrush, null, sun);
            for (int i = 0; i < 12; i++)
            {
                double angle = i * 30 * Math.PI / 180;
                double x1 = -3 + Math.Cos(angle) * 1.2;
                double y1 = 10 + Math.Sin(angle) * 1.2;
                double x2 = -3 + Math.Cos(angle) * 1.8;
                double y2 = 10 + Math.Sin(angle) * 1.8;
                painter.DrawLine(penBlack, new PointModels { X = x1, Y = y1 }, new PointModels { X = x2, Y = y2 });
            }

            // ==== 9. Трава (линии + прямоугольник) ====
            painter.DrawRectangle(grassBrush, null, new RectangleModel { X = -5, Y = -1, Width = 20, Height = 1 });
            for (int i = 0; i < 15; i++)
            {
                double x = -5 + i * 1.3;
                painter.DrawLine(penGrass, new PointModels { X = x, Y = 0 }, new PointModels { X = x + 0.3, Y = -0.5 });
            }

            // ==== 10. Облака (эллипсы + линии) ====
            painter.DrawEllipse(Brushes.White, penCloud, new RectangleModel { X = 7, Y = 8, Width = 2, Height = 1 });
            painter.DrawEllipse(Brushes.White, penCloud, new RectangleModel { X = 8, Y = 7.5, Width = 2, Height = 1.2 });
            painter.DrawEllipse(Brushes.White, penCloud, new RectangleModel { X = 9, Y = 8.2, Width = 1.5, Height = 0.8 });
        }
    }
}