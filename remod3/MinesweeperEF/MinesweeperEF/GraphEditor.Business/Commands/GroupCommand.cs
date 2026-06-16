using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class GroupCommand : ICommand {
    private readonly PictureModel _picture;
    private readonly GroupModel _newGroup;
    private readonly List<(GroupModel Group, List<Guid> RemovedIds)> _affected;

    public GroupCommand(PictureModel picture, IEnumerable<Guid> selectedIds, string? name) {
        _picture = picture;
        var ids = selectedIds.ToList();

        _newGroup = new GroupModel { Name = name };
        foreach (var id in ids) _newGroup.Add(id);

        _affected = _picture.Groups
            .Where(g => g.RectangleIds.Any(ids.Contains))
            .Select(g => (g, ids.Where(id => g.RectangleIds.Contains(id)).ToList()))
            .ToList();
    }

    public void Execute() {
        foreach (var (group, removedIds) in _affected) {
            foreach (var id in removedIds) group.Remove(id);
            if (group.IsEmpty) _picture.Groups.Remove(group);
        }
        _picture.Groups.Add(_newGroup);
    }

    public void Undo() {
        _picture.Groups.Remove(_newGroup);
        foreach (var (group, removedIds) in _affected) {
            if (!_picture.Groups.Contains(group)) _picture.Groups.Add(group);
            foreach (var id in removedIds) group.Add(id);
        }
    }
}
