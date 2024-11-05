using GraphicsApp.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class Painter
    {
        public MathRectangle LimitRectangle = new MathRectangle
        {
            Left = -8,
            Right = 9,
            Bottom = -1,
            Top = 9,
        };

        public void Paint(PaintManager paintManager)
        {
            var treePen = new Pen(Color.Black, 3);
            var treeBrush = new SolidBrush(Color.Green);

            // низ
            paintManager.DrawPolygon(treePen, treeBrush, new[]
            {
                new MathPoint { X = 0, Y = -1 },
                new MathPoint { X = -1, Y = 2 },
                new MathPoint { X = 1, Y = 2 },
            });

            // сеередин
            paintManager.DrawPolygon(treePen, treeBrush, new[]
            {
                new MathPoint { X = 0, Y = 1 },
                new MathPoint { X = -2, Y = 4 },
                new MathPoint { X = 2, Y = 4 },
            });

            // вверх
            paintManager.DrawPolygon(treePen, treeBrush, new[]
            {
                new MathPoint { X = 0, Y = 3 },
                new MathPoint { X = -3, Y = 6 },
                new MathPoint { X = 3, Y = 6 },
            });

            // Ствол
            var trunkPen = new Pen(Color.Brown, 2);
            var trunkBrush = new SolidBrush(Color.Brown);
            paintManager.DrawRectangle(trunkPen, trunkBrush, new MathRectangle { Left = -0.5, Right = 0.5, Bottom = -1, Top = 0 });
        }
    }
}