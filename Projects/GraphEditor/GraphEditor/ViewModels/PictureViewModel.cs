using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.ViewModels;

internal class PictureViewModel
{
    public IList<RectangleViewModel> Rectangles { get; set; }

    public RectangleViewModel SelectedRectangle { get; set; }

    public IEnumerable<MarkerViewModel> Markers => SelectedRectangle?.Markers ?? Enumerable.Empty<MarkerViewModel>();
}
