using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null)
                return;

            bool hasRects = viewModel.Rectangles != null && viewModel.Rectangles.Any();
            bool hasCircles = viewModel.Circles != null && viewModel.Circles.Any();

            if (!hasRects && !hasCircles)
                return;

            foreach (var rect in viewModel.Rectangles)
            {
                var pen = rect.BorderPen;
                var brush = rect.FillBrush;

                g.FillRectangle(brush, rect.Rectangle);
                g.DrawRectangle(pen, rect.Rectangle);
            }

            if (viewModel.Circles != null && viewModel.Circles.Any())
            {
                foreach (var circle in viewModel.Circles)
                {
                    var pen = circle.BorderPen;
                    var brush = circle.FillBrush;

                    g.FillEllipse(brush, circle.Rectangle);
                    g.DrawEllipse(pen, circle.Rectangle);
                }
            }

            if (isInteractive)
            {
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                var pen = Pens.Black;

                foreach (var marker in viewModel.Markers)
                {
                    var brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, marker.Rectangle);

                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}
