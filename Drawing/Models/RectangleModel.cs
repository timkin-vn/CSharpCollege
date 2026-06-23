namespace Drawing.Models
{
    internal class RectangleModel
    {
        public PointModel Location { get; set; } = new PointModel();

        public SizeModel Size { get; set; } = new SizeModel();

        public double X
        {
            get => Location.X;
            set => Location.X = value;
        }

        public double Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }

        public double Width
        {
            get => Size.Width;
            set => Size.Width = value;
        }

        public double Height
        {
            get => Size.Height;
            set => Size.Height = value;
        }

        public double Left => X;

        public double Right => X + Width;

        public double Top => Y + Height;   // Изменено: Топ теперь вычисляется как Y + Height

        public double Bottom => Y;          // Изменено: Bottom теперь просто Y
    }
}
