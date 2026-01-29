using System.Collections.Generic;

namespace GraphEditor.Business.Models;

public class PictureModel
{
    public IList<RectangleModel> Rectangles { get; set; } = new List<RectangleModel>();

    public RectangleModel SelectedRectangle { get; set; }
}
