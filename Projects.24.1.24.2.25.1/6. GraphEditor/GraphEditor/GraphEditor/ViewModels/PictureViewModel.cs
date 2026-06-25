using GraphEditor.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.ViewModels
{
    internal class PictureViewModel
    {
        // Ссылки на бизнес-модели (для рисования)
        public List<object> DrawOrder { get; set; } = new List<object>();

        // ViewModel-представления для маркеров
        public IList<RectangleViewModel> Rectangles { get; set; }
        public IList<TriangleViewModel> Triangles { get; set; }
        public IList<CircleViewModel> Circles { get; set; }
        public IList<HilbertCurveViewModel> HilbertCurves { get; set; }

        public RectangleViewModel SelectedRectangle { get; set; }
        public TriangleViewModel SelectedTriangle { get; set; }
        public CircleViewModel SelectedCircle { get; set; }
        public HilbertCurveViewModel SelectedHilbertCurve { get; set; }

        public object SelectedShape
        {
            get
            {
                if (SelectedRectangle != null) return SelectedRectangle;
                if (SelectedTriangle != null) return SelectedTriangle;
                if (SelectedCircle != null) return SelectedCircle;
                if (SelectedHilbertCurve != null) return SelectedHilbertCurve;
                return null;
            }
        }

        public IEnumerable<MarkerViewModel> Markers
        {
            get
            {
                if (SelectedRectangle != null) return SelectedRectangle.Markers;
                if (SelectedTriangle != null) return SelectedTriangle.Markers;
                if (SelectedCircle != null) return SelectedCircle.Markers;
                if (SelectedHilbertCurve != null) return SelectedHilbertCurve.Markers;
                return Enumerable.Empty<MarkerViewModel>();
            }
        }
    }
}