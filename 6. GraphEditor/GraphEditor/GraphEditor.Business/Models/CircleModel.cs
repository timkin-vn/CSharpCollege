using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class CircleModel : FigureModel
    {
        public int CenterX => Left + Width / 2;
        public int CenterY => Top + Height / 2;
        public int Radius => Math.Min(Width, Height) / 2;

        public override bool IsInside(PointModel loc)
        {
            var dx = loc.X - CenterX;
            var dy = loc.Y - CenterY;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        public override void Normalize()
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

            var size = Math.Max(Width, Height);
            Width = size;
            Height = size;
        }

        public override Rectangle GetBoundingRectangle() => new Rectangle(Left, Top, Width, Height);
    }
}