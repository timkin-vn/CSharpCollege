using GraphEditor.ViewModels;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null || viewModel.Figures == null) return;

            g.Clear(Color.White);

            foreach (var figure in viewModel.Figures)
            {
                if (figure is RectangleViewModel rect)
                {
                    using (var fillBrush = new SolidBrush(rect.FillColor))
                    using (var borderPen = new Pen(rect.BorderColor, 2))
                    {
                        g.FillRectangle(fillBrush, rect.Rectangle);
                        g.DrawRectangle(borderPen, rect.Rectangle);
                    }
                }
                else if (figure is CircleViewModel circle)
                {
                    using (var fillBrush = new SolidBrush(circle.FillColor))
                    using (var borderPen = new Pen(circle.BorderColor, 2))
                    {
                        g.FillEllipse(fillBrush, circle.Rectangle);
                        g.DrawEllipse(borderPen, circle.Rectangle);
                    }
                }
                else if (figure is TriangleViewModel triangle)
                {
                    using (var fillBrush = new SolidBrush(triangle.FillColor))
                    using (var borderPen = new Pen(triangle.BorderColor, 2))
                    {
                        var points = triangle.GetPoints();
                        g.FillPolygon(fillBrush, points);
                        g.DrawPolygon(borderPen, points);
                    }
                }
            }

            if (isInteractive)
            {
                foreach (var marker in viewModel.Markers)
                {
                    using (var brush = new SolidBrush(marker.IsActive ? Color.Red : Color.Blue))
                    using (var pen = new Pen(Color.Black))
                    {
                        g.FillRectangle(brush, marker.Rectangle);
                        g.DrawRectangle(pen, marker.Rectangle);
                    }
                }
            }
        }
    }
}