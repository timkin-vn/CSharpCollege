namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int AdjacentMines { get; set; }
        public bool IsRevealed { get; set; }
        public bool HasMine { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsExploded { get; set; }

        public string DisplayText
        {
            get
            {
                if (IsFlagged && !IsRevealed)
                    return "🚩";

                if (!IsRevealed)
                    return "";

                if (HasMine)
                    return IsExploded ? "💥" : "💣";

                return AdjacentMines > 0 ? AdjacentMines.ToString() : "";
            }
        }

        public string CellColor
        {
            get
            {
                if (!IsRevealed)
                    return "lightgray";

                if (HasMine)
                    return IsExploded ? "darkred" : "red";

                return "white";
            }
        }

        public string TextColor
        {
            get
            {
                if (!IsRevealed || HasMine)
                    return "black";

                switch (AdjacentMines)
                {
                    case 1: return "blue";
                    case 2: return "green";
                    case 3: return "red";
                    case 4: return "darkblue";
                    case 5: return "darkred";
                    case 6: return "teal";
                    case 7: return "black";
                    case 8: return "gray";
                    default: return "black";
                }
            }
        }
    }
}