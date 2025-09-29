using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class RectangleModel
    {
        public int Dx { get; set; }

        public int Dy { get; set; }

        public int Left {  get; set; }
        public int LeftPart
        {
            get => Left;
            set
            {
                Width += Left - value;
                Left = value;
            }
        }
        public int Top { get; set; }
        public int TopPart
        {
            get => Top;
            set
            {
                int bottom = Bottom;
                Top = value;
                Height = bottom - Top;
            }
        }
        public int Width { get; set; }

        public int Height { get; set; }

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
        public Color GradientColor { get; set; } = Color.White;
        public bool UseGradient { get; set; } = false;
    }
}
