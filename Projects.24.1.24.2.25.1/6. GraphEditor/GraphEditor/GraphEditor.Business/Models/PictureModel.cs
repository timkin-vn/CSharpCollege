using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public IList<ShapeModel> Shapes { get; set; } = new List<ShapeModel>();

        public IList<RectangleModel> Rectangles => Shapes.OfType<RectangleModel>().ToList();

        public ShapeModel SelectedShape { get; set; }

        public ShapeModel SelectedRectangle
        {
            get => SelectedShape;
            set => SelectedShape = value;
        }
        public enum ShapeType { Rectangle, Ellipse }
    }
}
