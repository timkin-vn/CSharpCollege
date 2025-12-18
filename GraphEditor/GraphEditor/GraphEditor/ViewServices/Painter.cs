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
                {
                    switch (rect.Shape)
                    {
                        case GraphEditor.Business.Models.ShapeType.Circle:
                            g.FillEllipse(rect.FillBrush, r);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Triangle:
                            var t1 = GetTrianglePoints(r);
                            g.FillPolygon(rect.FillBrush, t1);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Hexagon:
                            var hx = GetHexagonPoints(r);
                            g.FillPolygon(rect.FillBrush, hx);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Parallelogram:
                            var pp = GetParallelogramPoints(r);
                            g.FillPolygon(rect.FillBrush, pp);
                            break;
                        default:
                            g.FillRectangle(rect.FillBrush, r);
                            break;
                    }
                }

                if (rect.BorderPen != null)
                {
                    switch (rect.Shape)
                    {
                        case GraphEditor.Business.Models.ShapeType.Circle:
                            g.DrawEllipse(rect.BorderPen, r);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Triangle:
                            var t2 = GetTrianglePoints(r);
                            g.DrawPolygon(rect.BorderPen, t2);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Hexagon:
                            var hx2 = GetHexagonPoints(r);
                            g.DrawPolygon(rect.BorderPen, hx2);
                            break;
                        case GraphEditor.Business.Models.ShapeType.Parallelogram:
                            var pp2 = GetParallelogramPoints(r);
                            g.DrawPolygon(rect.BorderPen, pp2);
                            break;
                        default:
                            g.DrawRectangle(rect.BorderPen, r);
                            break;
                    }
                }
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

        private Point[] GetTrianglePoints(Rectangle r)
        {
            // Use normalized rectangle (r.Top is min Y, r.Bottom is max Y) so triangle always points up
            var pApex = new Point(r.Left + r.Width / 2, r.Top);
            var pLeft = new Point(r.Left, r.Bottom);
            var pRight = new Point(r.Right, r.Bottom);
            return new[] { pApex, pLeft, pRight };
        }

        private Point[] GetHexagonPoints(Rectangle r)
        {
            // regular hexagon inscribed into rectangle horizontally
            int cx = r.Left + r.Width / 2;
            int cy = r.Top + r.Height / 2;
            int rx = r.Width / 2;
            int ry = r.Height / 2;

            var pts = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 6 + i * Math.PI / 3; // start at 30 degrees
                int x = cx + (int)(rx * Math.Cos(angle));
                int y = cy + (int)(ry * Math.Sin(angle));
                pts[i] = new Point(x, y);
            }

            return pts;
        }

        private Point[] GetParallelogramPoints(Rectangle r)
        {
            // skew horizontally by 25% of width
            int skew = (int)(r.Width * 0.25);
            var p1 = new Point(r.Left + skew, r.Top);
            var p2 = new Point(r.Right, r.Top);
            var p3 = new Point(r.Right - skew, r.Bottom);
            var p4 = new Point(r.Left, r.Bottom);
            return new[] { p1, p2, p3, p4 };
        }
    }
}
