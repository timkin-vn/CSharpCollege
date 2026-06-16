using Drawing.Models;
using DrawingProject.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Drawing.Services {
    internal class Painter : IDisposable {
        public Scaler Scaler { get; set; }
        public PictureBuilder PictureBuilder { get; set; } = new PictureBuilder();
        public Stack<GraphicsState> GraphicsStates { get => _graphicsStates; set => _graphicsStates = value; }
        public Dictionary<string, Brush> BrushCache { get => _brushCache; set => _brushCache = value; }
        public Dictionary<string, Pen> PenCache { get => _penCache; set => _penCache = value; }

        private Graphics _g;
        private Stack<GraphicsState> _graphicsStates = new Stack<GraphicsState>();
        private Dictionary<string, Brush> _brushCache = new Dictionary<string, Brush>();
        private Dictionary<string, Pen> _penCache = new Dictionary<string, Pen>();

        public void Initialize(Rectangle bounds) {
            Scaler = new Scaler {
                TargetBounds = bounds,
                SourceBounds = PictureBuilder.SourceBounds
            };
            Scaler.Initialize();
        }

        public void Paint(Graphics g) {
            _g = g ?? throw new ArgumentNullException(nameof(g));
            _g.SmoothingMode = SmoothingMode.AntiAlias;
            _g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _g.CompositingQuality = CompositingQuality.HighQuality;

            try {
                PictureBuilder.DrawPicture(this);
            } finally {
                _g = null;
                ClearCache();
            }
        }

        public void SaveState() {
            if (_g != null) {
                GraphicsStates.Push(_g.Save());
            }
        }

        public void RestoreState() {
            if (_g != null && GraphicsStates.Count > 0) {
                _g.Restore(GraphicsStates.Pop());
            }
        }

        public void DrawEllipse(Brush brush, Pen pen, RectangleModel rectangleModel) {
            if (_g == null) return;

            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null) {
                _g.FillEllipse(brush, rectangle);
            }

            if (pen != null) {
                _g.DrawEllipse(pen, rectangle);
            }
        }

        public void DrawRectangle(Brush brush, Pen pen, RectangleModel rectangleModel) {
            if (_g == null) return;

            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null) {
                _g.FillRectangle(brush, rectangle);
            }

            if (pen != null) {
                _g.DrawRectangle(pen, rectangle);
            }
        }

        public void DrawRoundedRectangle(Brush brush, Pen pen, RectangleModel rectangleModel, float radius) {
            if (_g == null) return;

            var rect = Scaler.Scale(rectangleModel);
            var path = CreateRoundedRectanglePath(rect, radius);

            if (brush != null) {
                _g.FillPath(brush, path);
            }

            if (pen != null) {
                _g.DrawPath(pen, path);
            }
        }

        public void DrawLine(Pen pen, PointModel point1, PointModel point2) {
            if (_g == null || pen == null) return;

            var pt1 = Scaler.Scale(point1);
            var pt2 = Scaler.Scale(point2);
            _g.DrawLine(pen, pt1, pt2);
        }

        public void DrawLines(Pen pen, PointModel[] points) {
            if (_g == null || pen == null || points == null || points.Length < 2) return;

            var pts = points.Select(p => Scaler.Scale(p)).ToArray();
            _g.DrawLines(pen, pts);
        }

        public void DrawPie(Brush brush, Pen pen, RectangleModel rectangleModel, int startAngle, int sweepAngle) {
            if (_g == null) return;

            var rectangle = Scaler.Scale(rectangleModel);

            if (brush != null) {
                _g.FillPie(brush, rectangle, startAngle, sweepAngle);
            }

            if (pen != null) {
                _g.DrawPie(pen, rectangle, startAngle, sweepAngle);
            }
        }

        public void DrawPolygon(Brush brush, Pen pen, PointModel[] points) {
            if (_g == null || points == null || points.Length < 2) return;

            var pts = points.Select(p => Scaler.Scale(p)).ToArray();

            if (brush != null) {
                _g.FillPolygon(brush, pts);
            }

            if (pen != null) {
                _g.DrawPolygon(pen, pts);
            }
        }

        public void DrawPath(Brush brush, Pen pen, GraphicsPath path) {
            if (_g == null || path == null) return;

            if (brush != null) {
                _g.FillPath(brush, path);
            }

            if (pen != null) {
                _g.DrawPath(pen, path);
            }
        }

        public Brush GetCachedBrush(string key, Func<Brush> brushCreator) {
            if (!BrushCache.TryGetValue(key, out var brush)) {
                brush = brushCreator();
                BrushCache[key] = brush;
            }
            return brush;
        }

        public Pen GetCachedPen(string key, Func<Pen> penCreator) {
            if (!PenCache.TryGetValue(key, out var pen)) {
                pen = penCreator();
                PenCache[key] = pen;
            }
            return pen;
        }

        private GraphicsPath CreateRoundedRectanglePath(RectangleF rect, float radius) {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void ClearCache() {
            foreach (var brush in BrushCache.Values) {
                brush.Dispose();
            }
            BrushCache.Clear();

            foreach (var pen in PenCache.Values) {
                pen.Dispose();
            }
            PenCache.Clear();
        }

        public void Dispose() {
            ClearCache();
            _g = null;
        }
    }
}