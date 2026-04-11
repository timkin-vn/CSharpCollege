namespace Drawing.Services
{
    public class Scaler
    {
        public int ScaleHorizontal(int value, int width)
        {
            return (int)(value * width / 1000.0);
        }

        public int ScaleVertical(int value, int height)
        {
            return (int)(value * height / 1000.0);
        }
    }
}