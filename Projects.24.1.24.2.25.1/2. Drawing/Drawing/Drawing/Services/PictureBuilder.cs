using Drawing.Models;

namespace Drawing.Services
{
    public class PictureBuilder
    {
        public RectangleModel HouseBody { get; private set; }
        public RectangleModel Roof { get; private set; }
        public RectangleModel Window { get; private set; }

        public void Build(SizeModel canvasSize, Scaler scaler)
        {
           
            HouseBody = new RectangleModel
            {
                Left = (int)(canvasSize.Width * 0.3),
                Top = (int)(canvasSize.Height * 0.45),
                Width = (int)(canvasSize.Width * 0.4),
                Height = (int)(canvasSize.Height * 0.4)
            };

            
            Roof = new RectangleModel
            {
                Left = (int)(canvasSize.Width * 0.25),
                Top = (int)(canvasSize.Height * 0.2),
                Width = (int)(canvasSize.Width * 0.5),
                Height = (int)(canvasSize.Height * 0.25)
            };

            
            Window = new RectangleModel
            {
                Left = (int)(canvasSize.Width * 0.45),
                Top = (int)(canvasSize.Height * 0.55),
                Width = (int)(canvasSize.Width * 0.1),
                Height = (int)(canvasSize.Height * 0.1)
            };
        }
    }
}