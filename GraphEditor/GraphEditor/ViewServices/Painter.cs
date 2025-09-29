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
            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
            {
                return;
            }

            foreach (var rect in viewModel.Rectangles)
            {
                if (rect.Rectangle.Width <= 0 || rect.Rectangle.Height <= 0)
                {
                    continue;
                }
                if (rect.UseGradient)
                {
                    using (var gradientBrush = new LinearGradientBrush(rect.Rectangle,rect.FillColor, rect.GradientColor, LinearGradientMode.ForwardDiagonal))
                    {
                        g.FillRectangle(gradientBrush, rect.Rectangle);
                    }
                }
                else
                {
                    var brush = rect.FillBrush;
                    g.FillRectangle(brush, rect.Rectangle);
                }

                var pen = rect.BorderPen;
                g.DrawRectangle(pen, rect.Rectangle);
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
