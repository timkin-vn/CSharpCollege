using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public IList<RectangleModel> Rectangles { get; set; } = new List<RectangleModel>();

        public RectangleModel SelectedRectangle { get; set; }

        public IList<RectangleModel> SelectedRectangles { get; set; } = new List<RectangleModel>();
    }
}
