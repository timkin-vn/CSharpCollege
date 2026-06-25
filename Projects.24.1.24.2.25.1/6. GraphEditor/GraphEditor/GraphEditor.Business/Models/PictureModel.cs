using System.Collections.Generic;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public List<RectangleModel> Rectangles { get; set; } = new List<RectangleModel>();
        public List<TriangleModel> Triangles { get; set; } = new List<TriangleModel>();
        public List<CircleModel> Circles { get; set; } = new List<CircleModel>();
        public List<HilbertCurveModel> HilbertCurves { get; set; } = new List<HilbertCurveModel>();

        // Общий порядок отрисовки (храним ссылки на фигуры)
        public List<object> DrawOrder { get; set; } = new List<object>();

        public RectangleModel SelectedRectangle { get; set; }
        public TriangleModel SelectedTriangle { get; set; }
        public CircleModel SelectedCircle { get; set; }
        public HilbertCurveModel SelectedHilbertCurve { get; set; }

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

        public void ClearSelection()
        {
            SelectedRectangle = null;
            SelectedTriangle = null;
            SelectedCircle = null;
            SelectedHilbertCurve = null;
        }
    }
}