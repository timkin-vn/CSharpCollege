using GraphEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

            foreach (var rectangle in viewModel.Rectangles)
            {
                var pen = new Pen(rectangle.BorderColor, 3);
                var brush = new SolidBrush(rectangle.FillColor);

                g.FillRectangle(brush, rectangle.Rectangle);
                g.DrawRectangle(pen, rectangle.Rectangle);
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
    }
}
