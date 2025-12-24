using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class ChangePropertyCommand<T> : ICommand {
    private readonly List<(RectangleModel Rect, T OldValue)> _changes;
    private readonly T _newValue;
    private readonly Action<RectangleModel, T> _setter;
    private readonly Func<RectangleModel, T> _getter;
    private readonly string _description;

    public string Description => _description;

    public ChangePropertyCommand(
        IEnumerable<RectangleModel> rectangles,
        T newValue,
        Func<RectangleModel, T> getter,
        Action<RectangleModel, T> setter,
        string description) {
        _newValue = newValue;
        _getter = getter;
        _setter = setter;
        _description = description;
        _changes = rectangles.Select(r => (r, _getter(r))).ToList();
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
