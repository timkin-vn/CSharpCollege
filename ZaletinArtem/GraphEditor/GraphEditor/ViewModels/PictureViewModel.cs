using System.Collections.Generic;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        public IEnumerable<RectangleViewModel> Rectangles { get; set; }

        public IEnumerable<MarkerViewModel> Markers { get; set; }
    }
}
