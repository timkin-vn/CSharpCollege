using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class TriangleModel : FigureModel
    {
        public override bool IsInside(PointModel loc)
        {
            var points = GetPoints();
            return IsPointInTriangle(new Point(loc.X, loc.Y),
                                   points[0], points[1], points[2]);
        }

        private bool IsPointInTriangle(Point p, Point a, Point b, Point c)
        {
            var d = (b.Y - c.Y) * (a.X - c.X) + (c.X - b.X) * (a.Y - c.Y);
            var u = ((b.Y - c.Y) * (p.X - c.X) + (c.X - b.X) * (p.Y - c.Y)) / (double)d;
            var v = ((c.Y - a.Y) * (p.X - c.X) + (a.X - c.X) * (p.Y - c.Y)) / (double)d;
            var w = 1 - u - v;

            return u >= 0 && v >= 0 && w >= 0;
        }

        public Point[] GetPoints()
        {
            return new Point[]
            {
                new Point(Left + Width / 2, Top),                 
                new Point(Left, Top + Height),                    
                new Point(Left + Width, Top + Height)                
            };
        }

        public override void Normalize()
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

        public override Rectangle GetBoundingRectangle() => new Rectangle(Left, Top, Width, Height);
    }
}