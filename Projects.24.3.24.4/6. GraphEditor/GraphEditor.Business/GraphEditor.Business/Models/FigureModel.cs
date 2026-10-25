using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    [Serializable]
    public abstract class FigureModel
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Right => Left + Width;
        public int Bottom => Top + Height;

        public EditMode EditMode { get; set; }

        public Color FillColor { get; set; } = Color.Yellow;
        public Color BorderColor { get; set; } = Color.Blue;

        public virtual bool IsInside(PointModel loc) =>
            loc.X >= Left && loc.X <= Right &&
            loc.Y >= Top && loc.Y <= Bottom;

        public void Normalize()
        {
            if (Width < 0)
            {
                Left += Width;
                Width = -Width;
            }

            if (Height < 0)
            {
                Top += Height;
                Height = -Height;
            }
        }
    }

    public class RectangleModel : FigureModel { }

    public class CircleModel : FigureModel
    {
        // Для круга можно переопределить проверку попадания (вписанный эллипс/круг), 
        // но для простоты пока оставим прямоугольный хитбокс или базовый.
    }

    public class TriangleModel : FigureModel { }
}