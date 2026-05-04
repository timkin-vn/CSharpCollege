using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D; // Иначе не работает

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

        private void DrawRectangle(Graphics g, RectangleViewModel rect)
        {
            // Цвет заливки с учётом прозрачности
            Color fillColor = Color.FromArgb(rect.Opacity, rect.FillColor);
            using (var brush = new SolidBrush(fillColor))
            using (var pen = new Pen(rect.BorderColor, 3))
            {
                if (rect.CornerRadius > 0)
                {
                    // Скруглённый прямоугольник
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

        // Вспомогательный метод создания GraphicsPath для скруглённого прямоугольника
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            // Верхняя левая дуга
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            // Верхняя линия
            path.AddLine(rect.X + radius, rect.Y, rect.Right - radius, rect.Y);
            // Верхняя правая дуга
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            // Правая линия
            path.AddLine(rect.Right, rect.Y + radius, rect.Right, rect.Bottom - radius);
            // Нижняя правая дуга
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            // Нижняя линия
            path.AddLine(rect.Right - radius, rect.Bottom, rect.X + radius, rect.Bottom);
            // Нижняя левая дуга
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            // Левая линия
            path.AddLine(rect.X, rect.Bottom - radius, rect.X, rect.Y + radius);

            path.CloseFigure();
            return path;
        }
    }
}