namespace DrawingApp.Models
{
    /// <summary>
    /// Точка в виртуальной (логической) системе координат.
    /// </summary>
    public class PointModel
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointModel() { }

        public PointModel(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}