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
        internal int Dx;

        internal int Dy;

        internal List<RectangleModel> RectangleList = new List<RectangleModel>();

        public IEnumerable<RectangleModel> Rectangles => RectangleList;

        public RectangleModel SelectedRectangle { get; internal set; }
        public bool IsNewFillColor { get; set; } = false;
        public bool IsNewDrawColor { get; set; } = false;
        public Color FillColor_New { get; set; } = Color.Yellow;
        public Color DrawColor_New { get; set; } = Color.Blue;

        public PictureMode Mode { get; internal set; }
    }
}
