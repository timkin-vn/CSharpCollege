using System.Collections.Generic;

namespace NonogramWeb.Models
{
    public class MoveResultModel
    {
        public bool Success { get; set; }
        public string CellState { get; set; }
        public int MistakesCount { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public List<int> CompletedRows { get; set; }
        public List<int> CompletedColumns { get; set; }
        public string ErrorMessage { get; set; }

        public MoveResultModel()
        {
            Success = false;
            CellState = "Empty";
            MistakesCount = 0;
            IsGameOver = false;
            IsGameWon = false;
            CompletedRows = new List<int>();
            CompletedColumns = new List<int>();
            ErrorMessage = string.Empty;
        }
    }
}