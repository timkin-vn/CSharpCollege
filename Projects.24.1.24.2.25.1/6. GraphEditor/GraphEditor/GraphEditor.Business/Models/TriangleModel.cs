using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class TriangleModel
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int X3 { get; set; }
        public int Y3 { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }
        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter()
        {
            return new PointModel
            {
                X = (X1 + X2 + X3) / 3,
                Y = (Y1 + Y2 + Y3) / 3
            };
        }

        public bool IsInside(PointModel loc)
        {
            var area = Math.Abs((X2 - X1) * (Y3 - Y1) - (X3 - X1) * (Y2 - Y1));
            if (area == 0) return false;
            var area1 = Math.Abs((X2 - loc.X) * (Y3 - loc.Y) - (X3 - loc.X) * (Y2 - loc.Y));
            var area2 = Math.Abs((X1 - loc.X) * (Y3 - loc.Y) - (X3 - loc.X) * (Y1 - loc.Y));
            var area3 = Math.Abs((X1 - loc.X) * (Y2 - loc.Y) - (X2 - loc.X) * (Y1 - loc.Y));
            return Math.Abs(area - (area1 + area2 + area3)) < 0.5;
        }

        public Rectangle GetBoundingBox()
        {
            int left = Math.Min(Math.Min(X1, X2), X3);
            int top = Math.Min(Math.Min(Y1, Y2), Y3);
            int right = Math.Max(Math.Max(X1, X2), X3);
            int bottom = Math.Max(Math.Max(Y1, Y2), Y3);
            return new Rectangle(left, top, right - left, bottom - top);
        }
    }
}