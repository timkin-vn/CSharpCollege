using GraphEditor.Business.Models;
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
            if (viewModel?.Figures == null || !viewModel.Figures.Any())
                return;

            foreach (var figure in viewModel.Figures)
            {
                using (var pen = new Pen(figure.BorderColor, 3))
                using (var brush = new SolidBrush(figure.FillColor))
                {
                    var rect = figure.Rectangle;

                    switch (figure.Type)
                    {
                        case FigureType.Rectangle:
                            g.FillRectangle(brush, rect);
                            g.DrawRectangle(pen, rect);
                            break;
                        case FigureType.Ellipse:
                            g.FillEllipse(brush, rect);
                            g.DrawEllipse(pen, rect);
                            break;
                        case FigureType.RoundedRectangle:
                            using (var path = GetRoundedRectanglePath(rect, figure.CornerRadius))
                            {
                                g.FillPath(brush, path);
                                g.DrawPath(pen, path);
                            }
                            break;
                    }
                }
            }

            if (isInteractive)
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

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}