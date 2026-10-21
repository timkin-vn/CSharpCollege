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

            Pen pen;
            foreach (var rectangle in viewModel.Rectangles)
            {
                Point[] rhombus = null;
                Point[] triangle = null;

                pen = new Pen(rectangle.BorderColor, 3);
                Brush brush;

                switch (rectangle.FillType)
                {
                    case FillType.None:
                        brush = null;
                        break;

                    case FillType.Horizontal:
                        brush = new System.Drawing.Drawing2D.HatchBrush(
                            System.Drawing.Drawing2D.HatchStyle.Horizontal,
                            rectangle.FillColor,
                            Color.White);
                        break;

                    case FillType.Vertical:
                        brush = new System.Drawing.Drawing2D.HatchBrush(
                            System.Drawing.Drawing2D.HatchStyle.Vertical,
                            rectangle.FillColor,
                            Color.White);
                        break;

                    case FillType.Diagonal:
                        brush = new System.Drawing.Drawing2D.HatchBrush(
                            System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal,
                            rectangle.FillColor,
                            Color.White);
                        break;

                    default:
                        brush = new SolidBrush(rectangle.FillColor);
                        break;
                }

                switch (rectangle.ShapeType)
                {
                    case ShapeType.Rectangle:

                        if (brush != null)
                            g.FillRectangle(brush, rectangle.Rectangle);

                        g.DrawRectangle(pen, rectangle.Rectangle);
                        break;

                    case ShapeType.Rhombus:

                        rhombus = new[]
                        {
                          new Point(rectangle.Rectangle.Left + rectangle.Rectangle.Width / 2, rectangle.Rectangle.Top),
                          new Point(rectangle.Rectangle.Right, rectangle.Rectangle.Top + rectangle.Rectangle.Height / 2),
                          new Point(rectangle.Rectangle.Left + rectangle.Rectangle.Width / 2, rectangle.Rectangle.Bottom),
                          new Point(rectangle.Rectangle.Left, rectangle.Rectangle.Top + rectangle.Rectangle.Height / 2)
                        };

                        if (brush != null)
                            g.FillPolygon(brush, rhombus);

                        g.DrawPolygon(pen, rhombus);
                        break;

                    case ShapeType.Triangle:

                        triangle = new[]
                        {
                          new Point(rectangle.Rectangle.Left + rectangle.Rectangle.Width / 2, rectangle.Rectangle.Top),
                          new Point(rectangle.Rectangle.Right, rectangle.Rectangle.Bottom),
                          new Point(rectangle.Rectangle.Left, rectangle.Rectangle.Bottom)
                        };

                        if (brush != null)
                            g.FillPolygon(brush, triangle);

                        g.DrawPolygon(pen, triangle);
                        break;
                }

                if (rectangle.IsSelected)
                {
                    Pen selectPen = new Pen(Color.Red, 2);

                    switch (rectangle.ShapeType)
                    {
                        case ShapeType.Rectangle:
                            g.DrawRectangle(selectPen, rectangle.Rectangle);
                            break;

                        case ShapeType.Rhombus:
                            g.DrawPolygon(selectPen, rhombus);
                            break;

                        case ShapeType.Triangle:
                            g.DrawPolygon(selectPen, triangle);
                            break;
                    }
                }
            }

            if (isInteractive && viewModel.SelectedRectangle != null)
            {
                pen = Pens.Black;

                foreach (var marker in viewModel.SelectedRectangle.ShapeMarkers)
                {
                    g.FillRectangle(Brushes.White, marker.Rectangle);
                    g.DrawRectangle(pen, marker.Rectangle);
                }
            }
        }
    }
}
