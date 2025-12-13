namespace GraphEditor.Business.Models;

public class GroupModel {
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; init; }

    public List<Guid> RectangleIds { get; } = [];

    public bool Contains(Guid rectId) => RectangleIds.Contains(rectId);

    public void Add(Guid rectId) {
        if (!RectangleIds.Contains(rectId)) RectangleIds.Add(rectId);
    }

    public void Remove(Guid rectId) => RectangleIds.Remove(rectId);

    public bool IsEmpty => RectangleIds.Count == 0;
}

public static class GroupModelExtensions {
    public static IEnumerable<RectangleModel> Resolve(this GroupModel group, PictureModel picture) =>
        picture.Rectangles.Where(r => group.RectangleIds.Contains(r.Id));
}