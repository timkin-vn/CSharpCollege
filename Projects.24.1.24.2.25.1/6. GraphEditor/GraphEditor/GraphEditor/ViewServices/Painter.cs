
using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(
            Graphics g,
            PictureViewModel viewModel,
            bool isInteractive)
        {
            if (viewModel?.Rectangles == null ||
                !viewModel.Rectangles.Any())
            {
                return;
            }

            foreach (var figure in viewModel.Rectangles)
            {
                Rectangle bounds = figure.Rectangle;

                using (var pen = new Pen(figure.BorderColor, 3))
                using (var brush = new SolidBrush(figure.FillColor))
                {
                    switch (figure.ShapeType)
                    {
                        case ShapeType.Ellipse:
                            g.FillEllipse(brush, bounds);
                            g.DrawEllipse(pen, bounds);
                            break;

                        case ShapeType.Triangle:
                            Point[] points =
                            {
                                new Point(
                                    bounds.Left + bounds.Width / 2,
                                    bounds.Top),
                                new Point(bounds.Left, bounds.Bottom),
                                new Point(bounds.Right, bounds.Bottom)
                            };

                            g.FillPolygon(brush, points);
                            g.DrawPolygon(pen, points);
                            break;

                        default:
                            g.FillRectangle(brush, bounds);
                            g.DrawRectangle(pen, bounds);
                            break;
                    }
                }
            }

            if (!isInteractive)
            {
                return;
            }

            using (var pen = new Pen(Color.Black))
            {
                foreach (var marker in viewModel.Markers)
                {
                    Brush brush = marker.IsActive
                        ? Brushes.Black
                        : Brushes.White;

                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}
