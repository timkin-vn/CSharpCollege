using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewModels
{
    internal class RectangleViewModel
    {
        public Rectangle Rectangle { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;

        public Color DrawColor { get; set; } = Color.Red;
    }
}
