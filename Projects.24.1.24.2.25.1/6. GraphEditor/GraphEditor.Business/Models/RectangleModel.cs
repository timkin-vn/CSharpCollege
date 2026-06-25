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

        public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;

        public Color FillColor { get; set; } = PictureServiceDefaultColors.DefaultFillColor;

        public Color BorderColor { get; set; } = PictureServiceDefaultColors.DefaultBorderColor;
    }

    internal static class PictureServiceDefaultColors
    {
        public static readonly Color DefaultFillColor = Color.Yellow;

        public static readonly Color DefaultBorderColor = Color.Blue;
    }
}
