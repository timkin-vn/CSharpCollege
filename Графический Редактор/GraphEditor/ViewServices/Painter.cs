using GraphEditor.Business.Models;
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
            if (viewModel?.Shapes == null)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (var shape in viewModel.Shapes)
            {
                DrawShape(g, shape);
            }

            if (isInteractive && viewModel.Markers != null)
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

        private void DrawShape(Graphics g, ShapeViewModel shape)
        {
            shape.BorderPen = new Pen(shape.BorderColor, shape.BorderThickness);

            switch (shape.ShapeType)
            {
                case ShapeType.Rectangle:
                    g.FillRectangle(shape.FillBrush, shape.Rectangle);
                    g.DrawRectangle(shape.BorderPen, shape.Rectangle);
                    break;

                case ShapeType.Circle:
                    g.FillEllipse(shape.FillBrush, shape.Rectangle);
                    g.DrawEllipse(shape.BorderPen, shape.Rectangle);
                    break;

                case ShapeType.Triangle:
                    DrawTriangle(g, shape);
                    break;
            }
        }

        private void DrawTriangle(Graphics g, ShapeViewModel shape)
        {
            int x1 = Math.Min(shape.Left, shape.Right);
            int x2 = Math.Max(shape.Left, shape.Right);
            int y1 = Math.Min(shape.Top, shape.Bottom);
            int y2 = Math.Max(shape.Top, shape.Bottom);

            Point[] points;

            if (shape.TrianglePointsUp)
            {
                points = new Point[]
                {
                    new Point(x1 + (x2 - x1) / 2, y1), 
                    new Point(x1, y2),                  
                    new Point(x2, y2)                   
                };
            }
            else
            {
                points = new Point[]
                {
                    new Point(x1 + (x2 - x1) / 2, y2), 
                    new Point(x1, y1),                  
                    new Point(x2, y1)                  
                };
            }

            g.FillPolygon(shape.FillBrush, points);
            g.DrawPolygon(shape.BorderPen, points);
        }
    }
}