using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Picture.Models;
using System;
using System.Drawing;

namespace Picture.ViewServices
{
    internal class Scaler
    {
        public RectangleModel SourceBounds { get; set; } = new RectangleModel(0, 0, 500, 500); // Виртуальный холст 500x500
        public Rectangle TargetBounds { get; set; }

        public void Initialize()
        {
            if (TargetBounds.Width * SourceBounds.Height > SourceBounds.Width * TargetBounds.Height)
            {
                // Подгоняем по высоте
                var newWidth = TargetBounds.Width * SourceBounds.Height / (double)TargetBounds.Height;
                SourceBounds.X -= (newWidth - SourceBounds.Width) / 2;
                SourceBounds.Width = newWidth;
            }
            else
            {
                // Подгоняем по ширине
                var newHeight = TargetBounds.Height * SourceBounds.Width / (double)TargetBounds.Width;
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
            return (int)((y - SourceBounds.Top) * TargetBounds.Height / SourceBounds.Height);
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
            return new Point(ScaleX(point.X), ScaleY(point.Y));
        }

        public Rectangle Scale(RectangleModel rect)
        {
            return new Rectangle(
                ScaleX(rect.X),
                ScaleY(rect.Y),
                ScaleWidth(rect.Width),
                ScaleHeight(rect.Height)
            );
        }
    }
}