using System;

namespace GraphEditor.Business.Models
{
    [Serializable]
    public class DiamondModel : ShapeModel
    {
        public override bool Contains(int px, int py)
        {
            if (Width <= 0 || Height <= 0)
                return false;

            double rx = Width / 2.0;
            double ry = Height / 2.0;
            double cx = X + rx;
            double cy = Y + ry;

            double nx = System.Math.Abs(px - cx) / rx;
            double ny = System.Math.Abs(py - cy) / ry;

            return nx + ny <= 1.0;
        }

        public override ShapeModel Clone()
        {
            return new DiamondModel
            {
                Id = Id, X = X, Y = Y, Width = Width, Height = Height, FillArgb = FillArgb
            };
        }
    }
}
