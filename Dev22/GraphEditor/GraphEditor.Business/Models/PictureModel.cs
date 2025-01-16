using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        internal Point Diff { get; set; }

        private List<RectangleModel> _rectangles = new List<RectangleModel>();

        public IEnumerable<RectangleModel> Rectangles => _rectangles;

        public RectangleModel SelectedRectangle { get; private set; }

        public EditMode EditMode { get; set; }

        public void AddNewRectangle(RectangleModel rectangle)
        {
            _rectangles.Add(rectangle);
            SelectedRectangle = rectangle;
        }
    }
}
