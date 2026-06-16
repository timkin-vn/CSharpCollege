using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class CreateRectangleCommand(PictureModel picture, RectangleModel rectangle) : ICommand {
    public void Execute() {
        if (!picture.Rectangles.Contains(rectangle)) {
            picture.Rectangles.Add(rectangle);
        }
    }

    public void Undo() {
        picture.Rectangles.Remove(rectangle);
        picture.SelectedRectangleIds.Remove(rectangle.Id);
        if (picture.SelectedRectangle?.Id == rectangle.Id) {
            picture.SelectedRectangle = null;
        }
    }
}
