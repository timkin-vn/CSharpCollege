using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class CircleModel
    {
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }   // ширина эллипса
        public int Height { get; set; }  // высота эллипса

        public int Dx { get; set; }
        public int Dy { get; set; }
        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public PointModel GetCenter() => new PointModel { X = CenterX, Y = CenterY };

        public bool IsInside(PointModel loc)
        {
            // Проверка попадания в эллипс
            if (Width <= 0 || Height <= 0) return false;
            double rx = Width / 2.0;
            double ry = Height / 2.0;
            double dx = (loc.X - CenterX) / rx;
            double dy = (loc.Y - CenterY) / ry;
            return dx * dx + dy * dy <= 1.0;
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(CenterX - Width / 2, CenterY - Height / 2, Width, Height);
        }

        public void SetBoundingBox(Rectangle rect)
        {
            CenterX = rect.X + rect.Width / 2;
            CenterY = rect.Y + rect.Height / 2;
            Width = rect.Width;
            Height = rect.Height;
            if (Width < 1) Width = 1;
            if (Height < 1) Height = 1;
        }
    }
}