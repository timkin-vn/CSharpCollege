using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        internal int Dx;

        internal int Dy;

        internal List<RectangleModel> RectangleList = new List<RectangleModel>();

        public IEnumerable<RectangleModel> Rectangles => RectangleList;

        public RectangleModel SelectedRectangle { get; internal set; }

        public PictureMode Mode { get; internal set; }
    }
}
