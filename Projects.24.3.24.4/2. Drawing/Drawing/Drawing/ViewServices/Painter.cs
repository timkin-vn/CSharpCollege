using Drawing.Models;
using Drawing.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.ViewServices
{
    internal class Painter
    {
        private Graphics _graphics;

        private Scaler _scaler;

        private PictureBuilder _builder = new PictureBuilder();

        public void Paint(Rectangle bounds, Graphics g)
        {
            _scaler = new Scaler
            {
                TargetBounds = bounds,
                SourceBounds = _builder.SourceBounds,
            };

            _scaler.Initialize();

            _graphics = g;

            _builder.DrawPicture(this);

            _graphics = null;
        }

        public void DrawEllipse(Pen pen, Brush brush, RectangleModel rectangleModel)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _graphics.FillEllipse(brush, rectangle);
            }

            if (pen != null)
            {
                _graphics.DrawEllipse(pen, rectangle);
            }
        }

        public void DrawRectangle(Pen pen, Brush brush, RectangleModel rectangleModel)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _graphics.FillRectangle(brush, rectangle);
            }

            if (pen != null)
            {
                _graphics.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawPie(Pen pen, Brush brush, RectangleModel rectangleModel, double startAngle, double sweepAngle)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
            {
                _graphics.FillPie(brush, rectangle, (float)startAngle, (float)sweepAngle);
            }

            if (pen != null)
            {
                _graphics.DrawPie(pen, rectangle, (float)startAngle, (float)sweepAngle);
            }
        }

        public void DrawPolygon(Pen pen, Brush brush, PointModel[] pointModels)
        {
            //var points = new Point[pointModels.Length];
            //for (int i = 0; i < pointModels.Length; i++)
            //{
            //    points[i] = _scaler.Scale(pointModels[i]);
            //}
            var points = pointModels.Select(pm => _scaler.Scale(pm)).ToArray();

            if (brush != null)
            {
                _graphics.FillPolygon(brush, points);
            }

            if (pen != null)
            {
                _graphics.DrawPolygon(pen, points);
            }
        }

        public void DrawLine(Pen pen, PointModel start, PointModel end)
        {
            var p1 = _scaler.Scale(start);
            var p2 = _scaler.Scale(end);
            _graphics.DrawLine(pen, p1, p2);
        }

        public void DrawArc(Pen pen, RectangleModel rect, float startAngle, float sweepAngle)
        {
            var r = _scaler.Scale(rect);
            _graphics.DrawArc(pen, r, startAngle, sweepAngle);
        }

        public void DrawString(string text, Font font, Brush brush, PointModel location)
        {
            var p = _scaler.Scale(location);
            _graphics.DrawString(text, font, brush, p.X, p.Y);
        }

        public void DrawBezier(Pen pen, PointModel p1, PointModel p2, PointModel p3, PointModel p4)
        {
            _graphics.DrawBezier(pen, _scaler.Scale(p1), _scaler.Scale(p2), _scaler.Scale(p3), _scaler.Scale(p4));
        }

    }

}
