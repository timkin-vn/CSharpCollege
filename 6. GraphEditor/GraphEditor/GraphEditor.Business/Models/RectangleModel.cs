using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class RectangleModel : FigureModel
    {
        public override bool IsInside(PointModel loc) =>
            loc.X >= Left && loc.X <= Right &&
            loc.Y >= Top && loc.Y <= Bottom;

        public override void Normalize()
        {
            if (Width < 0)
            {
                Left += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }

        public override Rectangle GetBoundingRectangle() => new Rectangle(Left, Top, Width, Height);
    }
}