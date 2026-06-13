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
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Bottom => Top + Height;
        public int Right => Left + Width;
        public EditMode EditMode { get; set; }
        public int BorderWidth { get; set; } = 3;

        public FigureType Figure { get; set; } = FigureType.Rectangle;

        public bool IsInside(PointModel loc) => loc.X >= Left && loc.X <= Right && loc.Y >= Top && loc.Y <= Bottom;

        public Color FillColor { get; set; } = Color.Red;

        public Color BorderColor { get; set; } = Color.Yellow;

        public void Normalize()
        {
            if(Width < 0)
            {
                Left += Width;
                Width = -Width;
            }
            if(Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }

        public byte Opacity { get; set; } = 255;
    }
}
