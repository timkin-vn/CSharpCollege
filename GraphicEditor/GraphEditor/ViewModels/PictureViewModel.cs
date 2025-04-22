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
        public bool IsNewFillColor { get; set; } = false;
        public bool IsNewDrawColor { get; set; } = false;
        public Color FillColor_New { get; set; } = Color.Yellow;
        public Color DrawColor_New { get; set; } = Color.Red;
    }
}
