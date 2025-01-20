namespace GraphEditor.Business.Models
{
    public class Marker
    {
        public RectangleModel Rectangle { get; set; }

        public bool IsActive { get; set; }

        public PictureMode Mode { get; set; }

        public PictureCursor Cursor { get; set; }
    }
}
