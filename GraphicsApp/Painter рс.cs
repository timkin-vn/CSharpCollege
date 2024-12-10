using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -10,
            Right = 10,
            Bottom = -2,
            Top = 10,
        };

        public void Paint(PaintManager paintManager)
        {
            // Перо для рисования контуров
            var mainPen = new Pen(Color.Black, 2);
            var detailPen = new Pen(Color.DarkGray, 2);

            // Заливка для корпуса ракеты
            var bodyBrush = new SolidBrush(Color.LightGray);

            // Создаем градиентный фон от синего к фиолетовому
            var backgroundRect = new Rectangle(
                0,
                0,
                (int)paintManager.Graphics.VisibleClipBounds.Width,
                (int)paintManager.Graphics.VisibleClipBounds.Height);
            var backgroundBrush = new LinearGradientBrush(
                backgroundRect,
                Color.Blue,
                Color.Purple,
                LinearGradientMode.Vertical);

            // Заливаем фон
            paintManager.Graphics.FillRectangle(backgroundBrush, paintManager.Graphics.VisibleClipBounds);

            // Рисуем корпус ракеты
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -1, Right = 1, Bottom = -4, Top = 4 });

            // Рисуем нос ракеты
            paintManager.DrawPolygon(mainPen, bodyBrush,
                new[]
                {
                    new MathPoint { X = -1, Y = 4 },
                    new MathPoint { X = 1, Y = 4 },
                    new MathPoint { X = 0, Y = 6 }
                });

            // Рисуем сопла
            paintManager.DrawPolygon(mainPen, new SolidBrush(Color.DarkGray),
                new[]
                {
                    new MathPoint { X = -1, Y = -4 },
                    new MathPoint { X = 1, Y = -4 },
                    new MathPoint { X = -0.5, Y = -6 },
                    new MathPoint { X = 0.5, Y = -6 }
                });

            // Рисуем стабилизаторы
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = -2, Right = -1, Bottom = -2, Top = -1 });
            paintManager.DrawRectangle(mainPen, bodyBrush, new MathRectangle { Left = 1, Right = 2, Bottom = -2, Top = -1 });

            // Рисуем окно с градиентом
            var windowRect = new Rectangle(0, 0, 1, 1); // Используем временный прямоугольник для создания градиента
            var windowBrush = new LinearGradientBrush(
                windowRect,
                Color.LightSkyBlue,
                Color.White,
                LinearGradientMode.Horizontal);
            paintManager.DrawEllipse(mainPen, windowBrush, new MathRectangle { Left = -0.5, Right = 0.5, Bottom = 2, Top = 3 });

            // Добавляем дополнительные детали
            // Горизонтальные линии на корпусе
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = -3.5, Top = -3.4 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = -2.5, Top = -2.4 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = -1.5, Top = -1.4 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = 0.5, Top = 0.6 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = 1.5, Top = 1.6 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -1.1, Right = 1.1, Bottom = 2.5, Top = 2.6 });

            // Вертикальные линии на корпусе
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = -0.9, Right = -0.8, Bottom = -4, Top = 4 });
            paintManager.DrawRectangle(detailPen, null, new MathRectangle { Left = 0.8, Right = 0.9, Bottom = -4, Top = 4 });

            // Дополнительные детали на носу ракеты
            paintManager.DrawPolygon(detailPen, null,
                new[]
                {
                    new MathPoint { X = -0.5, Y = 4 },
                    new MathPoint { X = 0.5, Y = 4 },
                    new MathPoint { X = 0, Y = 5 }
                });

            // Дополнительные детали на соплах
            paintManager.DrawPolygon(detailPen, null,
                new[]
                {
                    new MathPoint { X = -0.8, Y = -4 },
                    new MathPoint { X = 0.8, Y = -4 },
                    new MathPoint { X = -0.4, Y = -5 },
                    new MathPoint { X = 0.4, Y = -5 }
                });
        }
    }
}
