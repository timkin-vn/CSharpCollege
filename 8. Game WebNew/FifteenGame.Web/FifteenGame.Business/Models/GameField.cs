using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int RowCount = 8;
        public const int ColumnCount = 8;
        public const int MinMatchLength = 3;
        public const int GemTypes = 6;
        public const int EmptyCell = -1;

        private int[,] _gems = new int[RowCount, ColumnCount];
        private bool[,] _matched = new bool[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _gems[row, column];
            internal set => _gems[row, column] = value;
        }

        public bool[,] Matched => _matched;
        public int Score { get; set; }
        public int MovesLeft { get; set; } = 20;
        public GameState GameState { get; set; }
        public int SelectedRow { get; set; } = -1;
        public int SelectedColumn { get; set; } = -1;
    }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
}