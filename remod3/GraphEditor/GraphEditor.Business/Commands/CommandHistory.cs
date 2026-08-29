namespace GraphEditor.Business.Commands;

public class CommandHistory {
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    public bool CanUndo => _undoStack.Count > 0;
    public bool CanRedo => _redoStack.Count > 0;

    public void Execute(ICommand command) {
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear();
    }

    public void Undo() {
        if (!CanUndo) return;

        var command = _undoStack.Pop();
        command.Undo();
        _redoStack.Push(command);
    }

    public void Redo() {
        if (!CanRedo) return;

        var command = _redoStack.Pop();
        command.Execute();
        _undoStack.Push(command);
    }

    public void Clear() {
        _undoStack.Clear();
        _redoStack.Clear();
    }
}
