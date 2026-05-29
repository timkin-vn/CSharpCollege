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

                // Расчет цвета с учетом прозрачности
                int alpha = (int)(rectangle.Opacity * 255);
                var brush = new SolidBrush(Color.FromArgb(alpha, rectangle.FillColor.R, rectangle.FillColor.G, rectangle.FillColor.B));

                if (rectangle.IsEllipse)
                {
                    g.FillEllipse(brush, rectangle.Rectangle);
                    g.DrawEllipse(pen, rectangle.Rectangle);
                }
                else
                {
                    g.FillRectangle(brush, rectangle.Rectangle);
                    g.DrawRectangle(pen, rectangle.Rectangle);
                }

                pen.Dispose();
                brush.Dispose();
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