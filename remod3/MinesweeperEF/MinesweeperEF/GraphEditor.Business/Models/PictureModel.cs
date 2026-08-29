namespace GraphEditor.Business.Models;

public class PictureModel {
    public IList<RectangleModel> Rectangles { get; init; } = new List<RectangleModel>();

    public IList<GroupModel> Groups { get; init; } = new List<GroupModel>();

    public RectangleModel? SelectedRectangle { get; set; }
    
    public ISet<Guid> SelectedRectangleIds { get; } = new HashSet<Guid>();
}
