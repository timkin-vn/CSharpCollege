using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class PasteRectanglesCommand(PictureModel picture, IEnumerable<RectangleModel> rectangles) : ICommand {
    private readonly List<RectangleModel> _pastedRectangles = rectangles.ToList();

    public void Execute() {
        foreach (var rect in _pastedRectangles.Where(rect => !picture.Rectangles.Contains(rect))) {
            picture.Rectangles.Add(rect);
        }
    }

    public void Undo() {
        foreach (var rect in _pastedRectangles) {
            picture.Rectangles.Remove(rect);
            picture.SelectedRectangleIds.Remove(rect.Id);
        }
        if (_pastedRectangles.Any(r => r.Id == picture.SelectedRectangle?.Id)) {
            picture.SelectedRectangle = null;
        }
    }
}
