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
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel == null) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // тени для всех фигур
            if (viewModel.Rectangles != null)
            {
                foreach (var rectangle in viewModel.Rectangles)
                {
                    DrawShadow(g, rectangle.Rectangle);
                }
            }

            if (viewModel.Circles != null)
            {
                foreach (var circle in viewModel.Circles)
                {
                    DrawShadow(g, circle.BoundingRectangle);
                }
            }

            // основные фигуры
            if (viewModel.Rectangles != null)
            {
                foreach (var rectangle in viewModel.Rectangles)
                {
                    var isSelected = viewModel.SelectedRectangle == rectangle;
                    DrawRectangleWithStyle(g, rectangle, isSelected);
                }
            }

            if (viewModel.Circles != null)
            {
                foreach (var circle in viewModel.Circles)
                {
                    var isSelected = viewModel.SelectedCircle == circle;
                    DrawCircleWithStyle(g, circle, isSelected);
                }
            }

            // Маркеры поверх всего
            if (isInteractive)
            {
                DrawMarkers(g, viewModel.Markers);
            }
        }

        private void DrawShadow(Graphics g, Rectangle rect)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;

            var shadowRect = new Rectangle(rect.X + 4, rect.Y + 4, rect.Width, rect.Height);

            // Градиентная тень
            using (var path = new GraphicsPath())
            {
                path.AddRectangle(shadowRect);
                using (var shadowBrush = new PathGradientBrush(path))
                {
                    shadowBrush.CenterColor = Color.FromArgb(40, Color.Black);
                    shadowBrush.SurroundColors = new[] { Color.FromArgb(0, Color.Black) };
                    g.FillRectangle(shadowBrush, shadowRect);
                }
            }
        }

        private void DrawRectangleWithStyle(Graphics g, RectangleViewModel rect, bool isSelected)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;

            var gradientRect = rect.Rectangle;

            // Градиентная заливка
            using (var gradientBrush = new LinearGradientBrush(
                gradientRect,
                rect.FillColor.Lighten(0.3f),
                rect.FillColor.Darken(0.2f),
                45f))
            {
                g.FillRectangle(gradientBrush, gradientRect);
            }
            var borderWidth = isSelected ? 4 : 2;
            using (var pen = new Pen(rect.BorderColor, borderWidth))
            {
                if (isSelected)
                {
                    pen.DashStyle = DashStyle.Dash;
                    pen.Color = Color.FromArgb(200, Color.Gold);
                }
                g.DrawRectangle(pen, rect.Rectangle);
            }

            // Эффект выделения свечения
            if (isSelected)
            {
                using (var glowPen = new Pen(Color.FromArgb(100, Color.Yellow), 8))
                {
                    glowPen.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(glowPen, rect.Rectangle);
                }
            }
        }

        private void DrawCircleWithStyle(Graphics g, CircleViewModel circle, bool isSelected)
        {
            if (circle.Radius <= 0) return;

            // Градиент для круга
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(circle.BoundingRectangle);
                using (var gradientBrush = new PathGradientBrush(path))
                {
                    gradientBrush.CenterColor = circle.FillColor.Lighten(0.4f);
                    gradientBrush.SurroundColors = new[] { circle.FillColor.Darken(0.1f) };
                    g.FillEllipse(gradientBrush, circle.BoundingRectangle);
                }
            }
            var borderWidth = isSelected ? 4 : 2;
            using (var pen = new Pen(circle.BorderColor, borderWidth))
            {
                if (isSelected)
                {
                    pen.DashStyle = DashStyle.Dash;
                    pen.Color = Color.FromArgb(200, Color.Gold);
                }
                g.DrawEllipse(pen, circle.BoundingRectangle);
            }

            // Эффект выделения - свечение
            if (isSelected)
            {
                using (var glowPen = new Pen(Color.FromArgb(100, Color.Yellow), 8))
                {
                    glowPen.DashStyle = DashStyle.Dash;
                    g.DrawEllipse(glowPen, circle.BoundingRectangle);
                }
            }
        }

        private void DrawMarkers(Graphics g, IEnumerable<MarkerViewModel> markers)
        {
            if (markers == null) return;

            foreach (var marker in markers)
            {
                if (!marker.IsActive) continue;

                // Красивые маркеры с градиентом
                using (var markerBrush = new LinearGradientBrush(
                    marker.Rectangle,
                    Color.White,
                    Color.DarkBlue,
                    45f))
                {
                    g.FillRectangle(markerBrush, marker.Rectangle);
                }

                // Обводка маркера
                using (var pen = new Pen(Color.Black, 1))
                {
                    g.DrawRectangle(pen, marker.Rectangle);
                }

                // Внутренний крестик для лучшей видимости
                var centerX = marker.Rectangle.X + marker.Rectangle.Width / 2;
                var centerY = marker.Rectangle.Y + marker.Rectangle.Height / 2;
                var crossSize = 3;

                using (var crossPen = new Pen(Color.White, 1))
                {
                    g.DrawLine(crossPen, centerX - crossSize, centerY, centerX + crossSize, centerY);
                    g.DrawLine(crossPen, centerX, centerY - crossSize, centerX, centerY + crossSize);
                }
            }
        }
    }

    public static class ColorExtensions
    {
        public static Color Lighten(this Color color, float factor)
        {
            factor = Math.Max(0, Math.Min(1, factor));
            return Color.FromArgb(
                color.A,
                Math.Min(255, (int)(color.R + (255 - color.R) * factor)),
                Math.Min(255, (int)(color.G + (255 - color.G) * factor)),
                Math.Min(255, (int)(color.B + (255 - color.B) * factor))
            );
        }

        public static Color Darken(this Color color, float factor)
        {
            factor = Math.Max(0, Math.Min(1, factor));
            return Color.FromArgb(
                color.A,
                Math.Max(0, (int)(color.R * (1 - factor))),
                Math.Max(0, (int)(color.G * (1 - factor))),
                Math.Max(0, (int)(color.B * (1 - factor)))
            );
        }
    }
}