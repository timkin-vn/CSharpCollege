namespace Minesweeper.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Number { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsMine { get; set; }

        public string Text
        {
            get
            {
                if (IsFlagged && !IsRevealed) return "⚑";
                if (!IsRevealed) return string.Empty;
                if (IsMine) return "✹";
                return Number == 0 ? string.Empty : Number.ToString();
            }
        }
    }
}
