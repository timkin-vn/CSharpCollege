using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphEditor.ViewModels;

namespace GraphEditor.ViewServices
{
    public class Painter
    {
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive = true)
        {
            if(viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
            {
                return;
            }
            Pen pen;
            foreach (var item in viewModel.Rectangles)
            {
                pen = new Pen(item.BorderColor, item.BorderWidth);
                var brush = new SolidBrush(item.FillColor);

                g.FillRectangle(brush, item.Rectangle);
                g.DrawRectangle(pen, item.Rectangle);

            }
            if(isInteractive)
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
    }
}
