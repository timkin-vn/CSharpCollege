using System.Drawing;
using System.Drawing.Drawing2D;
using EllipseEditor.Business.Models;

namespace EllipseEditor.ViewServices
{

    public class Painter
    {
        public void Paint(Graphics g, PictureModel picture, ShapeModel selected)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            foreach (var shape in picture.Shapes)
            {
                var ellipse = shape as EllipseModel;
                if (ellipse == null) continue;

                using (var brush = new SolidBrush(ellipse.FillColor))
                {
                    g.FillEllipse(brush, ellipse.X, ellipse.Y, ellipse.Width, ellipse.Height);
                }
                using (var pen = new Pen(Color.Black, 1f))
                {
                    g.DrawEllipse(pen, ellipse.X, ellipse.Y, ellipse.Width, ellipse.Height);
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
    }
}
