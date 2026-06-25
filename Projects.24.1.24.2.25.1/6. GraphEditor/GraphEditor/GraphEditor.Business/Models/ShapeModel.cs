using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public abstract class ShapeModel
    {
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.FromArgb(40, 40, 44);
        public Color BorderColor { get; set; } = Color.FromArgb(150, 150, 150);

        public abstract bool IsInside(PointModel loc);
        public abstract Rectangle GetBoundingBox();
        public abstract void Move(int dx, int dy);
        public abstract object Clone();
    }
}