using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class MoveForwardCommand(IList<RectangleModel> rectangles, int index) : ICommand {
    public void Execute() => Swap();
    public void Undo() => Swap();

    private void Swap() =>
        (rectangles[index + 1], rectangles[index]) = (rectangles[index], rectangles[index + 1]);
}
