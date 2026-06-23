using System;

namespace GraphEditor.Business.Models
{
    [Serializable]
    public class TriangleModel : ShapeModel
    {
        public void GetVertices(out double ax, out double ay,
                                out double blx, out double bly,
                                out double brx, out double bry)
        {
            ax = X + Width / 2.0; ay = Y;
            blx = X;              bly = Y + Height;
            brx = X + Width;      bry = Y + Height;
        }

        public override bool Contains(int px, int py)
        {
            if (Width <= 0 || Height <= 0)
                return false;

            double ax, ay, blx, bly, brx, bry;
            GetVertices(out ax, out ay, out blx, out bly, out brx, out bry);

            double d1 = Cross(px, py, ax, ay, brx, bry);
            double d2 = Cross(px, py, brx, bry, blx, bly);
            double d3 = Cross(px, py, blx, bly, ax, ay);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        private static double Cross(double px, double py,
                                    double ax, double ay,
                                    double bx, double by)
        {
            return (px - bx) * (ay - by) - (ax - bx) * (py - by);
        }

        public override ShapeModel Clone()
        {
            return new TriangleModel
            {
                Id = Id, X = X, Y = Y, Width = Width, Height = Height, FillArgb = FillArgb
            };
        }
    }
}
