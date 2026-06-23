using System;

namespace GraphEditor.Business.Models
{
    [Serializable]
    public class RectangleModel : ShapeModel
    {
        public override bool Contains(int px, int py)
        {
            if (Width <= 0 || Height <= 0)
                return false;

            return px >= X && px <= X + Width && py >= Y && py <= Y + Height;
        }

        public override ShapeModel Clone()
        {
            return new RectangleModel
            {
                Id = Id, X = X, Y = Y, Width = Width, Height = Height, FillArgb = FillArgb
            };
        }
    }
}
