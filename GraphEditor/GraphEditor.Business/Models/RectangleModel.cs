using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public enum GradientType
    {
        None,
        Linear,
        Radial
    }

    public class RectangleModel
    {
        public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;

        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Right => Left + Width;
        public int Bottom => Top + Height;
        public int EndX => Right;
        public int EndY => Bottom;
        public EditMode EditMode { get; set; }
        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public byte Opacity { get; set; } = 255;

        public float RotationAngle { get; set; } = 0f;

        public bool ShowShadow { get; set; } = false;
        public int ShadowOffsetX { get; set; } = 5;
        public int ShadowOffsetY { get; set; } = 5;
        public Color ShadowColor { get; set; } = Color.Gray;

        public GradientType GradientType { get; set; } = GradientType.None;
        public Color FillColor2 { get; set; } = Color.White;
        public float GradientAngle { get; set; } = 45f;

        public bool IsInside(PointModel loc)
        {
            switch (ShapeType)
            {
                case ShapeType.Ellipse:
                    return IsInsideEllipse(loc);
                case ShapeType.Line:
                    return DistanceToLine(loc) <= 8;
                default:
                    int left = Width < 0 ? Left + Width : Left;
                    int top = Height < 0 ? Top + Height : Top;
                    int width = Width < 0 ? -Width : Width;
                    int height = Height < 0 ? -Height : Height;
                    return loc.X >= left && loc.X <= left + width &&
                           loc.Y >= top && loc.Y <= top + height;
            }
        }

        public void Normalize()
        {
            if (ShapeType == ShapeType.Line)
                return;

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

        private bool IsInsideEllipse(PointModel loc)
        {
            int left = Width < 0 ? Left + Width : Left;
            int top = Height < 0 ? Top + Height : Top;
            int width = Width < 0 ? -Width : Width;
            int height = Height < 0 ? -Height : Height;

            if (width < 2 || height < 2)
                return false;

            float centerX = left + width / 2f;
            float centerY = top + height / 2f;
            float radiusX = width / 2f;
            float radiusY = height / 2f;
            double nx = (loc.X - centerX) / radiusX;
            double ny = (loc.Y - centerY) / radiusY;
            return nx * nx + ny * ny <= 1;
        }

        private double DistanceToLine(PointModel loc)
        {
            double x1 = Left;
            double y1 = Top;
            double x2 = EndX;
            double y2 = EndY;
            double dx = x2 - x1;
            double dy = y2 - y1;
            double lengthSquared = dx * dx + dy * dy;

            if (lengthSquared < 1)
                return Math.Sqrt((loc.X - x1) * (loc.X - x1) + (loc.Y - y1) * (loc.Y - y1));

            double t = Math.Max(0, Math.Min(1, ((loc.X - x1) * dx + (loc.Y - y1) * dy) / lengthSquared));
            double projX = x1 + t * dx;
            double projY = y1 + t * dy;
            double distX = loc.X - projX;
            double distY = loc.Y - projY;
            return Math.Sqrt(distX * distX + distY * distY);
        }
    }
}