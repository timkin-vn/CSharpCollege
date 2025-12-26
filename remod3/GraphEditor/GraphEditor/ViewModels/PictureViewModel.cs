using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GraphEditor.Business.Models;

namespace GraphEditor.ViewModels;

internal class PictureViewModel {
    public IList<RectangleViewModel> Rectangles { get; init; } = new List<RectangleViewModel>();

    public RectangleViewModel? SelectedRectangle { get; init; }

    public IEnumerable<MarkerViewModel> Markers => SelectedRectangle?.Markers ?? Enumerable.Empty<MarkerViewModel>();

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
            SelectedRectangle = selected,
        };
    }
}