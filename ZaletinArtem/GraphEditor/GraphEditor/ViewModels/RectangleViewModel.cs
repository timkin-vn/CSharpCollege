using System.Drawing;

namespace GraphEditor.ViewModels
{
    internal class RectangleViewModel
    {
        public Rectangle Rectangle { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;

        public Color DrawColor { get; set; } = Color.Blue;

        public int Layer { get; set; } = 0;
    }
}
