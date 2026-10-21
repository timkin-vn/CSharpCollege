using MemoryGame.Business.Models;

namespace MemoryGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }

        public CardState State { get; set; }

        public bool CanClick { get; set; }

        public string ImageName
        {
            get
            {
                switch (State)
                {
                    case CardState.Opened:
                        return $"{Value}.png";
                    case CardState.Matched:
                        return $"{Value}-matched.png";
                    default:
                        return "back.png";
                }
            }
        }
    }
}
