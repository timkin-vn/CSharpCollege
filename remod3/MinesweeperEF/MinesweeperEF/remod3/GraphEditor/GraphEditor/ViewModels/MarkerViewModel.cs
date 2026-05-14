using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class MarkerViewModel {
    public const int MarkerHalfSize = 3;

    public Rectangle Rectangle { get; init; }

    public bool IsActive { get; init; }

    public EditMode EditMode { get; init; }

    public Cursor? Cursor { get; init; }
}