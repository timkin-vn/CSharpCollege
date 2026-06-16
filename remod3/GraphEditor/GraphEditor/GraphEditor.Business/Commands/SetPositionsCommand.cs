using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class SetPositionsCommand : ICommand {
    private readonly List<(RectangleModel Rect, int OldLeft, int OldTop, int NewLeft, int NewTop)> _moves;

    public SetPositionsCommand(IEnumerable<(RectangleModel Rect, int NewLeft, int NewTop)> positions) {
        _moves = positions.Select(p => (p.Rect, p.Rect.Left, p.Rect.Top, p.NewLeft, p.NewTop)).ToList();
    }

    public void Execute() {
        foreach (var (rect, _, _, newLeft, newTop) in _moves) {
            rect.Left = newLeft;
            rect.Top = newTop;
        }
    }

    public void Undo() {
        foreach (var (rect, oldLeft, oldTop, _, _) in _moves) {
            rect.Left = oldLeft;
            rect.Top = oldTop;
        }
    }
}
