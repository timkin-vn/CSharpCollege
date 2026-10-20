using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public bool ShowGrid { get; set; }

        private static readonly Color AccentColor = Color.FromArgb(0, 150, 255);

        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive, Rectangle canvas = default(Rectangle))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (isInteractive && ShowGrid && canvas.Width > 0)
            {
                DrawGrid(g, canvas);
            }

            if (viewModel == null) return;

            // Рисуем в порядке DrawOrder
            foreach (var shape in viewModel.DrawOrder)
            {
                if (shape is RectangleModel rect)
                {
                    var r = new Rectangle(Math.Min(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom), Math.Abs(rect.Width), Math.Abs(rect.Height));
                    if (r.Width < 1 || r.Height < 1) continue;
                    DrawShadow(g, r, false);
                    using (var brush = CreateGradient(r, rect.FillColor))
                    using (var pen = new Pen(rect.BorderColor, 2))
                    {
                        g.FillRectangle(brush, r);
                        g.DrawRectangle(pen, r);
                    }
                }
                else if (shape is TriangleModel tri)
                {
                    var pts = new[] { new Point(tri.X1, tri.Y1), new Point(tri.X2, tri.Y2), new Point(tri.X3, tri.Y3) };
                    var bb = tri.GetBoundingBox();
                    if (bb.Width < 1 || bb.Height < 1) continue;
                    using (var shadow = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
                    {
                        g.FillPolygon(shadow, pts.Select(p => new Point(p.X + 4, p.Y + 4)).ToArray());
                    }
                    using (var brush = CreateGradient(bb, tri.FillColor))
                    using (var pen = new Pen(tri.BorderColor, 2) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round })
                    {
                        g.FillPolygon(brush, pts);
                        g.DrawPolygon(pen, pts);
                    }
                }
                else if (shape is CircleModel circ)
                {
                    var r = new Rectangle(circ.CenterX - circ.Width / 2, circ.CenterY - circ.Height / 2, circ.Width, circ.Height);
                    if (r.Width < 1 || r.Height < 1) continue;
                    DrawShadow(g, r, true);
                    using (var brush = CreateGradient(r, circ.FillColor))
                    using (var pen = new Pen(circ.BorderColor, 2))
                    {
                        g.FillEllipse(brush, r);
                        g.DrawEllipse(pen, r);
                    }
                }
                else if (shape is HilbertCurveModel hilb)
                {
                    using (var pen = new Pen(hilb.BorderColor, 2) { LineJoin = System.Drawing.Drawing2D.LineJoin.Round })
                    {
                        DrawHilbert(g, pen, hilb.X, hilb.Y, hilb.Size, hilb.Order);
                    }
                }
            }

            if (!isInteractive) return;

            // Пунктирная рамка выделения и маркеры
            var markers = viewModel.Markers.ToList();
            if (markers.Count > 0)
            {
                var bounds = markers[0].Rectangle;
                foreach (var m in markers) bounds = Rectangle.Union(bounds, m.Rectangle);
                bounds.Inflate(-3, -3);
                using (var dash = new Pen(AccentColor, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    g.DrawRectangle(dash, bounds);
                }
            }

            foreach (var marker in markers)
            {
                var fill = marker.IsActive ? Color.White : Color.FromArgb(120, 120, 130);
                using (var brush = new SolidBrush(fill))
                using (var pen = new Pen(AccentColor, 1.5f))
                {
                    g.FillEllipse(brush, marker.Rectangle);
                    g.DrawEllipse(pen, marker.Rectangle);
                }
            }
        }

        private static System.Drawing.Drawing2D.LinearGradientBrush CreateGradient(Rectangle r, Color baseColor)
        {
            var rect = new Rectangle(r.X, r.Y, Math.Max(1, r.Width), Math.Max(1, r.Height));
            var light = ControlPaint.Light(baseColor, 0.35f);
            return new System.Drawing.Drawing2D.LinearGradientBrush(rect, light, baseColor, 90f);
        }

        private static void DrawShadow(Graphics g, Rectangle r, bool ellipse)
        {
            var s = new Rectangle(r.X + 4, r.Y + 4, r.Width, r.Height);
            using (var brush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
            {
                if (ellipse) g.FillEllipse(brush, s); else g.FillRectangle(brush, s);
            }
        }

        private static void DrawGrid(Graphics g, Rectangle canvas)
        {
            const int step = 20;
            using (var pen = new Pen(Color.FromArgb(45, 255, 255, 255), 1))
            {
                for (int x = canvas.Left - canvas.Left % step; x < canvas.Right; x += step)
                    g.DrawLine(pen, x, canvas.Top, x, canvas.Bottom);
                for (int y = canvas.Top - canvas.Top % step; y < canvas.Bottom; y += step)
                    g.DrawLine(pen, canvas.Left, y, canvas.Right, y);
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