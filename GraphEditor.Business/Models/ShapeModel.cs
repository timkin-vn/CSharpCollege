using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public abstract class ShapeModel
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

        public abstract bool IsInside(PointModel loc);

        public void Normalize()
        {
            if (Width < 0) { Left += Width; Width = -Width; }
            if (Height < 0) { Top += Height; Height = -Height; }
        }
    }
}
