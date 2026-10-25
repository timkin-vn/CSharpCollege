using GraphEditor.ViewModels;
using System.Drawing;
using System.Linq;

namespace GraphEditor.ViewServices
{
    internal class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive)
        {
            if (viewModel?.Figures == null) return;

            foreach (var fig in viewModel.Figures)
            {
                using (var pen = new Pen(fig.BorderColor, 2))
                using (var brush = new SolidBrush(fig.FillColor))
                {
                    var rect = fig.Bounds;

                    if (fig.FigureType == "Circle")
                    {
                        g.FillEllipse(brush, rect);
                        g.DrawEllipse(pen, rect);
                    }
                    else if (fig.FigureType == "Triangle")
                    {
                        Point[] points = {
                            new Point(rect.X + rect.Width / 2, rect.Y),
                            new Point(rect.X, rect.Y + rect.Height),
                            new Point(rect.X + rect.Width, rect.Y + rect.Height)
                        };
                        g.FillPolygon(brush, points);
                        g.DrawPolygon(pen, points);
                    }
                    else // Rectangle
                    {
                        g.FillRectangle(brush, rect);
                        g.DrawRectangle(pen, rect);
                    }

                    // Подсветка выбранной фигуры
                    if (isInteractive && fig == viewModel.SelectedFigure)
                    {
                        using (var dashPen = new Pen(Color.Red, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                        {
                            g.DrawRectangle(dashPen, rect.X - 2, rect.Y - 2, rect.Width + 4, rect.Height + 4);
                        }
                    }
                }
            }

            // Отрисовка маркеров изменения размера
            if (isInteractive && viewModel?.SelectedFigure != null)
            {
                foreach (var marker in viewModel.Markers)
                {
                    g.FillRectangle(Brushes.White, marker.Rectangle);
                    g.DrawRectangle(Pens.Black, marker.Rectangle);
                }
            }
        }
    }
}