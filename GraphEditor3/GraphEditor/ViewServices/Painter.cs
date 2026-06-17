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

            foreach (var rect in viewModel.Rectangles)
            {
                var pen = rect.BorderPen;

                using (var brush = rect.GetFillBrush())
                {
                    g.FillRectangle(brush, rect.Rectangle);
                    g.DrawRectangle(pen, rect.Rectangle);
                }
            }

            if (isInteractive)
            {
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                var pen = Pens.Black;

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