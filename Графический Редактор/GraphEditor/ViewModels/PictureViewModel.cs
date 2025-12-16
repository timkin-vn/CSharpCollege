using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        public IList<ShapeViewModel> Shapes { get; set; }
        public ShapeViewModel SelectedShape { get; set; }

        public IEnumerable<MarkerViewModel> Markers => SelectedShape?.Markers ?? Enumerable.Empty<MarkerViewModel>();
    }
}