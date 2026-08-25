namespace DrawingApp.Models
{
    /// <summary>
    /// Прямоугольная область в виртуальной системе координат.
    /// </summary>
    public class RectangleModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double Left => X;
        public double Right => X + Width;
        public double Top => Y;
        public double Bottom => Y - Height;
    }
}