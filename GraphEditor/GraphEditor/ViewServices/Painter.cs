using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public bool ShowGrid { get; set; } = false;
        public int GridSize { get; set; } = 20;
        public Color GridColor { get; set; } = Color.LightGray;
        public float GridLineWidth { get; set; } = 0.5f;

        public bool ShowSelectionHighlight { get; set; } = true;
        public Color SelectionColor { get; set; } = Color.Red;
        public float SelectionLineWidth { get; set; } = 2f;

        public float ZoomFactor { get; set; } = 1.0f;

        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null)
                return;

            var originalSmoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var originalTransform = g.Transform;
            if (ZoomFactor != 1.0f)
            {
                g.ScaleTransform(ZoomFactor, ZoomFactor);
            }

            if (ShowGrid)
            {
                DrawGrid(g, viewModel);
            }

            if (viewModel.Rectangles != null && viewModel.Rectangles.Any())
            {
                foreach (var rectangle in viewModel.Rectangles)
                {
                    DrawRectangle(g, rectangle, rectangle == viewModel.SelectedRectangle && isInteractive);
                }
            }

            if (isInteractive && ShowSelectionHighlight && viewModel.SelectedRectangle != null)
            {
                DrawSelectionHighlight(g, viewModel.SelectedRectangle);
            }

            if (isInteractive)
            {
                DrawMarkers(g, viewModel);
            }

            g.Transform = originalTransform;
            g.SmoothingMode = originalSmoothingMode;
        }

        private void DrawGrid(Graphics g, PictureViewModel viewModel)
        {
            int maxWidth = 2000;
            int maxHeight = 2000;

            if (viewModel.Rectangles != null && viewModel.Rectangles.Any())
            {
                maxWidth = Math.Max(maxWidth, viewModel.Rectangles.Max(r => r.Rectangle.Right) + 500);
                maxHeight = Math.Max(maxHeight, viewModel.Rectangles.Max(r => r.Rectangle.Bottom) + 500);
            }

            using (var gridPen = new Pen(GridColor, GridLineWidth))
            {
                gridPen.DashStyle = DashStyle.Dot;

                for (int x = 0; x <= maxWidth; x += GridSize)
                {
                    g.DrawLine(gridPen, x, 0, x, maxHeight);
                }

                for (int y = 0; y <= maxHeight; y += GridSize)
                {
                    g.DrawLine(gridPen, 0, y, maxWidth, y);
                }
            }

            int majorGridSize = GridSize * 5;
            using (var majorGridPen = new Pen(Color.FromArgb(180, 180, 180), 1f))
            {
                for (int x = 0; x <= maxWidth; x += majorGridSize)
                {
                    g.DrawLine(majorGridPen, x, 0, x, maxHeight);
                }

                for (int y = 0; y <= maxHeight; y += majorGridSize)
                {
                    g.DrawLine(majorGridPen, 0, y, maxWidth, y);
                }
            }
        }

        private void DrawRectangle(Graphics g, RectangleViewModel rectangle, bool isSelected)
        {
            if (rectangle.ShowShadow)
            {
                DrawShadow(g, rectangle);
            }

            Brush fillBrush;
            if (rectangle.GradientType != GradientType.None)
            {
                fillBrush = CreateGradientBrush(rectangle);
            }
            else
            {
                Color fillColor = rectangle.Opacity < 255
                    ? Color.FromArgb(rectangle.Opacity, rectangle.FillColor)
                    : rectangle.FillColor;
                fillBrush = new SolidBrush(fillColor);
            }

            Color borderColor = rectangle.Opacity < 255
                ? Color.FromArgb(rectangle.Opacity, rectangle.BorderColor)
                : rectangle.BorderColor;

            float borderWidth = isSelected ? 4f : 3f;
            using (var pen = new Pen(borderColor, borderWidth))
            {
                if (rectangle.RotationAngle != 0)
                {
                    DrawRotatedRectangle(g, rectangle, fillBrush, pen);
                }
                else
                {
                    g.FillRectangle(fillBrush, rectangle.Rectangle);
                    g.DrawRectangle(pen, rectangle.Rectangle);
                }
            }

            if (fillBrush is SolidBrush sb)
                sb.Dispose();
            else if (fillBrush is LinearGradientBrush lgb)
                lgb.Dispose();
        }

        private void DrawRotatedRectangle(Graphics g, RectangleViewModel rectangle, Brush brush, Pen pen)
        {
            var rect = rectangle.Rectangle;
            var oldTransform = g.Transform;

            float centerX = rect.X + rect.Width / 2f;
            float centerY = rect.Y + rect.Height / 2f;

            g.TranslateTransform(centerX, centerY);
            g.RotateTransform(rectangle.RotationAngle);
            g.TranslateTransform(-centerX, -centerY);

            g.FillRectangle(brush, rect);
            g.DrawRectangle(pen, rect);

            g.Transform = oldTransform;
        }

        private void DrawShadow(Graphics g, RectangleViewModel rectangle)
        {
            var shadowRect = new Rectangle(
                rectangle.Rectangle.X + rectangle.ShadowOffsetX,
                rectangle.Rectangle.Y + rectangle.ShadowOffsetY,
                rectangle.Rectangle.Width,
                rectangle.Rectangle.Height
            );

            Color shadowColor = Color.FromArgb(100, rectangle.ShadowColor);
            using (var shadowBrush = new SolidBrush(shadowColor))
            {
                if (rectangle.RotationAngle != 0)
                {
                    var oldTransform = g.Transform;
                    float centerX = shadowRect.X + shadowRect.Width / 2f;
                    float centerY = shadowRect.Y + shadowRect.Height / 2f;
                    g.TranslateTransform(centerX, centerY);
                    g.RotateTransform(rectangle.RotationAngle);
                    g.TranslateTransform(-centerX, -centerY);
                    g.FillRectangle(shadowBrush, shadowRect);
                    g.Transform = oldTransform;
                }
                else
                {
                    g.FillRectangle(shadowBrush, shadowRect);
                }
            }
        }

        private LinearGradientBrush CreateGradientBrush(RectangleViewModel rectangle)
        {
            var rect = rectangle.Rectangle;
            float angle = rectangle.GradientAngle;
            return new LinearGradientBrush(rect, rectangle.FillColor, rectangle.FillColor2, angle);
        }

        private void DrawSelectionHighlight(Graphics g, RectangleViewModel rectangle)
        {
            var rect = rectangle.Rectangle;
            int padding = 3;
            var highlightRect = new Rectangle(
                rect.X - padding,
                rect.Y - padding,
                rect.Width + padding * 2,
                rect.Height + padding * 2
            );

            using (var pen = new Pen(SelectionColor, SelectionLineWidth))
            {
                pen.DashStyle = DashStyle.Dash;

                if (rectangle.RotationAngle != 0)
                {
                    var oldTransform = g.Transform;
                    float centerX = rect.X + rect.Width / 2f;
                    float centerY = rect.Y + rect.Height / 2f;
                    g.TranslateTransform(centerX, centerY);
                    g.RotateTransform(rectangle.RotationAngle);
                    g.TranslateTransform(-centerX, -centerY);
                    g.DrawRectangle(pen, highlightRect);
                    g.Transform = oldTransform;
                }
                else
                {
                    g.DrawRectangle(pen, highlightRect);
                }
            }
        }

        private void DrawMarkers(Graphics g, PictureViewModel viewModel)
        {
            if (viewModel.Markers == null || !viewModel.Markers.Any())
                return;

            using (var pen = new Pen(Color.Black, 1))
            {
                foreach (var marker in viewModel.Markers)
                {
                    Brush brush;
                    if (marker.EditMode == EditMode.Rotating)
                    {
                        brush = Brushes.Green;
                    }
                    else
                    {
                        brush = marker.IsActive ? Brushes.Black : Brushes.White;
                    }

                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}