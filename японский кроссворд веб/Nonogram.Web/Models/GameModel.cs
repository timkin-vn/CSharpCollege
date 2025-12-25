using System.Collections.Generic;
using System.Linq;

namespace NonogramWeb.Models
{
    // Перечисление состояний клетки (внутреннее для веб-приложения)
    public enum GameCellState
    {
        Empty = 0,
        Filled = 1,
        Crossed = -1
    }

    // Основная модель для представления игры
    public class GameModel
    {
        public List<CellModel> Cells { get; set; }
        public List<RowClueModel> RowClues { get; set; }
        public List<ColumnClueModel> ColumnClues { get; set; }
        public int MistakesCount { get; set; }
        public int Size { get; set; }

        // Методы для удобства
        public string GetCellCssClass(int row, int col)
        {
            var cell = Cells?.FirstOrDefault(c => c.Row == row && c.Column == col);
            if (cell == null) return "empty";

            if (cell.State == GameCellState.Filled) return "filled";
            if (cell.State == GameCellState.Crossed) return "crossed";
            return "empty";
        }

        public bool IsCellCrossed(int row, int col)
        {
            var cell = Cells?.FirstOrDefault(c => c.Row == row && c.Column == col);
            return cell != null && cell.State == GameCellState.Crossed;
        }

        public string GetRowClueText(int row)
        {
            var clue = RowClues?.FirstOrDefault(r => r.Index == row);
            if (clue == null || clue.Values == null) return "";
            return string.Join(" ", clue.Values);
        }

        public string GetRowClueClass(int row)
        {
            var clue = RowClues?.FirstOrDefault(r => r.Index == row);
            return clue != null && clue.IsCompleted ? "row-clue completed" : "row-clue";
        }

        public string GetColumnClueText(int col)
        {
            if (col < 0 || col >= ColumnClues.Count)
                return "";

            var clue = ColumnClues[col];
            if (clue == null || clue.Values == null || !clue.Values.Any())
                return "";

            return string.Join(" ", clue.Values);
        }

        public string GetColumnClueClass(int col)
        {
            var clue = ColumnClues?.FirstOrDefault(c => c.Index == col);
            return clue != null && clue.IsCompleted ? "column-clue completed" : "column-clue";
        }
    }

    // Модель клетки
    public class CellModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public GameCellState State { get; set; }
    }

    // Модель подсказки строки
    public class RowClueModel
    {
        public int Index { get; set; }
        public List<int> Values { get; set; }
        public bool IsCompleted { get; set; }
    }

    // Модель подсказки столбца
    public class ColumnClueModel
    {
        public int Index { get; set; }
        public List<int> Values { get; set; }
        public bool IsCompleted { get; set; }
    }

    // Модель результата хода
    public class MoveResult
    {
        public bool Success { get; set; }
        public string CellState { get; set; }
        public int MistakesCount { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public List<int> CompletedRows { get; set; }
        public List<int> CompletedColumns { get; set; }
    }
}