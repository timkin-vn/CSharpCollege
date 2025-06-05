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
            return(int)((SourceRectangle.Top - y) * TargetRectangle.Height / SourceRectangle.Height);
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
            double targetAspect = (double)TargetRectangle.Width / TargetRectangle.Height;
            double sourceAspect = SourceRectangle.Width / SourceRectangle.Height;

            if (targetAspect > sourceAspect)
            {
                // Добавки слева и справа
                SourceRectangle.Width = SourceRectangle.Height * targetAspect;
                SourceRectangle.X = -5; 
            }
            else
            {
                // Добавки сверху и снизу
                SourceRectangle.Height = SourceRectangle.Width / targetAspect;
                SourceRectangle.Y = 0; 
            }
        }
    }
}
