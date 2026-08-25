using System.Drawing;

namespace DrawingApp.Models
{
    public enum ShapeType
    {
        Rectangle,
        Ellipse,
        Polygon
    }

    public class ShapeModel
    {
        public ShapeType Type { get; set; }
        public RectangleModel Bounds { get; set; }
        public PointModel[] PolygonPoints { get; set; }

        public Pen StrokePen { get; set; }
        public Brush FillBrush { get; set; }

        /// <summary>
        /// Проверка попадания клика в область фигуры (в виртуальных координатах).
        /// </summary>
        public bool ContainsPoint(PointModel point)
        {
            if (Type == ShapeType.Rectangle || Type == ShapeType.Ellipse)
            {
                if (Bounds == null) return false;
                return point.X >= Bounds.X && point.X <= Bounds.X + Bounds.Width &&
                       point.Y <= Bounds.Y && point.Y >= Bounds.Y - Bounds.Height;
            }
            else if (Type == ShapeType.Polygon && PolygonPoints != null)
            {
                double minX = double.MaxValue, maxX = double.MinValue;
                double minY = double.MaxValue, maxY = double.MinValue;

                foreach (var p in PolygonPoints)
                {
                    if (p.X < minX) minX = p.X;
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y < minY) minY = p.Y;
                    if (p.Y > maxY) maxY = p.Y;
                }

                return point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY;
            }

            return false;
        }

        /// <summary>
        /// Перемещение фигуры на дельту (dx, dy).
        /// </summary>
        public void Move(double dx, double dy)
        {
            if (Bounds != null)
            {
                Bounds.X += dx;
                Bounds.Y += dy;
            }

            if (PolygonPoints != null)
            {
                foreach (var p in PolygonPoints)
                {
                    p.X += dx;
                    p.Y += dy;
                }
            }
        }
    }
}