using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class ChangePropertyCommand<T> : ICommand {
    private readonly List<(RectangleModel Rect, T OldValue)> _changes;
    private readonly T _newValue;
    private readonly Action<RectangleModel, T> _setter;

    public string Description { get; }

    public ChangePropertyCommand(
        IEnumerable<RectangleModel> rectangles,
        T newValue,
        Func<RectangleModel, T> getter,
        Action<RectangleModel, T> setter,
        string description) {
        _newValue = newValue;
        var getter1 = getter;
        _setter = setter;
        Description = description;
        _changes = rectangles.Select(r => (r, getter1(r))).ToList();
    }

    public void Execute() {
        foreach (var (rect, _) in _changes) {
            _setter(rect, _newValue);
        }
    }

    public void Undo() {
        foreach (var (rect, oldValue) in _changes) {
            _setter(rect, oldValue);
        }
    }
}
