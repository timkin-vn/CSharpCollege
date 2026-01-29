namespace MemoryGame.Wpf.ViewModels
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public int Value { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsMatched { get; set; }

        public string Text
        {
            get { return (IsRevealed || IsMatched) ? Value.ToString() : string.Empty; }
        }
    }
}
