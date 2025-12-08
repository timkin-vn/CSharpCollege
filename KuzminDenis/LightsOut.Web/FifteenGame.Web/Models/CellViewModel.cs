namespace LightsOut.Web.Models
{
    public class CellViewModel
    {
        public bool IsOn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string Command =>
            $"row={Row}&column={Column}";
    }
}
