using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class MoveRectanglesCommand : ICommand {
    private readonly List<(RectangleModel Rect, int OldLeft, int OldTop)> _moveInfo;
    private readonly int _deltaX;
    private readonly int _deltaY;

    public MoveRectanglesCommand(IEnumerable<RectangleModel> rectangles, int deltaX, int deltaY) {
        _deltaX = deltaX;
        _deltaY = deltaY;
        _moveInfo = rectangles.Select(r => (r, r.Left, r.Top)).ToList();
    }

    public void Execute() {
        foreach (var (rect, _, _) in _moveInfo) {
            rect.Left += _deltaX;
            rect.Top += _deltaY;
        }
    }

    public void Undo() {
        foreach (var (rect, oldLeft, oldTop) in _moveInfo) {
            rect.Left = oldLeft;
            rect.Top = oldTop;
        }
    }
}
