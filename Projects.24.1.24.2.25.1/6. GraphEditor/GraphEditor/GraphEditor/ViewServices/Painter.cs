using GraphEditor.ViewModels;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (g == null || viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var rectangle in viewModel.Rectangles)
            {
                using (var brush = new SolidBrush(rectangle.FillColor))
                using (var pen = new Pen(rectangle.BorderColor, rectangle.BorderThickness))
                {
                    pen.Alignment = PenAlignment.Center;
                    g.FillRectangle(brush, rectangle.Rectangle);
                    g.DrawRectangle(pen, rectangle.Rectangle);
                }
            }

            if (isInteractive && viewModel.Markers != null)
            {
                using (var pen = new Pen(Color.Black, 1f))
                using (var activeBrush = new SolidBrush(Color.Black))
                using (var inactiveBrush = new SolidBrush(Color.White))
                {
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
}
