using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseEvents.Models
{
    internal class RectangleModel
    {
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

        public Rectangle GetRectangle()
        {
            return new Rectangle
            {
                X = Left < Right ? Left : Right,
                Y = Top < Bottom ? Top : Bottom,
                Width = Width > 0 ? Width : -Width,
                Height = Height > 0 ? Height : -Height,
            };
        }

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
