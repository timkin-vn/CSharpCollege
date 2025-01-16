using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseEvents.Model
{
    internal class RectangleModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Point Point1
        {
            get => new Point { X = X, Y = Y };
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Point Point2
        {
            get => new Point { X = X + Width, Y = Y + Height, };
            set
            {
                Width = value.X - X;
                Height = value.Y - Y;
            }
        }

        public Rectangle Rectangle => new Rectangle
        {
            X = Width > 0 ? X : X + Width,
            Y = Height > 0 ? Y : Y + Height,
            Width = Width > 0 ? Width : -Width,
            Height = Height > 0 ? Height : -Height,
        };

        public void Normalize()
        {
            if (Width < 0)
            {
                X += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Y += Height;
                Height = -Height;
            }
        }
    }
}
