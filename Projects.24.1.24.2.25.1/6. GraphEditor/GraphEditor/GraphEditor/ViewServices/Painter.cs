using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
                return;

            foreach (var rectangle in viewModel.Rectangles)
            {
                DrawRectangle(g, rectangle);
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

        // этот метод с BorderOpacity
        private void DrawRectangle(Graphics g, RectangleViewModel rect)
        {
            Color fillColor = Color.FromArgb(rect.Opacity, rect.FillColor);
            Color borderColor = Color.FromArgb(rect.BorderOpacity, rect.BorderColor);
            using (var brush = new SolidBrush(fillColor))
            using (var pen = new Pen(borderColor, 3))
            {
                if (rect.CornerRadius > 0)
                {
                    using (var path = GetRoundedRectPath(rect.Rectangle, rect.CornerRadius))
                    {
                        g.FillPath(brush, path);
                        g.DrawPath(pen, path);
                    }
                }
                else
                {
                    g.FillRectangle(brush, rect.Rectangle);
                    g.DrawRectangle(pen, rect.Rectangle);
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);

            path.CloseFigure();
            return path;
        }
    }
}