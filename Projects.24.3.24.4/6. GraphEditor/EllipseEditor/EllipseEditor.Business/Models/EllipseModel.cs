using System;

namespace EllipseEditor.Business.Models
{

    [Serializable]
    public class EllipseModel : ShapeModel
    {

        public override bool Contains(int px, int py)
        {
            if (Width <= 0 || Height <= 0)
                return false;

            double rx = Width / 2.0;
            double ry = Height / 2.0;
            double cx = X + rx;
            double cy = Y + ry;

            double nx = (px - cx) / rx;
            double ny = (py - cy) / ry;

            return (nx * nx + ny * ny) <= 1.0;
        }

        public override ShapeModel Clone()
        {
            return new EllipseModel
            {
                Id = Id,
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
                FillArgb = FillArgb
            };
        }
    }
}
