using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphEditor.Business.Models
{
    public class EllipseModel : ShapeModel
    {
        public override bool IsInside(PointModel loc)
        {
            
            double rx = Width / 2.0;
            double ry = Height / 2.0;

            if (rx <= 0 || ry <= 0) return false;

            double cx = Left + rx;
            double cy = Top + ry;

            return (Math.Pow(loc.X - cx, 2) / Math.Pow(rx, 2)) +
                   (Math.Pow(loc.Y - cy, 2) / Math.Pow(ry, 2)) <= 1;
        }
    }
}
