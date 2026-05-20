using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        public IList<FigureViewModel> Figures { get; set; } = new List<FigureViewModel>();
        public FigureViewModel SelectedFigure { get; set; }

        public IEnumerable<MarkerViewModel> Markers =>
            SelectedFigure?.Markers ?? Enumerable.Empty<MarkerViewModel>();
    }
}