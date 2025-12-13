using GraphEditor.Business.Models;

namespace GraphEditor.Business.Services;

public class PictureService {
    public PictureModel PictureModel { get; private set; } = new();

    public static Color DefaultFillColor = Color.Yellow;

    private static readonly Color DefaultBorderColor = Color.Blue;

    public PictureService() {
        PictureModel.Rectangles.Add(new RectangleModel { Left = 100, Top = 50, Width = 200, Height = 150, FillColor = Color.LightCyan });
        var newRectangle = new RectangleModel { Left = 200, Top = 100, Width = 200, Height = 150 };
        PictureModel.Rectangles.Add(newRectangle);
        SelectExclusive(newRectangle);
        PictureModel.Rectangles.Add(new RectangleModel { Left = 300, Top = 200, Width = 200, Height = 150, FillColor = Color.Pink });
    }

    public void CreateRectangle(PointModel loc) {
        var newRectangle = new RectangleModel {
            Left = loc.X,
            Top = loc.Y,
            Width = 0,
            Height = 0,
            FillColor = DefaultFillColor,
            BorderColor = DefaultBorderColor
        };
        PictureModel.Rectangles.Add(newRectangle);
        SelectExclusive(newRectangle);
        newRectangle.EditMode = EditMode.Creating;
    }
        
    public void SetText(string text) {
        if (PictureModel.SelectedRectangle != null) {
            PictureModel.SelectedRectangle.Text = text;
        }
    }
        
    public PictureModel GetPictureModel() {
        return PictureModel;
    }

    public bool GroupSelected(string? name = null) {
        var selectedIds = PictureModel.SelectedRectangleIds.ToList();
        if (selectedIds.Count < 2) {
            return false;
        }

        foreach (var group in PictureModel.Groups.ToList().Where(group => group.RectangleIds.Any(id => selectedIds.Contains(id)))) {
            foreach (var id in selectedIds) {
                group.Remove(id);
            }
            if (group.IsEmpty) {
                PictureModel.Groups.Remove(group);
            }
        }
            
        var g = new GroupModel { Name = name };
        foreach (var id in selectedIds) {
            g.Add(id);
        }
        PictureModel.Groups.Add(g);
        return true;
    }

    public void Ungroup(Guid groupId) {
        var group = PictureModel.Groups.FirstOrDefault(g => g.Id == groupId);
        if (group == null) {
            return;
        }

        PictureModel.Groups.Remove(group);
    }

    public void DeleteRectangle() {
        if (!PictureModel.SelectedRectangleIds.Any()) {
            return;
        }

        var toDelete = GetSelectedRectangles().ToList();
        foreach (var rect in toDelete) {
            PictureModel.Rectangles.Remove(rect);
            foreach (var group in PictureModel.Groups.ToList()) {
                group.Remove(rect.Id);
                if (group.IsEmpty) {
                    PictureModel.Groups.Remove(group);
                }
            }
        }

        ClearSelection();
    }

    public void SetResizeMode(EditMode mode) {
        if (PictureModel.SelectedRectangle != null) {
            PictureModel.SelectedRectangle.EditMode = mode;
        }
    }

    public void SetMoveMode(PointModel loc, bool additiveSelection) {
        var selectedRect = SelectAt(loc, additiveSelection, includeGroup: !additiveSelection);
        PictureModel.SelectedRectangle = selectedRect;

        if (selectedRect == null) {
            return;
        }

        foreach (var rect in GetSelectedRectangles()) {
            rect.Dx = loc.X - rect.Left;
            rect.Dy = loc.Y - rect.Top;
            rect.EditMode = EditMode.Moving;
        }
    }

    public void ResetMode() {
        foreach (var rect in GetSelectedRectangles()) {
            rect.Normalize();
            rect.EditMode = EditMode.None;
        }
    }

    public void UpdateMovingPoint(PointModel loc) {
        var selectedRect = PictureModel.SelectedRectangle;
        if (selectedRect == null) {
            return;
        }

        switch (selectedRect.EditMode) {
            case EditMode.Creating:
            case EditMode.ResizeBr:
                selectedRect.Right = loc.X;
                selectedRect.Bottom = loc.Y;
                break;

            case EditMode.ResizeR:
                selectedRect.Right = loc.X;
                break;

            case EditMode.Moving:
                foreach (var rect in GetSelectedRectangles()) {
                    rect.Left = loc.X - rect.Dx;
                    rect.Top = loc.Y - rect.Dy;
                }
                break;
        }
    }

    public void OpenFile(string fileName) {
        PictureModel = FileService.OpenFile(fileName);
    }

    public void SaveToFile(string fileName) {
        var fileService = new FileService();
        FileService.SaveToFile(fileName, PictureModel);
    }

    public void CreateNewPicture() {
        PictureModel.Rectangles.Clear();
        PictureModel.Groups.Clear();
        ClearSelection();
    }

    public void SetFillColor(Color color) {
        foreach (var rect in GetSelectedRectangles()) {
            rect.FillColor = color;
        }
    }

    public void MoveForward() {
        if (PictureModel.SelectedRectangle == null) {
            return;
        }

        var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
        if (index < 0 || index == PictureModel.Rectangles.Count - 1) {
            return;
        }

        (PictureModel.Rectangles[index + 1], PictureModel.Rectangles[index]) = (PictureModel.Rectangles[index], PictureModel.Rectangles[index + 1]);
    }
        
    public RectangleModel? SelectAt(PointModel loc, bool additiveSelection, bool includeGroup = true) {
        var rect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
        UpdateSelection(rect, additiveSelection, includeGroup);
        return rect;
    }

    private void SelectExclusive(RectangleModel? rect) {
        UpdateSelection(rect, additiveSelection: false, includeGroup: false);
    }

    private IEnumerable<RectangleModel> GetSelectedRectangles() =>
        PictureModel.Rectangles.Where(r => PictureModel.SelectedRectangleIds.Contains(r.Id));

    private void UpdateSelection(RectangleModel? rect, bool additiveSelection, bool includeGroup) {
        if (!additiveSelection) {
            PictureModel.SelectedRectangleIds.Clear();
            PictureModel.SelectedRectangle = null;
        }

        if (rect == null)
            return;

        if (additiveSelection && PictureModel.SelectedRectangleIds.Contains(rect.Id)) {
            PictureModel.SelectedRectangleIds.Remove(rect.Id);
            if (PictureModel.SelectedRectangle?.Id == rect.Id)
                PictureModel.SelectedRectangle = GetSelectedRectangles().LastOrDefault();
            return;
        }

        PictureModel.SelectedRectangleIds.Add(rect.Id);

        PictureModel.SelectedRectangle = rect;

        if (!includeGroup) return;
        var group = PictureModel.Groups.FirstOrDefault(g => g.Contains(rect.Id));
        if (group == null) return;
        foreach (var member in group.Resolve(PictureModel))
            PictureModel.SelectedRectangleIds.Add(member.Id);
    }

    private void ClearSelection() {
        PictureModel.SelectedRectangle = null;
        PictureModel.SelectedRectangleIds.Clear();
    }
}