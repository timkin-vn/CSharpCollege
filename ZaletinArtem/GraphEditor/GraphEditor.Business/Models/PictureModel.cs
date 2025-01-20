using System.Collections.Generic;
using System.Linq;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        internal int Dx;

        internal int Dy;

        internal List<RectangleModel> RectangleList = new List<RectangleModel>();

        public IEnumerable<RectangleModel> Rectangles => RectangleList.OrderBy(r => r.Layer);

        public RectangleModel SelectedRectangle { get; internal set; }

        public PictureMode Mode { get; internal set; }
    }

}
