using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.Business.Models;
using GraphEditor.ViewModels;

namespace GraphEditor.ViewServices
{
    public class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive = true)
        {
            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
            {
                return;
            }
            Pen pen;
            foreach (var item in viewModel.Rectangles)
            {
                pen = new Pen(item.BorderColor, item.BorderWidth);
                var brush = new SolidBrush(item.FillColor);
                switch (item.Figure) {
                    case FigureType.Rectangle:
                        g.FillRectangle(brush, item.Rectangle);
                        g.DrawRectangle(pen, item.Rectangle);
                        break;
                    case FigureType.Ellipse:
                        g.FillEllipse(brush, item.Rectangle);
                        g.DrawEllipse(pen, item.Rectangle);
                        break;
                    case FigureType.Pentagon:
                        g.FillPolygon(brush, GetPentagonPoints(item.Rectangle));
                        g.DrawPolygon(pen, GetPentagonPoints(item.Rectangle));
                        break;
                    case FigureType.Rhombus:
                        g.FillPolygon(brush, GetRhombusPoints(item.Rectangle));
                        g.DrawPolygon(pen, GetRhombusPoints(item.Rectangle));
                        break;
                }

            }
            if (isInteractive)
            {
                pen = Pens.Black;
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                foreach (var item in viewModel.Markers)
                {
                    var brush = item.IsActive ? activeBrush : inactiveBrush;

                    g.FillRectangle(brush, item.Rectangle);
                    g.DrawRectangle(pen, item.Rectangle);
                }
            }
        }
        private PointF[] GetPentagonPoints(Rectangle bounds)
        {
            PointF[] points = new PointF[5];
            float centerX = bounds.X + bounds.Width / 2;
            float centerY = bounds.Y + bounds.Height / 2;
            float radius = Math.Min(bounds.Width, bounds.Height) / 2;
            for (int i = 0; i < 5; i++)
            {
                double angleInRadians = (i * 72 - 90) * (Math.PI / 180);
                points[i] = new PointF(
                    centerX + (float)(radius * Math.Cos(angleInRadians)),
                    centerY + (float)(radius * Math.Sin(angleInRadians))
                );
            }
            return points;
        }
        private Point[] GetRhombusPoints(Rectangle bounds) => new Point[]{
            new Point(bounds.X + bounds.Width / 2, bounds.Y + 0),
            new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height / 2),
            new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height),
            new Point(bounds.X + 0, bounds.Y + bounds.Height / 2),
        };
    }
}
