namespace GraphEditor.Business.Models;

public class PictureModel {
    public IList<RectangleModel> Rectangles { get; init; } = new List<RectangleModel>();

    public IList<GroupModel> Groups { get; set; } = new List<GroupModel>();

    public RectangleModel? SelectedRectangle { get; set; }
}