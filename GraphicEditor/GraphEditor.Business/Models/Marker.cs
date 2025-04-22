using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class Marker
    {
        public RectangleModel Rectangle { get; set; }

        public bool IsActive { get; set; }

        public PictureMode Mode { get; set; }

        public PictureCursor Cursor { get; set; }
    }
}
