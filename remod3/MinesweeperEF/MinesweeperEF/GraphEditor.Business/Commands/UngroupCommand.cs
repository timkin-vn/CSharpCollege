using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class UngroupCommand(PictureModel picture, GroupModel group) : ICommand {
    public void Execute() => picture.Groups.Remove(group);

    public void Undo() => picture.Groups.Add(group);
}
