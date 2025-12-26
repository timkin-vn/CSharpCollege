using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class PictureViewModel {
    public IList<RectangleViewModel> Rectangles { get; private init; } = new List<RectangleViewModel>();

    public RectangleViewModel? SelectedRectangle { get; private init; }

    public IEnumerable<MarkerViewModel> Markers => SelectedRectangle?.Markers ?? [];

    public static PictureViewModel FromModel(PictureModel model) {
        var rects = model.Rectangles
            .Select(r => RectangleViewModel.FromBusiness(r, model.SelectedRectangleIds.Contains(r.Id)))
            .ToList();

        RectangleViewModel? selected = null;
        if (model.SelectedRectangle != null) {
            selected = rects.FirstOrDefault(r => r.Id == model.SelectedRectangle.Id);
        }

        return new PictureViewModel {
            Rectangles = rects,
            SelectedRectangle = selected
        };
    }
}