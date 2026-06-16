namespace GraphEditor.Business.Commands;

public interface ICommand {
    void Execute();
    void Undo();
}
