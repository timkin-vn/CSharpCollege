using System.Collections.Generic;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public IList<FigureModel> Figures { get; set; } = new List<FigureModel>();
        public FigureModel SelectedFigure { get; set; }
    }
}