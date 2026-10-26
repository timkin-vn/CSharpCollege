using GraphEditor.Business.Models;
using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Pen pen;
            foreach (var rectangle in viewModel.Rectangles)
            {
                pen = new Pen(rectangle.BorderColor, 3);
                var brush = new SolidBrush(rectangle.FillColor);

                if (rectangle.Model is EllipseModel)
                {
                    g.FillEllipse(brush, rectangle.Rectangle);
                    g.DrawEllipse(pen, rectangle.Rectangle);
                }
                else if (rectangle.Model is RhombusModel)
                {
                    var rect = rectangle.Rectangle;
                    var points = new Point[] {
                        new Point(rect.X + rect.Width / 2, rect.Y),
                        new Point(rect.X + rect.Width, rect.Y + rect.Height / 2),
                        new Point(rect.X + rect.Width / 2, rect.Y + rect.Height),
                        new Point(rect.X, rect.Y + rect.Height / 2)
                    };
                    g.FillPolygon(brush, points);
                    g.DrawPolygon(pen, points);
                }
                else
                {
                    g.FillRectangle(brush, rectangle.Rectangle);
                    g.DrawRectangle(pen, rectangle.Rectangle);
                }
            }

            if (isInteractive)
            {
                pen = Pens.Black;
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
    }
}