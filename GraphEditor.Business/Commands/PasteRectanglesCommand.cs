using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class PasteRectanglesCommand : ICommand {
    private readonly PictureModel _picture;
    private readonly List<RectangleModel> _pastedRectangles;

    public string Description => "Paste rectangles";

    public PasteRectanglesCommand(PictureModel picture, IEnumerable<RectangleModel> rectangles) {
        _picture = picture;
        _pastedRectangles = rectangles.ToList();
    }

    public void Execute() {
        foreach (var rect in _pastedRectangles) {
            if (!_picture.Rectangles.Contains(rect)) {
                _picture.Rectangles.Add(rect);
            }
        }
    }

    public void Undo() {
        foreach (var rect in _pastedRectangles) {
            _picture.Rectangles.Remove(rect);
            _picture.SelectedRectangleIds.Remove(rect.Id);
        }
        if (_pastedRectangles.Any(r => r.Id == _picture.SelectedRectangle?.Id)) {
            _picture.SelectedRectangle = null;
        }
    }
}
