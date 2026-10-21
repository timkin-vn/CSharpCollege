namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }
        public string Color { get; set; }

        public bool IsOpened { get; set; }

        public bool IsFound { get; set; }

        public string BackgroundColor => IsOpened || IsFound ? Color : "LightGray";

        public string Text => "";

        public int Row { get; set; }

        public int Column { get; set; }

        public int Index { get; set; }
    }
}