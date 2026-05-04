using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null || viewModel.Rectangles == null)
            {
                return;
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var shapeVm in viewModel.Rectangles)
            {
                using (var pen = new Pen(shapeVm.BorderColor, 3))
                using (var brush = new SolidBrush(shapeVm.FillColor))
                {
                    if (shapeVm.SourceShape is EllipseModel)
                    {
                        g.FillEllipse(brush, shapeVm.Rectangle);
                        g.DrawEllipse(pen, shapeVm.Rectangle);
                    }
                    else
                    {
                        g.FillRectangle(brush, shapeVm.Rectangle);
                        g.DrawRectangle(pen, shapeVm.Rectangle);
                    }
                }
            }

            if (isInteractive && viewModel.Markers != null)
            {
                var pen = Pens.Black;
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;

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
