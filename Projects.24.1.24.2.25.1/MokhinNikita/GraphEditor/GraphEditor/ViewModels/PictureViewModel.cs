using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.Business.Services;

namespace GraphEditor.ViewModels
{
    public class PictureViewModel
    {
        public IList<ReactangleViewModel> Rectangles { get; set; }

        public ReactangleViewModel Selected { get; set; }

        public IEnumerable<MarkerViewModel> Markers => Selected?.Markers ?? Enumerable.Empty<MarkerViewModel>();
    }
}
