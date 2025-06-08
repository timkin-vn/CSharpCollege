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
        public Rectangle TargetRectangle { get; set; }

        public RectangleModel SourceRectangle { get; set; }

        public void Initialize(Rectangle targetRectangle, RectangleModel sourceRectangle)
        {
            TargetRectangle = targetRectangle;
            SourceRectangle = sourceRectangle;
            Rescale();
        }

        public int ScaleX(double x)
        {
            return (int)((x - SourceRectangle.Left) * TargetRectangle.Width / SourceRectangle.Width);
        }

        public int ScaleY(double y)
        {
            return (int)((SourceRectangle.Top - y) * TargetRectangle.Height / SourceRectangle.Height);
        }

        public int ScaleWidth(double width)
        {
            return (int)(width * TargetRectangle.Width / SourceRectangle.Width);
        }

        public int ScaleHeigth(double  heigth)
        {
            return (int)(heigth * TargetRectangle.Height / SourceRectangle.Height);
        }

        public Point Scale(PointModel point)
        {
            return new Point
            {
                X = ScaleX(point.X),
                Y = ScaleY(point.Y),
            };
        }

        public Rectangle Scale(RectangleModel rect)
        {
            return new Rectangle
            {
                X = ScaleX(rect.X),
                Y = ScaleY(rect.Top),
                Width = ScaleWidth(rect.Width),
                Height = ScaleHeigth(rect.Height),
            };
        }

        private void Rescale()
        {
            if (TargetRectangle.Width * SourceRectangle.Height > SourceRectangle.Width * TargetRectangle.Height)
            {
                // Добавки слева и справа
                var newWidth = TargetRectangle.Width * SourceRectangle.Height / TargetRectangle.Height;
                SourceRectangle.X -= (newWidth - SourceRectangle.Width) / 2;
                SourceRectangle.Width = newWidth;
            }
            else
            {
                // Добавки сверху и снизу
                var newHeight = TargetRectangle.Height * SourceRectangle.Width / TargetRectangle.Width;
                SourceRectangle.Y -= (newHeight - SourceRectangle.Height) / 2;
                SourceRectangle.Height = newHeight;
            }
        }
    }
}
