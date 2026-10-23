using System.Drawing;
using System.Linq;
using EllipseEditor.Business.Models;

namespace EllipseEditor.Business.Services
{

    public class PictureService
    {
        private int _nextId = 1;

        public EllipseModel AddEllipse(PictureModel picture, int x, int y, int width, int height)
        {
            var ellipse = new EllipseModel
            {
                Id = _nextId++,
                X = x,
                Y = y,
                Width = width,
                Height = height,
                FillColor = Color.CornflowerBlue
            };
            picture.Shapes.Add(ellipse);
            return ellipse;
        }

        public ShapeModel HitTest(PictureModel picture, int x, int y)
        {
            for (int i = picture.Shapes.Count - 1; i >= 0; i--)
            {
                if (picture.Shapes[i].Contains(x, y))
                    return picture.Shapes[i];
            }
            return null;
        }

        public void Move(ShapeModel shape, int dx, int dy)
        {
            if (shape == null) return;
            shape.X += dx;
            shape.Y += dy;
        }

        public void Resize(ShapeModel shape, int newWidth, int newHeight)
        {
            if (shape == null) return;
            if (newWidth < 5) newWidth = 5;
            if (newHeight < 5) newHeight = 5;
            shape.Width = newWidth;
            shape.Height = newHeight;
        }

        public void SetColor(ShapeModel shape, Color color)
        {
            if (shape == null) return;
            shape.FillColor = color;
        }

        public void Remove(PictureModel picture, ShapeModel shape)
        {
            if (shape == null) return;
            picture.Shapes.Remove(shape);
        }

        public void EnsureNextId(PictureModel picture)
        {
            _nextId = picture.Shapes.Any() ? picture.Shapes.Max(s => s.Id) + 1 : 1;
        }
    }
}
