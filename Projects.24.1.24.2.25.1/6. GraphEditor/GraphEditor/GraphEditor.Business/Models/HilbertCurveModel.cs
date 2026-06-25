using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class HilbertCurveModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Order { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }
        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter() => new PointModel { X = X + Size / 2, Y = Y + Size / 2 };

        public bool IsInside(PointModel loc) =>
            loc.X >= X && loc.X <= X + Size &&
            loc.Y >= Y && loc.Y <= Y + Size;

        public Rectangle GetBoundingBox() => new Rectangle(X, Y, Size, Size);

        public void SetBoundingBox(Rectangle rect)
        {
            X = rect.X;
            Y = rect.Y;
            Size = Math.Min(rect.Width, rect.Height);
            if (Size < 1) Size = 1;
        }
    }
}