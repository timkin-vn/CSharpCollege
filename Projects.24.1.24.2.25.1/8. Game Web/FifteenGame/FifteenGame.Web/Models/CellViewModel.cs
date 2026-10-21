
namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Value { get; set; }

        public string Text
        {
            get { return Value == 0 ? string.Empty : Value.ToString(); }
        }

        public bool IsEmpty { get; set; }

        public string CssClass
        {
            get
            {
                if (Value == 0)
                {
                    return "tile tile-empty";
                }

                return "tile tile-" + Value;
            }
        }
    }
}
