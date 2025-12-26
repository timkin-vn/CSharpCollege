using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        public IEnumerable<RectangleViewModel> Rectangles { get; set; }

        public IEnumerable<MarkerViewModel> Markers { get; set; }
    }
}
