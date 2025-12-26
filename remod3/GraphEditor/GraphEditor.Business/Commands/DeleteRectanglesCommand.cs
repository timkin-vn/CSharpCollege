using GraphEditor.Business.Models;

namespace GraphEditor.Business.Commands;

public class DeleteRectanglesCommand : ICommand {
    private readonly PictureModel _picture;
    private readonly List<RectangleModel> _deletedRectangles;
    private readonly List<(int Index, RectangleModel Rect)> _positionInfo;
    private readonly List<(GroupModel Group, List<Guid> RemovedIds)> _groupInfo;

    public DeleteRectanglesCommand(PictureModel picture, IEnumerable<RectangleModel> rectangles) {
        _picture = picture;
        _deletedRectangles = rectangles.ToList();
        _positionInfo = [];
        _groupInfo = [];

        foreach (var rect in _deletedRectangles) {
            var index = _picture.Rectangles.IndexOf(rect);
            _positionInfo.Add((index, rect));
        }

        foreach (var group in _picture.Groups) {
            var removedIds = group.RectangleIds
                .Where(id => _deletedRectangles.Any(r => r.Id == id))
                .ToList();
            if (removedIds.Count != 0) {
                _groupInfo.Add((group, removedIds));
            }
        }
    }

    public void Execute() {
        foreach (var rect in _deletedRectangles) {
            _picture.Rectangles.Remove(rect);
            _picture.SelectedRectangleIds.Remove(rect.Id);

            foreach (var group in _picture.Groups.ToList()) {
                group.Remove(rect.Id);
                if (group.IsEmpty) {
                    _picture.Groups.Remove(group);
                }
            }
        }

        if (_deletedRectangles.Any(r => r.Id == _picture.SelectedRectangle?.Id)) {
            _picture.SelectedRectangle = null;
        }
    }

    public void Undo() {
        foreach (var (index, rect) in _positionInfo.OrderBy(p => p.Index)) {
            if (index >= 0 && index <= _picture.Rectangles.Count) {
                _picture.Rectangles.Insert(index, rect);
            } else {
                _picture.Rectangles.Add(rect);
            }
        }

        foreach (var (group, removedIds) in _groupInfo) {
            if (!_picture.Groups.Contains(group)) {
                _picture.Groups.Add(group);
            }

            foreach (var id in removedIds.Where(id => !group.Contains(id))) {
                group.Add(id);
            }
        }
    }
}
