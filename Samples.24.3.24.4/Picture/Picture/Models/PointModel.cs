using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture.Models
{
    public class PointModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointModel(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
