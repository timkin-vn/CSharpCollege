using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models.Xml
{
    public class CircleModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }

        public int Diameter { get; set; }

        public int Right
        {
            get => Left + Diameter;
            set => Diameter = value - Left;
        }

        public int Bottom
        {
            get => Top + Diameter;
            set => Diameter = value - Top;
        }

        public int Radius => Diameter / 2;

        public int CenterX => Left + Radius;
        public int CenterY => Top + Radius;

        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.LightGreen;
        public Color BorderColor { get; set; } = Color.DarkGreen;

        public bool IsInside(PointModel loc)
        {
            int dx = loc.X - CenterX;
            int dy = loc.Y - CenterY;

            return dx * dx + dy * dy <= Radius * Radius;
        }

        public void Normalize()
        {
            if (Diameter < 0)
            {
                Left += Diameter;
                Diameter = -Diameter;
            }
        }
    }
}
