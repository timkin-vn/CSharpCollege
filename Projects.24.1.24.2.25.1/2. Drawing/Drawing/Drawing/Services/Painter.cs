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
        private Graphics _graphics;

        private Scaler _scaler;

        private PictureBuilder _builder = new PictureBuilder();

        public void Initialize(Rectangle bounds)
        {
            _scaler = new Scaler
            {
                TargetBounds = bounds,
                SourceBounds = _builder.SourceBounds,
            };

            _scaler.Initialize();
        }

        public void Paint(Graphics g)
        {
            _graphics = g;

            _builder.DrawPicture(this);

            _graphics = null;
        }

        public void DrawEllipse(Brush brush, Pen pen, RectangleModel rectangleModel)
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

        public void DrawRectangle(Brush brush, Pen pen, RectangleModel rectangleModel)
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

        public void DrawPie(Brush brush, Pen pen, RectangleModel rectangleModel, double startAngle, double sweepAngle)
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

        public void DrawPolygon(Brush brush, Pen pen, PointModel[] pointModels)
        {
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
