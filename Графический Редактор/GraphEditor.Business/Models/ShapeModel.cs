using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class ShapeModel
    {
        public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;

        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool TrianglePointsUp { get; set; } = true;
        public int Bottom
        {
            get => Top + Height;
            set => Height = value - Top;
        }

        public int Right
        {
            get => Left + Width;
            set => Width = value - Left;
        }

        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;
        public int BorderThickness { get; set; } = 3;

        public bool IsInside(PointModel loc)
        {
            if (ShapeType == ShapeType.Rectangle)
            {
                return loc.X >= Left && loc.X <= Right &&
                       loc.Y >= Top && loc.Y <= Bottom;
            }
            else if (ShapeType == ShapeType.Circle)
            {
                var centerX = Left + Width / 2;
                var centerY = Top + Height / 2;
                var radius = Math.Min(Width, Height) / 2;
                var dx = loc.X - centerX;
                var dy = loc.Y - centerY;
                return dx * dx + dy * dy <= radius * radius;
            }
            else if (ShapeType == ShapeType.Triangle)
            {
                // Более точная проверка для треугольника
                var p = new PointF(loc.X, loc.Y);
                var p1 = new PointF(Left + Width / 2, Top);
                var p2 = new PointF(Left, Bottom);
                var p3 = new PointF(Right, Bottom);

                return IsPointInTriangle(p, p1, p2, p3);
            }

            return false;
        }

        private bool IsPointInTriangle(PointF p, PointF p1, PointF p2, PointF p3)
        {
            float d1 = Sign(p, p1, p2);
            float d2 = Sign(p, p2, p3);
            float d3 = Sign(p, p3, p1);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        private float Sign(PointF p1, PointF p2, PointF p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        public void Normalize()
        {
            if (Width < 0)
            {
                Left += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }
    }
}