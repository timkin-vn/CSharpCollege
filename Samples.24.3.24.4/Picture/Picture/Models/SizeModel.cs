using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture.Models
{
    public class SizeModel
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public SizeModel(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
