using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphEditor.Business.Models
{
    public class PictureModel
    {
        public IList<ShapeModel> Shapes { get; set; } = new List<ShapeModel>();
        public ShapeModel SelectedShape { get; set; }
    }
}