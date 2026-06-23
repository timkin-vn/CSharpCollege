using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphEditor.ViewModels
{
    internal abstract class FigureViewModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Bottom => Top + Height;
        public int Right => Left + Width;

        public abstract Rectangle Rectangle { get; }
        public abstract IEnumerable<MarkerViewModel> Markers { get; }

        public Color FillColor { get; set; }
        public Brush FillBrush { get; set; }
        public Color BorderColor { get; set; }
        public Pen BorderPen { get; set; }

        public abstract void UpdateFillColor(Color newColor);
        public abstract void UpdateBorderColor(Color newColor);
    }
}