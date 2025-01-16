using GraphEditor.Business.Models;
using GraphEditor.Business.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Services
{
    internal class Painter
    {
        public void Paint(Graphics g, GraphEditorService service, bool isInteractive)
        {
            var rectangles = service.GetRectangles();
            foreach (var rect in rectangles)
            {
                var pen = Pens.Blue;
                var brush = Brushes.Yellow;
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);
            }

            if (isInteractive)
            {
                var activeBrush = Brushes.Black;
                var inactiveBrush = Brushes.White;
                var markerPen = Pens.Black;

                foreach (var marker in service.GetMarkers())
                {
                    var brush = marker.IsActive ? activeBrush : inactiveBrush;
                    g.FillRectangle(brush, marker.Rectangle);
                    g.DrawRectangle(markerPen, marker.Rectangle);
                }
            }
        }
    }
}
