using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null) return;

            // Рисуем в порядке DrawOrder
            foreach (var shape in viewModel.DrawOrder)
            {
                if (shape is RectangleModel rect)
                {
                    using (var pen = new Pen(rect.BorderColor, 3))
                    using (var brush = new SolidBrush(rect.FillColor))
                    {
                        var r = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
                        g.FillRectangle(brush, r);
                        g.DrawRectangle(pen, r);
                    }
                }
                else if (shape is TriangleModel tri)
                {
                    using (var pen = new Pen(tri.BorderColor, 3))
                    using (var brush = new SolidBrush(tri.FillColor))
                    {
                        var pts = new[] { new Point(tri.X1, tri.Y1), new Point(tri.X2, tri.Y2), new Point(tri.X3, tri.Y3) };
                        g.FillPolygon(brush, pts);
                        g.DrawPolygon(pen, pts);
                    }
                }
                else if (shape is CircleModel circ)
                {
                    using (var pen = new Pen(circ.BorderColor, 3))
                    using (var brush = new SolidBrush(circ.FillColor))
                    {
                        var r = new Rectangle(circ.CenterX - circ.Width / 2, circ.CenterY - circ.Height / 2, circ.Width, circ.Height);
                        g.FillEllipse(brush, r);
                        g.DrawEllipse(pen, r);
                    }
                }
                else if (shape is HilbertCurveModel hilb)
                {
                    using (var pen = new Pen(hilb.BorderColor, 3))
                    {
                        DrawHilbert(g, pen, hilb.X, hilb.Y, hilb.Size, hilb.Order);
                    }
                }
            }

            // Маркеры (используем отдельные списки для поиска выбранной фигуры)
            if (isInteractive)
            {
                foreach (var marker in viewModel.Markers)
                {
                    var brush = marker.IsActive ? Brushes.White : Brushes.Gray;
                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(Pens.White, marker.Rectangle);
                }
            }
        }

        private void DrawHilbert(Graphics g, Pen pen, int x, int y, int size, int order)
        {
            if (order <= 0 || size <= 0) return;
            int n = 1 << order;
            int step = size / n;
            if (step < 1) step = 1;
            var points = new List<Point>();
            for (int i = 0; i < n * n; i++)
            {
                int row, col;
                HilbertIndexToXY(i, n, out row, out col);
                points.Add(new Point(x + col * step + step / 2, y + row * step + step / 2));
            }
            if (points.Count > 1)
                g.DrawLines(pen, points.ToArray());
        }

        private void HilbertIndexToXY(int index, int n, out int row, out int col)
        {
            int rx, ry, s, t = index;
            row = 0; col = 0;
            for (s = 1; s < n; s <<= 1)
            {
                rx = 1 & (t >> 1);
                ry = 1 & (t ^ rx);
                if (ry == 0)
                {
                    if (rx == 1)
                    {
                        row = s - 1 - row;
                        col = s - 1 - col;
                    }
                    int tmp = row;
                    row = col;
                    col = tmp;
                }
                row += ry * s;
                col += rx * s;
                t >>= 2;
            }
        }
    }
}