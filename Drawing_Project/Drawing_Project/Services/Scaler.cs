using Drawing.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Services
{
    internal class Scaler
    {
        public Rectangle TargetBounds { get; set; }

        public RectangleModel SourceBounds { get; set; }

        public void Initialize()
        {
            if (TargetBounds.Width * SourceBounds.Height > SourceBounds.Width * TargetBounds.Height)
            {
                var newWidth = TargetBounds.Width * SourceBounds.Height / TargetBounds.Height;
                SourceBounds.X -= (newWidth - SourceBounds.Width) / 2;
                SourceBounds.Width = newWidth;
            }
            else
            {
                var newHeight = TargetBounds.Height * SourceBounds.Width / TargetBounds.Width;
                SourceBounds.Y -= (newHeight - SourceBounds.Height) / 2;
                SourceBounds.Height = newHeight;
            }
        }

        public int ScaleX(double x)
        {
            return (int)((x - SourceBounds.Left) * TargetBounds.Width / SourceBounds.Width);
        }

        public int ScaleY(double y)
        {
            return (int)((SourceBounds.Top - y) * TargetBounds.Height / SourceBounds.Height);
        }

        public int ScaleWidth(double width)
        {
            return (int)(width * TargetBounds.Width / SourceBounds.Width);
        }

        public int ScaleHeight(double height)
        {
            return (int)(height * TargetBounds.Height / SourceBounds.Height);
        }

        public Point Scale(PointModel point)
        {
            return new Point
            {
                X = ScaleX(point.X),
                Y = ScaleY(point.Y),
            };
        }

        public Rectangle Scale(RectangleModel point)
        {
            return new Rectangle
            {
                X = ScaleX(point.X),
                Y = ScaleY(point.Top),
                Width = ScaleWidth(point.Width),
                Height = ScaleHeight(point.Height),
            };
        }
    }
}