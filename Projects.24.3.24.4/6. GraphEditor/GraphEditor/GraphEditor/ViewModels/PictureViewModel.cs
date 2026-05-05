using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        public IList<RectangleViewModel> Rectangles { get; set; }

        public RectangleViewModel SelectedRectangle { get; set; }

        public IList<RectangleViewModel> SelectedRectangles { get; set; } = new List<RectangleViewModel>();

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                if (SelectedRectangle == null)
                {
                    return Enumerable.Empty<MarkerViewModel>();
                }

                return SelectedRectangle.Markers;
            }
        }
    }
}