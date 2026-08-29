using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public IList<FigureModel> Figures { get; set; } = new List<FigureModel>();
        public FigureModel SelectedFigure { get; set; }
        public FigureType CurrentFigureType { get; set; } = FigureType.Rectangle;
    }
}
