using System.Collections.Generic;

namespace MemoryGame.Web.ViewModels
{
    public class GameVm
    {
        public List<CellVm> Cells { get; set; } = new List<CellVm>();
        public string StatusText { get; set; }

        public bool NeedToHide { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWin { get; set; }
    }

    public class CellVm
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public int Value { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsMatched { get; set; }

        public string Text => (IsRevealed || IsMatched) ? Value.ToString() : "";
    }
}
