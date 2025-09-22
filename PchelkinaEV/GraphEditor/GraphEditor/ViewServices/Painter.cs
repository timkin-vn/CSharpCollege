using GraphEditor.ViewModels;
using System;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel?.Rectangles == null || !viewModel.Rectangles.Any())
                return;

            foreach (var rect in viewModel.Rectangles)
            {
                var r = rect.Rectangle;

                if (r.Width <= 0 || r.Height <= 0)
                    continue;

                if (rect.FillBrush != null)
                    g.FillRectangle(rect.FillBrush, r);

                if (rect.BorderPen != null)
                    g.DrawRectangle(rect.BorderPen, r);
            }

            if (isInteractive && viewModel.Markers != null)
            {
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                var pen = Pens.Black;

                foreach (var marker in viewModel.Markers)
                {
                    var r = marker.Rectangle;
                    if (r.Width <= 0 || r.Height <= 0)
                        continue;

                    var brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, r);
                    g.DrawRectangle(pen, r);
                }
            }
        }
    }
}
