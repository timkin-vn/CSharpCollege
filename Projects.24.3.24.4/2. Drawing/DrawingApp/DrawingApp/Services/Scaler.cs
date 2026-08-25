using DrawingApp.Models;
using System;
using System.Drawing;

namespace DrawingApp.Services
{
    public class Scaler
    {
        public Rectangle TargetBounds { get; set; }     // Физические пиксели формы
        public RectangleModel SourceBounds { get; set; } // Логический размер сцены

        private double _scale;
        private double _offsetX;
        private double _offsetY;

        public void Initialize()
        {
            if (SourceBounds == null || SourceBounds.Width <= 0 || SourceBounds.Height <= 0)
                return;

            double scaleX = TargetBounds.Width / SourceBounds.Width;
            double scaleY = TargetBounds.Height / SourceBounds.Height;

            _scale = Math.Min(scaleX, scaleY);

            _offsetX = (TargetBounds.Width - SourceBounds.Width * _scale) / 2.0;
            _offsetY = (TargetBounds.Height - SourceBounds.Height * _scale) / 2.0;
        }

        /// <summary>
        /// Перевод из виртуальных координат в физические пиксели.
        /// </summary>
        public Point Scale(PointModel point)
        {
            int x = (int)(_offsetX + (point.X - SourceBounds.X) * _scale);
            int y = (int)(_offsetY + (SourceBounds.Y - point.Y) * _scale);
            return new Point(x, y);
        }

        public Rectangle Scale(RectangleModel rect)
        {
            Point topLeft = Scale(new PointModel(rect.X, rect.Y));
            int width = (int)(rect.Width * _scale);
            int height = (int)(rect.Height * _scale);
            return new Rectangle(topLeft.X, topLeft.Y, width, height);
        }

        /// <summary>
        /// Перевод из пикселей экрана в виртуальные координаты.
        /// </summary>
        public PointModel Unscale(Point screenPoint)
        {
            if (_scale <= 0) return new PointModel(0, 0);

            double x = SourceBounds.X + (screenPoint.X - _offsetX) / _scale;
            double y = SourceBounds.Y - (screenPoint.Y - _offsetY) / _scale;

            return new PointModel(x, y);
        }
    }
}