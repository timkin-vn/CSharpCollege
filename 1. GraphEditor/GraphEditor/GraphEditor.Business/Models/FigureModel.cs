using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public abstract class FigureModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
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

        public abstract bool IsInside(PointModel loc);
        public abstract void Normalize();
        public abstract Rectangle GetBoundingRectangle();

        public virtual void ChangeFillColor(Color newColor) => FillColor = newColor;
        public virtual void ChangeBorderColor(Color newColor) => BorderColor = newColor;
    }
}
