using GraphEditor.Business.Models;

internal class MarkerViewModel
{
    public static int MarkerHalfSize = 3;

    public Rectangle Rectangle { get; set; }
    public bool IsActive { get; set; }
    public EditMode EditMode { get; set; }
    public Cursor Cursor { get; set; }
}