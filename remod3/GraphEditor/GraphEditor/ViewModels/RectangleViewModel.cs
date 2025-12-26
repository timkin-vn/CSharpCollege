using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class RectangleViewModel {
    private static readonly int MarkerHalfSize = MarkerViewModel.MarkerHalfSize;

    public Guid Id { get; private init; }

    private int Left { get; init; }

    private int Top { get; init; }

    private int Width { get; init; }

    private int Height { get; init; }

    public Color FillColor { get; private init; } = Color.Yellow;

    public Brush FillBrush { get; private init; } = Brushes.Yellow;

    public Color BorderColor { get; private init; } = Color.Blue;

    public Pen BorderPen { get; private init; } = Pens.Blue;

    public string? Text { get; private init; }

    public Color TextColor { get; private init; } = Color.Black;

    public string FontFamily { get; private init; } = "Segoe UI";

    public float FontSize { get; private init; } = 10f;

    public TextAlign TextAlign { get; private init; } = TextAlign.Center;

    public bool IsSelected { get; private init; }

    public Rectangle Rectangle => new(
        Left < Right ? Left : Right,
        Top < Bottom ? Top : Bottom,
        Math.Abs(Width),
        Math.Abs(Height));

    private int Right => Left + Width;

    private int Bottom => Top + Height;

    public IEnumerable<MarkerViewModel> Markers => [
        CreateMarker(Left, Top, EditMode.ResizeTl, Cursors.SizeNWSE, false),
        CreateMarker((Left + Right) / 2, Top, EditMode.ResizeT, Cursors.SizeNS, false),
        CreateMarker(Right, Top, EditMode.ResizeTr, Cursors.SizeNESW, false),
        CreateMarker(Right, (Top + Bottom) / 2, EditMode.ResizeR, Cursors.SizeWE, true),
        CreateMarker(Right, Bottom, EditMode.ResizeBr, Cursors.SizeNWSE, true),
        CreateMarker((Left + Right) / 2, Bottom, EditMode.ResizeB, Cursors.SizeNS, false),
        CreateMarker(Left, Bottom, EditMode.ResizeBl, Cursors.SizeNESW, false),
        CreateMarker(Left, (Top + Bottom) / 2, EditMode.ResizeL, Cursors.SizeWE, false)
    ];

    private static MarkerViewModel CreateMarker(int centerX, int centerY, EditMode mode, Cursor cursor, bool isActive) =>
        new() {
            Rectangle = new Rectangle(centerX - MarkerHalfSize, centerY - MarkerHalfSize, MarkerHalfSize * 2, MarkerHalfSize * 2),
            EditMode = mode,
            Cursor = cursor,
            IsActive = isActive,
        };

    public static RectangleViewModel FromBusiness(RectangleModel model, bool isSelected) {
        return new RectangleViewModel {
            Id = model.Id,
            Left = model.Left,
            Top = model.Top,
            Width = model.Width,
            Height = model.Height,
            FillColor = model.FillColor,
            FillBrush = new SolidBrush(model.FillColor),
            BorderColor = model.BorderColor,
            BorderPen = new Pen(model.BorderColor, model.BorderWidth <= 0 ? 1.5f : model.BorderWidth),
            Text = model.Text,
            TextColor = model.TextColor,
            FontFamily = model.FontFamily,
            FontSize = model.FontSize,
            TextAlign = model.TextAlign,
            IsSelected = isSelected,
        };
    }
}
