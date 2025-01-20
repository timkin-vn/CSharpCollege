using GraphEditor.ViewModels;
using System.Drawing;
using System.Linq;

namespace GraphEditor.PresenterServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel picture, bool interactiveMode)
        {
            if (picture == null)
            {
                return;
            }

            Pen pen;
            Brush brush;
            foreach (var rect in picture.Rectangles.OrderBy(r => r.Layer))
            {
                pen = new Pen(rect.DrawColor, 3);
                brush = new SolidBrush(rect.FillColor);
                g.FillRectangle(brush, rect.Rectangle);
                g.DrawRectangle(pen, rect.Rectangle);
            }

            if (interactiveMode)
            {
                // Draw markers
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                pen = Pens.Black;

                foreach (var marker in picture.Markers)
                {
                    brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}
