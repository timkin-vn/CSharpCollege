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
        public void Paint(Graphics g, PictureViewModel viewModel, bool isInteractive, bool showGrid = false, int gridSize = 20)
        {
            if (isInteractive && showGrid)
            {
                DrawGrid(g, gridSize);
            }

            if (viewModel == null || viewModel.Rectangles == null || !viewModel.Rectangles.Any())
                return;

            Pen pen;
            foreach (var rectangle in viewModel.Rectangles)
            {
                pen = new Pen(rectangle.BorderColor, 3);
                var brush = new SolidBrush(rectangle.FillColor);
                g.FillRectangle(brush, rectangle.Rectangle);
                g.DrawRectangle(pen, rectangle.Rectangle);
            }

            if (isInteractive && viewModel.SelectedRectangle != null)
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

        private void DrawGrid(Graphics g, int gridSize)
        {
            var pen = new Pen(Color.FromArgb(40, 0, 0, 0), 1);

            for (int x = 0; x < g.VisibleClipBounds.Width; x += gridSize)
                g.DrawLine(pen, x, 0, x, g.VisibleClipBounds.Height);

            for (int y = 0; y < g.VisibleClipBounds.Height; y += gridSize)
                g.DrawLine(pen, 0, y, g.VisibleClipBounds.Width, y);
        }
    }
}
