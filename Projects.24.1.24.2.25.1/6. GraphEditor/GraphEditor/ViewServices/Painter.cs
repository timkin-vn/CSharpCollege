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
            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
            {
                return;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var rectangle in viewModel.Rectangles)
            {
                using (var pen = new Pen(rectangle.BorderColor, 3))
                using (var brush = new SolidBrush(rectangle.FillColor))
                {
                    DrawFigure(g, rectangle, pen, brush);
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

        private void DrawFigure(Graphics g, RectangleViewModel viewModel, Pen pen, Brush brush)
        {
            var rect = viewModel.Rectangle;

            switch (viewModel.ShapeType)
            {
                case ShapeType.Square:
                    var square = GetRegularBounds(rect);
                    g.FillRectangle(brush, square);
                    g.DrawRectangle(pen, square);
                    break;

                case ShapeType.Circle:
                    var circle = GetRegularBounds(rect);
                    g.FillEllipse(brush, circle);
                    g.DrawEllipse(pen, circle);
                    break;

                case ShapeType.Hexagon:
                    var hexagon = GetHexagonPoints(GetRegularBounds(rect));
                    g.FillPolygon(brush, hexagon);
                    g.DrawPolygon(pen, hexagon);
                    break;

                default:
                    g.FillRectangle(brush, rect);
                    g.DrawRectangle(pen, rect);
                    break;
            }
        }

        private Rectangle GetRegularBounds(Rectangle rectangle)
        {
            var size = rectangle.Width < rectangle.Height ? rectangle.Width : rectangle.Height;

            return new Rectangle
            {
                X = rectangle.X + (rectangle.Width - size) / 2,
                Y = rectangle.Y + (rectangle.Height - size) / 2,
                Width = size,
                Height = size,
            };
        }

        private Point[] GetHexagonPoints(Rectangle rectangle)
        {
            var centerY = rectangle.Top + rectangle.Height / 2;
            var quarterWidth = rectangle.Width / 4;

            return new[]
            {
                new Point(rectangle.Left + quarterWidth, rectangle.Top),
                new Point(rectangle.Right - quarterWidth, rectangle.Top),
                new Point(rectangle.Right, centerY),
                new Point(rectangle.Right - quarterWidth, rectangle.Bottom),
                new Point(rectangle.Left + quarterWidth, rectangle.Bottom),
                new Point(rectangle.Left, centerY),
            };
        }
    }
}
