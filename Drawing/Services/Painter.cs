using Drawing.Models;
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
        private Scaler _scaler = new Scaler();

        private PictureBuilder _builder = new PictureBuilder();

        private Graphics _graphics;

        public void Initialize(Rectangle targetRectangle)
        {
            _scaler.Initialize(targetRectangle, _builder.PictureBounds);
        }

        public void Paint(Graphics g)
        {
            _graphics = g;
            _builder.BuildPicture(this);
            _graphics = null;
        }

        public void DrawEllipse(Brush brush, Pen pen, RectangleModel rectModel)
        {
            var rect = _scaler.Scale(rectModel);

            if (brush != null)
            {
                _graphics.FillEllipse(brush, rect);
            }

            if (pen != null)
            {
                _graphics.DrawEllipse(pen, rect);
            }
        }

        public void DrawRectangle(Brush brush, Pen pen, RectangleModel rectModel)
        {
            var rect = _scaler.Scale(rectModel);

            if (brush != null)
            {
                _graphics.FillRectangle(brush, rect);
            }

            if (pen != null)
            {
                _graphics.DrawRectangle(pen, rect);
            }
        }

        public void DrawLine(Pen pen, PointModel pointModel1, PointModel pointModel2)
        {
            var point1 = _scaler.Scale(pointModel1);
            var point2 = _scaler.Scale(pointModel2);

            if (pen != null)
            {
                _graphics.DrawLine(pen, point1, point2);
            }
        }

        public void DrawPie(Brush brush, Pen pen, RectangleModel rectModel, float startAngle, float sweepAngle)
        {
            var rect = _scaler.Scale(rectModel);

            if (brush != null)
            {
                _graphics.FillPie(brush, rect, startAngle, sweepAngle);
            }

            if (pen != null)
            {
                _graphics.DrawPie(pen, rect, startAngle, sweepAngle);
            }
        }

        public void DrawPolygon(Brush brush, Pen pen, PointModel[] pointModels)
        {
            var points = pointModels.Select(pt => _scaler.Scale(pt)).ToArray();

            if (brush != null)
            {
                _graphics.FillPolygon(brush, points);
            }

            if (pen != null)
            {
                _graphics.DrawPolygon(pen, points);
            }
        }
    }
}
