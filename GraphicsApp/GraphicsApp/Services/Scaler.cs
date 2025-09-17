using GraphicsApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsApp.Services
{
    internal class Scaler
    {
        private Rectangle _screenRectangle;

        public MathRectangle MathRectangle { get; set; }

        public Rectangle ScreenRectangle 
        {
            get => _screenRectangle;
            set
            {
                _screenRectangle = value;
                Rescale();
            }
        }

        public int ScaleX(double x)
        {
            return (int)((x - MathRectangle.Left) * (ScreenRectangle.Right - ScreenRectangle.Left) / (MathRectangle.Right - MathRectangle.Left));
        }

        public int ScaleWidth(double x)
        {
            return (int)(x * (ScreenRectangle.Right - ScreenRectangle.Left) / (MathRectangle.Right - MathRectangle.Left));
        }

        public int ScaleY(double y)
        {
            return (int)((MathRectangle.Top - y) * (ScreenRectangle.Bottom - ScreenRectangle.Top) / (MathRectangle.Top - MathRectangle.Bottom));
        }

        public int ScaleHeight(double y)
        {
            return (int)(y * (ScreenRectangle.Bottom - ScreenRectangle.Top) / (MathRectangle.Top - MathRectangle.Bottom));
        }

        public Point Scale(MathPoint pt)
        {
            return new Point { X = ScaleX(pt.X), Y = ScaleY(pt.Y), };
        }

        public Rectangle Scale(MathRectangle rect)
        {
            return new Rectangle { 
                X = ScaleX(rect.Left), 
                Y = ScaleY(rect.Top), 
                Width = ScaleWidth(rect.Width), 
                Height = ScaleHeight(rect.Height), 
            };
        }

        private void Rescale()
        {
            if (MathRectangle.Width * ScreenRectangle.Height > ScreenRectangle.Width * MathRectangle.Height)
            {
                var mathHeight = MathRectangle.Width * ScreenRectangle.Height / ScreenRectangle.Width;
                var dy = (mathHeight - MathRectangle.Height) / 2;
                MathRectangle.Bottom -= dy;
                MathRectangle.Height += dy * 2;
            }
            else
            {
                var mathWidth = MathRectangle.Height * ScreenRectangle.Width / ScreenRectangle.Height;
                var dx = (mathWidth - MathRectangle.Width) / 2;
                MathRectangle.Left -= dx;
                MathRectangle.Width += dx * 2;
            }
        }
    }
}
