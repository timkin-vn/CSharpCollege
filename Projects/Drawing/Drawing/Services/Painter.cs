using Drawing.Models;
using Drawing.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class Painter
    {
        public Scaler Scaler { get; set; }

        public PictureBuilder PictureBuilder { get; set; } = new PictureBuilder();

        private Graphics _g;

        public void Initialize(Rectangle bounds)
        {
            Scaler = new Scaler();
            Scaler.TargetBounds = bounds;
            Scaler.SourceBounds = PictureBuilder.SourceBounds;
            Scaler.Initialize();
        }

        public void Paint(Graphics g)
        {
            _g = g;

            PictureBuilder.DrawPicture(this);

            _g = null;
        }

        public void DrawEllipse(Brush brush, Pen pen, RectangleModel rectangleModel)
        {
            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _g.FillEllipse(brush, rectangle);
            }

            if (pen != null)
            {
                _g.DrawEllipse(pen, rectangle);
            }
        }

        public void DrawRectangle(Brush brush, Pen pen, RectangleModel rectangleModel)
        {
            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _g.FillRectangle(brush, rectangle);
            }

            if (pen != null)
            {
                _g.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawLine(Pen pen, PointModel point1, PointModel point2)
        {
            var pt1 = Scaler.Scale(point1);
            var pt2 = Scaler.Scale(point2);

            if (pen != null)
            {
                _g.DrawLine(pen, pt1, pt2);
            }
        }

        public void DrawPie(Brush brush, Pen pen, RectangleModel rectangleModel, int startAngle, int sweepAngle)
        {
            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _g.FillPie(brush, rectangle, startAngle, sweepAngle);
            }

            if (pen != null)
            {
                _g.DrawPie(pen, rectangle, startAngle, sweepAngle);
            }
        }

        public void DrawPolygon(Brush brush, Pen pen, PointModel[] points)
        {
            var pts = points.Select(p => Scaler.Scale(p)).ToArray();

            if (brush != null)
            {
                _g.FillPolygon(brush, pts);
            }

            if (pen != null)
            {
                _g.DrawPolygon(pen, pts);
            }
        }
    }
}
