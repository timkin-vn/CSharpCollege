using System;
using System.Collections.Generic;

namespace EllipseEditor.Business.Models
{

    [Serializable]
    public class PictureModel
    {
        public List<ShapeModel> Shapes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public PictureModel()
        {
            Shapes = new List<ShapeModel>();
            Width = 800;
            Height = 600;
        }
    }
}
