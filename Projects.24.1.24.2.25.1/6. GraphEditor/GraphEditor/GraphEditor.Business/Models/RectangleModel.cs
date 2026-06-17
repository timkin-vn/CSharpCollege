using System;
using System.Drawing;

namespace GraphEditor.Business.Models
{
    public class RectangleModel
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

        // Цвета теперь по умолчанию поддерживают Alpha-канал
        public Color FillColor { get; set; } = Color.FromArgb(180, Color.Yellow);
        public Color BorderColor { get; set; } = Color.FromArgb(255, Color.Blue);

        // Новое свойство: толщина обводки
        public float BorderThickness { get; set; } = 2.0f;

        public bool IsInside(PointModel loc) =>
            loc.X >= Math.Min(Left, Right) && loc.X <= Math.Max(Left, Right) &&
            loc.Y >= Math.Min(Top, Bottom) && loc.Y <= Math.Max(Top, Bottom);

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
}
