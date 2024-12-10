using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Models
{
    internal class MathRectangle
    {
        public double Left { get; set; }

        public double Bottom { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Right
        {
            get => Left + Width;
            set
            {
                Width = value - Left;
                Normalize();
            }
        }

        public double Top
        {
            get => Bottom + Height;
            set
            {
                Height = value - Bottom;
                Normalize();
            }
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
                Bottom += Height;
                Height = -Height;
            }
        }
    }
}
