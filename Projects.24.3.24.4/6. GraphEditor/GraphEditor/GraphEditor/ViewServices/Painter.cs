using System.Drawing;
using System.Drawing.Drawing2D;
using GraphEditor.Business.Models;

namespace GraphEditor.ViewServices
{
    public class Painter
    {
        public void Paint(Graphics g, PictureModel picture, ShapeModel selected)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            foreach (var shape in picture.Shapes)
            {
                if (shape.Width <= 0 || shape.Height <= 0)
                    continue;

                using (var brush = new SolidBrush(shape.FillColor))
                using (var pen = new Pen(Color.Black, 1f))
                {
                    DrawShape(g, shape, brush, pen);
                }

                if (ReferenceEquals(shape, selected))
                {
                    using (var pen = new Pen(Color.Red, 2f) { DashStyle = DashStyle.Dash })
                    {
                        g.DrawRectangle(pen, shape.X, shape.Y, shape.Width, shape.Height);
                    }
                }
            }
        }

        private void DrawShape(Graphics g, ShapeModel shape, Brush brush, Pen pen)
        {
            if (shape is TriangleModel)
            {
                var triangle = (TriangleModel)shape;
                double ax, ay, blx, bly, brx, bry;
                triangle.GetVertices(out ax, out ay, out blx, out bly, out brx, out bry);

                var points = new[]
                {
                    new PointF((float)ax, (float)ay),
                    new PointF((float)brx, (float)bry),
                    new PointF((float)blx, (float)bly)
                };
                g.FillPolygon(brush, points);
                g.DrawPolygon(pen, points);
            }
            else if (shape is EllipseModel)
            {
                g.FillEllipse(brush, shape.X, shape.Y, shape.Width, shape.Height);
                g.DrawEllipse(pen, shape.X, shape.Y, shape.Width, shape.Height);
            }
            else if (shape is DiamondModel)
            {
                float cx = shape.X + shape.Width / 2f;
                float cy = shape.Y + shape.Height / 2f;
                var points = new[]
                {
                    new PointF(cx, shape.Y),
                    new PointF(shape.X + shape.Width, cy),
                    new PointF(cx, shape.Y + shape.Height),
                    new PointF(shape.X, cy)
                };
                g.FillPolygon(brush, points);
                g.DrawPolygon(pen, points);
            }
            else
            {
                g.FillRectangle(brush, shape.X, shape.Y, shape.Width, shape.Height);
                g.DrawRectangle(pen, shape.X, shape.Y, shape.Width, shape.Height);
            }
        }
    }
}
