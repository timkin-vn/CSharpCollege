using GraphEditor.Business.Models;
using GraphEditor.Business.Commands;

namespace GraphEditor.Business.Services;

public class PictureService {
    public PictureModel PictureModel { get; private set; } = new();

    public static readonly Color DefaultFillColor = Color.Yellow;
    public static readonly Color DefaultBorderColor = Color.Blue;

    private readonly CommandHistory _commandHistory = new();
    private List<RectangleModel> _clipboard = [];

    public bool CanUndo => _commandHistory.CanUndo;
    public bool CanRedo => _commandHistory.CanRedo;
    public bool HasClipboard => _clipboard.Count > 0;

    public void CreateRectangle(PointModel loc) {
        var newRectangle = new RectangleModel {
            Left = loc.X,
            Top = loc.Y,
            Width = 0,
            Height = 0,
            FillColor = DefaultFillColor,
            BorderColor = DefaultBorderColor,
        };

        var command = new CreateRectangleCommand(PictureModel, newRectangle);
        _commandHistory.Execute(command);

        SelectExclusive(newRectangle);
        newRectangle.EditMode = EditMode.Creating;
    }

    public void SetText(string? text) {
        var selected = PictureModel.SelectedRectangle;
        if (selected == null) return;
        var command = new ChangePropertyCommand<string?>(
            [selected], text,
            r => r.Text,
            (r, t) => r.Text = t);
        _commandHistory.Execute(command);
    }

    public PictureModel GetPictureModel() {
        return PictureModel;
    }

    public void SetBorderWidth(float width) {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count == 0) return;

        var command = new ChangePropertyCommand<float>(
            selected, width,
            r => r.BorderWidth,
            (r, w) => r.BorderWidth = w);
        _commandHistory.Execute(command);
    }

    public void SetBorderColor(Color color) {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count == 0) return;

        var command = new ChangePropertyCommand<Color>(
            selected, color,
            r => r.BorderColor,
            (r, c) => r.BorderColor = c);
        _commandHistory.Execute(command);
    }

    public bool GroupSelected(string? name = null) {
        var selectedIds = PictureModel.SelectedRectangleIds.ToList();
        if (selectedIds.Count < 2) return false;
        var command = new GroupCommand(PictureModel, selectedIds, name);
        _commandHistory.Execute(command);
        return true;
    }

    public void Ungroup(Guid groupId) {
        var group = PictureModel.Groups.FirstOrDefault(g => g.Id == groupId);
        if (group == null) return;
        var command = new UngroupCommand(PictureModel, group);
        _commandHistory.Execute(command);
    }

    public void DeleteRectangle() {
        if (!PictureModel.SelectedRectangleIds.Any()) {
            return;
        }

        var toDelete = GetSelectedRectangles().ToList();
        var command = new DeleteRectanglesCommand(PictureModel, toDelete);
        _commandHistory.Execute(command);
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
        _commandHistory.Clear();
    }

    public void SaveToFile(string fileName) {
        FileService.SaveToFile(fileName, PictureModel);
    }

    public void CreateNewPicture() {
        PictureModel.Rectangles.Clear();
        PictureModel.Groups.Clear();
        ClearSelection();
        _commandHistory.Clear();
    }

    public void SetFillColor(Color color) {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count == 0) return;

        var command = new ChangePropertyCommand<Color>(
            selected, color,
            r => r.FillColor,
            (r, c) => r.FillColor = c);
        _commandHistory.Execute(command);
    }

    public void MoveForward() {
        if (PictureModel.SelectedRectangle == null) return;
        var index = PictureModel.Rectangles.IndexOf(PictureModel.SelectedRectangle);
        if (index < 0 || index == PictureModel.Rectangles.Count - 1) return;
        var command = new MoveForwardCommand(PictureModel.Rectangles, index);
        _commandHistory.Execute(command);
    }

    public RectangleModel? SelectAt(PointModel loc, bool additiveSelection, bool includeGroup = true) {
        var rect = PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
        UpdateSelection(rect, additiveSelection, includeGroup);
        return rect;
    }

    public RectangleModel? FindRectangle(PointModel loc) {
        return PictureModel.Rectangles.LastOrDefault(r => r.IsInside(loc));
    }

    private void SelectExclusive(RectangleModel? rect) {
        UpdateSelection(rect, additiveSelection: false, includeGroup: false);
    }

    public void Undo() {
        _commandHistory.Undo();
    }

    public void Redo() {
        _commandHistory.Redo();
    }

    public void CopySelected() {
        _clipboard = GetSelectedRectangles().Select(r => r.Clone()).ToList();
    }

    public void Paste(int offsetX = 20, int offsetY = 20) {
        if (!HasClipboard) return;

        var newRects = _clipboard.Select(r => r.Clone(offsetX, offsetY)).ToList();
        var command = new PasteRectanglesCommand(PictureModel, newRects);
        _commandHistory.Execute(command);

        ClearSelection();
        foreach (var rect in newRects) {
            PictureModel.SelectedRectangleIds.Add(rect.Id);
        }
        PictureModel.SelectedRectangle = newRects.LastOrDefault();

        _clipboard = newRects.Select(r => r.Clone()).ToList();
    }

    public void DuplicateSelected(int offsetX = 15, int offsetY = 15) {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count == 0) return;

        var newRects = selected.Select(r => r.Clone(offsetX, offsetY)).ToList();
        var command = new PasteRectanglesCommand(PictureModel, newRects);
        _commandHistory.Execute(command);

        ClearSelection();
        foreach (var rect in newRects) {
            PictureModel.SelectedRectangleIds.Add(rect.Id);
        }
        PictureModel.SelectedRectangle = newRects.LastOrDefault();
    }

    public void MoveSelected(int deltaX, int deltaY) {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count == 0) return;

        var command = new MoveRectanglesCommand(selected, deltaX, deltaY);
        _commandHistory.Execute(command);
    }

    public void AlignLeft() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var minLeft = selected.Min(r => r.Left);
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, minLeft, r.Top))));
    }

    public void AlignRight() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var maxRight = selected.Max(r => r.Right);
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, maxRight - r.Width, r.Top))));
    }

    public void AlignTop() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var minTop = selected.Min(r => r.Top);
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, r.Left, minTop))));
    }

    public void AlignBottom() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var maxBottom = selected.Max(r => r.Bottom);
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, r.Left, maxBottom - r.Height))));
    }

    public void AlignCenterHorizontal() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var centerX = (selected.Min(r => r.Left) + selected.Max(r => r.Right)) / 2;
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, centerX - r.Width / 2, r.Top))));
    }

    public void AlignCenterVertical() {
        var selected = GetSelectedRectangles().ToList();
        if (selected.Count < 2) return;
        var centerY = (selected.Min(r => r.Top) + selected.Max(r => r.Bottom)) / 2;
        _commandHistory.Execute(new SetPositionsCommand(selected.Select(r => (r, r.Left, centerY - r.Height / 2))));
    }

    public void DistributeHorizontally() {
        var selected = GetSelectedRectangles().OrderBy(r => r.Left).ToList();
        if (selected.Count < 3) return;
        var totalWidth = selected.Sum(r => r.Width);
        var minLeft = selected.Min(r => r.Left);
        var maxRight = selected.Max(r => r.Right);
        double gap = (double)(maxRight - minLeft - totalWidth) / (selected.Count - 1);
        double currentLeft = minLeft;
        var positions = selected.Select(r => {
            var newLeft = (int)Math.Round(currentLeft);
            currentLeft += r.Width + gap;
            return (r, newLeft, r.Top);
        }).ToList();
        _commandHistory.Execute(new SetPositionsCommand(positions));
    }

    public void DistributeVertically() {
        var selected = GetSelectedRectangles().OrderBy(r => r.Top).ToList();
        if (selected.Count < 3) return;
        var totalHeight = selected.Sum(r => r.Height);
        var minTop = selected.Min(r => r.Top);
        var maxBottom = selected.Max(r => r.Bottom);
        double gap = (double)(maxBottom - minTop - totalHeight) / (selected.Count - 1);
        double currentTop = minTop;
        var positions = selected.Select(r => {
            var newTop = (int)Math.Round(currentTop);
            currentTop += r.Height + gap;
            return (r, r.Left, newTop);
        }).ToList();
        _commandHistory.Execute(new SetPositionsCommand(positions));
    }

    private IEnumerable<RectangleModel> GetSelectedRectangles() =>
        PictureModel.Rectangles.Where(r => PictureModel.SelectedRectangleIds.Contains(r.Id));

    private void UpdateSelection(RectangleModel? rect, bool additiveSelection, bool includeGroup) {
        if (!additiveSelection) {
            PictureModel.SelectedRectangleIds.Clear();
        }

        if (rect == null) {
            if (!additiveSelection) {
                PictureModel.SelectedRectangle = null;
            }
            return;
        }

        if (additiveSelection && PictureModel.SelectedRectangleIds.Contains(rect.Id)) {
            PictureModel.SelectedRectangleIds.Remove(rect.Id);
            if (PictureModel.SelectedRectangle?.Id == rect.Id) {
                PictureModel.SelectedRectangle = GetSelectedRectangles().LastOrDefault();
            }
            return;
        }

        PictureModel.SelectedRectangle = rect;
        PictureModel.SelectedRectangleIds.Add(rect.Id);

        if (!includeGroup) return;
        var group = PictureModel.Groups.FirstOrDefault(g => g.Contains(rect.Id));
        if (group == null) return;
        foreach (var member in group.Resolve(PictureModel)) {
            PictureModel.SelectedRectangleIds.Add(member.Id);
        }
    }

    private void ClearSelection() {
        PictureModel.SelectedRectangle = null;
        PictureModel.SelectedRectangleIds.Clear();
    }
}
