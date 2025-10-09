using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class RectangleViewModel {
    private const int MarkerHalfSize = MarkerViewModel.MarkerHalfSize;

    public Guid Id { get; init; }

    public int Left { get; init; }

    public int Top { get; init; }

    public int Width { get; init; }

    public int Height { get; init; }

    public Color FillColor { get; init; } = Color.Yellow;

    public Brush FillBrush { get; init; } = Brushes.Yellow;

    public Color BorderColor { get; init; } = Color.Blue;

    public float BorderWidth { get; init; } = 1.5f;

    public Pen BorderPen { get; init; } = Pens.Blue;

    public string? Text { get; init; }

    public Color TextColor { get; init; } = Color.Black;

    public string FontFamily { get; init; } = "Segoe UI";

    public float FontSize { get; init; } = 10f;

    public TextAlign TextAlign { get; init; } = TextAlign.Center;

    public bool IsSelected { get; init; }

    public EditMode EditMode { get; init; }

    public Rectangle Rectangle => new(
        Left < Right ? Left : Right,
        Top < Bottom ? Top : Bottom,
        Math.Abs(Width),
        Math.Abs(Height));

    private int Right => Left + Width;

    private int Bottom => Top + Height;

    public IEnumerable<MarkerViewModel> Markers => new[] {
        CreateMarker(Left, Top, EditMode.ResizeTL, Cursors.SizeNWSE, false),
        CreateMarker((Left + Right) / 2, Top, EditMode.ResizeT, Cursors.SizeNS, false),
        CreateMarker(Right, Top, EditMode.ResizeTR, Cursors.SizeNESW, false),
        CreateMarker(Right, (Top + Bottom) / 2, EditMode.ResizeR, Cursors.SizeWE, true),
        CreateMarker(Right, Bottom, EditMode.ResizeBR, Cursors.SizeNWSE, true),
        CreateMarker((Left + Right) / 2, Bottom, EditMode.ResizeB, Cursors.SizeNS, false),
        CreateMarker(Left, Bottom, EditMode.ResizeBL, Cursors.SizeNESW, false),
        CreateMarker(Left, (Top + Bottom) / 2, EditMode.ResizeL, Cursors.SizeWE, false),
    };

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
            BorderWidth = model.BorderWidth <= 0 ? 1.5f : model.BorderWidth,
            BorderPen = new Pen(model.BorderColor, model.BorderWidth <= 0 ? 1.5f : model.BorderWidth),
            Text = model.Text,
            TextColor = model.TextColor,
            FontFamily = model.FontFamily,
            FontSize = model.FontSize,
            TextAlign = model.TextAlign,
            IsSelected = isSelected,
            EditMode = model.EditMode,
        };
    }
}
