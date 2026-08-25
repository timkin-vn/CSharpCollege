using DrawingApp.Models;
using DrawingApp.Services;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DrawingApp.ViewServices
{
    public class Painter
    {
        private Graphics _graphics;
        public Scaler Scaler { get; private set; }

        public void Paint(Rectangle bounds, Graphics g, RectangleModel sourceBounds, IEnumerable<ShapeModel> shapes)
        {
            Scaler = new Scaler
            {
                TargetBounds = bounds,
                SourceBounds = sourceBounds
            };
            Scaler.Initialize();

            _graphics = g;

            // Отрисовка газона (статический задний фон)
            using (var grassBrush = new SolidBrush(Color.FromArgb(160, 210, 120)))
            {
                DrawRectangle(null, grassBrush, new RectangleModel { X = 0, Y = 170, Width = 900, Height = 170 });
            }

            // Отрисовка всех объектов сцены
            if (shapes != null)
            {
                foreach (var shape in shapes)
                {
                    DrawShape(shape);
                }
            }

            _graphics = null;
        }

        private void DrawShape(ShapeModel shape)
        {
            switch (shape.Type)
            {
                case ShapeType.Rectangle:
                    DrawRectangle(shape.StrokePen, shape.FillBrush, shape.Bounds);
                    break;
                case ShapeType.Ellipse:
                    DrawEllipse(shape.StrokePen, shape.FillBrush, shape.Bounds);
                    break;
                case ShapeType.Polygon:
                    DrawPolygon(shape.StrokePen, shape.FillBrush, shape.PolygonPoints);
                    break;
            }
        }

        public void DrawRectangle(Pen pen, Brush brush, RectangleModel rect)
        {
            Rectangle scaledRect = Scaler.Scale(rect);
            if (brush != null) _graphics.FillRectangle(brush, scaledRect);
            if (pen != null) _graphics.DrawRectangle(pen, scaledRect);
        }

        public void DrawEllipse(Pen pen, Brush brush, RectangleModel rect)
        {
            Rectangle scaledRect = Scaler.Scale(rect);
            if (brush != null) _graphics.FillEllipse(brush, scaledRect);
            if (pen != null) _graphics.DrawEllipse(pen, scaledRect);
        }

        public void DrawPolygon(Pen pen, Brush brush, PointModel[] points)
        {
            Point[] scaledPoints = points.Select(p => Scaler.Scale(p)).ToArray();
            if (brush != null) _graphics.FillPolygon(brush, scaledPoints);
            if (pen != null) _graphics.DrawPolygon(pen, scaledPoints);
        }
    }
}