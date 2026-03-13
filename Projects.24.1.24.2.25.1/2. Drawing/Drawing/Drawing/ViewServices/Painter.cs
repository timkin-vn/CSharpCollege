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
    }
}
