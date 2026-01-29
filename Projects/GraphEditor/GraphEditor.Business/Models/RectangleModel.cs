using System.Drawing;

namespace GraphEditor.Business.Models;

public class RectangleModel
{
    public int Dx { get; set; }

    public int Dy { get; set; }

    public int Left { get; set; }

    public int Top { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int Bottom
    {
        get => Top + Height;
        set => Height = value - Top;
    }

    public int Right
    {
        get => Left + Width;
        set => Width = value - Left;
    }

    public EditMode EditMode { get; set; }

    public Color FillColor { get; set; } = Color.Yellow;

    public Color BorderColor { get; set; } = Color.Blue;

    public bool IsInside(PointModel loc) =>
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
