using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class RectangleModel : ShapeModel
    {
        public override bool IsInside(PointModel loc) =>
            loc.X >= Left && loc.X <= Right && loc.Y >= Top && loc.Y <= Bottom;
    }
}
