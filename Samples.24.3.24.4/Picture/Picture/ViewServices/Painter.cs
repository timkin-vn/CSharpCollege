using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Picture.Models;
using Picture.Services;
using System.Drawing;
using System.Linq;

namespace Picture.ViewServices
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
                SourceBounds = new RectangleModel(
                    _builder.SourceBounds.X,
                    _builder.SourceBounds.Y,
                    _builder.SourceBounds.Width,
                    _builder.SourceBounds.Height
                )
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
                _graphics.FillEllipse(brush, rectangle);

            if (pen != null)
                _graphics.DrawEllipse(pen, rectangle);
        }

        public void DrawRectangle(Pen pen, Brush brush, RectangleModel rectangleModel)
        {
            var rectangle = _scaler.Scale(rectangleModel);

            if (brush != null)
                _graphics.FillRectangle(brush, rectangle);

            if (pen != null)
                _graphics.DrawRectangle(pen, rectangle);
        }

        public void DrawLine(Pen pen, PointModel point1, PointModel point2)
        {
            var p1 = _scaler.Scale(point1);
            var p2 = _scaler.Scale(point2);
            _graphics.DrawLine(pen, p1, p2);
        }
    }
}