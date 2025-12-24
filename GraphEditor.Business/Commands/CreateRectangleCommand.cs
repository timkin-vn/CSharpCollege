using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class CreateRectangleCommand : ICommand {
    private readonly PictureModel _picture;
    private readonly RectangleModel _rectangle;

    public string Description => "Create rectangle";

    public CreateRectangleCommand(PictureModel picture, RectangleModel rectangle) {
        _picture = picture;
        _rectangle = rectangle;
    }

    public void Execute() {
        if (!_picture.Rectangles.Contains(_rectangle)) {
            _picture.Rectangles.Add(_rectangle);
        }
    }

    public void Undo() {
        _picture.Rectangles.Remove(_rectangle);
        _picture.SelectedRectangleIds.Remove(_rectangle.Id);
        if (_picture.SelectedRectangle?.Id == _rectangle.Id) {
            _picture.SelectedRectangle = null;
        }
    }
}
