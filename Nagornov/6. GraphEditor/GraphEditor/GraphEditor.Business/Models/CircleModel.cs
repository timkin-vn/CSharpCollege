using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class CircleModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Radius { get; set; }
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Pink;
        public Color BorderColor { get; set; } = Color.Red;

        public int Left => CenterX - Radius;
        public int Top => CenterY - Radius;
        public int Width => Radius * 2;
        public int Height => Radius * 2;

        public bool IsInside(PointModel loc)
        {
            int dx = loc.X - CenterX;
            int dy = loc.Y - CenterY;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        public void Normalize()
        {
            if (Radius < 0)
            {
                Radius = -Radius;
            }
        }

        public Rectangle GetBoundingRectangle()
        {
            return new Rectangle(Left, Top, Width, Height);
        }
    }
}