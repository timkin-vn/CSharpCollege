using GraphicsApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Services
{
    internal class PaintManager
    {
        public Scaler Scaler { get; set; }

        public Graphics Graphics { get; set; }

        public void DrawRectangle(Pen pen, Brush brush, MathRectangle rectangle)
        {
            var screenRectangle = Scaler.Scale(rectangle);

            if (brush != null)
            {
                Graphics.FillRectangle(brush, screenRectangle);
            }

            if (pen != null)
            {
                Graphics.DrawRectangle(pen, screenRectangle);
            }
        }
        public void DrawRectangleGrad(Pen pen, Color One_color, Color Second_color, MathRectangle rectangle)
        {
            var screenRectangle = Scaler.Scale(rectangle);
            Brush gradientBrush = new LinearGradientBrush(
                new Rectangle((int)(rectangle.Left), (int)(rectangle.Top), (int)(rectangle.Width), (int)(rectangle.Height)), One_color, Second_color, 56f);
            if (gradientBrush != null)
            {

                Graphics.FillRectangle(gradientBrush, screenRectangle);

            };


            if (pen != null)
            {
                Graphics.DrawRectangle(pen, screenRectangle);
            }
        }

        public void DrawEllipse(Pen pen, Brush brush, MathRectangle rectangle)
        {
            var screenRectangle = Scaler.Scale(rectangle);

            if (brush != null)
            {
                Graphics.FillEllipse(brush, screenRectangle);
            }

            if (pen != null)
            {
                Graphics.DrawEllipse(pen, screenRectangle);
            }
        }

        public void DrawPie(Pen pen, Brush brush, MathRectangle rectangle, float startAngle, float sweepAngle)
        {
            var screenRectangle = Scaler.Scale(rectangle);

            if (brush != null)
            {
                Graphics.FillPie(brush, screenRectangle, startAngle, sweepAngle);
            }

            if (pen != null)
            {
                Graphics.DrawPie(pen, screenRectangle, startAngle, sweepAngle);
            }
        }

        public void DrawPolygon(Pen pen, Brush brush, MathPoint[] points)
        {
            var sPoints = new List<Point>();
            foreach (var pt in points)
            {
                sPoints.Add(Scaler.Scale(pt));
            }

            var screenPoints = sPoints.ToArray();

            if (brush != null)
            {
                Graphics.FillPolygon(brush, screenPoints);
            }

            if (pen != null)
            {
                Graphics.DrawPolygon(pen, screenPoints);
            }
        }
    }
}
