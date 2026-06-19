using System.Drawing;
using System.Drawing.Drawing2D;
using TriangleEditor.Business.Models;

namespace TriangleEditor.ViewServices
{
    // Отрисовка рисунка на Graphics. Знает, как изображать конкретные фигуры.
    public class Painter
    {
        public void Paint(Graphics g, PictureModel picture, ShapeModel selected)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            foreach (var shape in picture.Shapes)
            {
                var triangle = shape as TriangleModel;
                if (triangle == null) continue;

                double ax, ay, blx, bly, brx, bry;
                triangle.GetVertices(out ax, out ay, out blx, out bly, out brx, out bry);

                var points = new[]
                {
                    new PointF((float)ax, (float)ay),
                    new PointF((float)brx, (float)bry),
                    new PointF((float)blx, (float)bly)
                };

                using (var brush = new SolidBrush(triangle.FillColor))
                {
                    g.FillPolygon(brush, points);
                }
                using (var pen = new Pen(Color.Black, 1f))
                {
                    g.DrawPolygon(pen, points);
                }

                // Рамка выделения у выбранной фигуры (по габаритному прямоугольнику).
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
