using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class PictureViewModel {
    public IList<RectangleViewModel>? Rectangles { get; init; }

    public RectangleViewModel? SelectedRectangle { get; set; }

    public IEnumerable<MarkerViewModel> Markers => SelectedRectangle?.Markers ?? [];
    
    public GraphEditor.Business.Models.PictureModel? Model { get; init; }
    
    public GraphEditor.Business.Models.PictureModel ToModel() {
        return new GraphEditor.Business.Models.PictureModel {
            Rectangles = Rectangles?
                .Select(r => r.ToModel())
                .ToList() ?? new List<GraphEditor.Business.Models.RectangleModel>(),
            SelectedRectangle = SelectedRectangle?.ToModel()
        };
    }
    
    public PictureModel ToModel() {
        return new PictureModel {
            Rectangles = Rectangles?
                .Select(r => r.ToModel())
                .ToList() ?? new List<RectangleModel>(),
            SelectedRectangle = SelectedRectangle?.ToModel()
        };
    }

    public static PictureViewModel FromModel(PictureModel model)
    {
        return new PictureViewModel
        {
            Rectangles = model.Rectangles
                .Select(RectangleViewModel.FromModel)
                .ToList(),
            SelectedRectangle = model.SelectedRectangle != null
                ? RectangleViewModel.FromModel(model.SelectedRectangle)
                : null
        };
    }
}