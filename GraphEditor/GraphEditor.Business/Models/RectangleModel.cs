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
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Right => Left + Width;
        public int Bottom => Top + Height;
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

        public bool IsInside(PointModel loc) =>
            loc.X >= Left && loc.X <= Right &&
            loc.Y >= Top && loc.Y <= Bottom;

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