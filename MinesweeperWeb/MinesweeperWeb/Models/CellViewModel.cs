namespace MinesweeperWeb.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMinesCount { get; set; }

        public string DisplayText => IsRevealed
            ? (IsMine ? "💣" : (AdjacentMinesCount > 0 ? AdjacentMinesCount.ToString() : ""))
            : (IsFlagged ? "🚩" : "");

        public string CssClass => GetCssClass();

        private string GetCssClass()
        {
            if (!IsRevealed) return "cell closed";
            if (IsMine) return "cell mine";
            if (AdjacentMinesCount == 0) return "cell empty";
            return $"cell number-{AdjacentMinesCount}";
        }
    }
}