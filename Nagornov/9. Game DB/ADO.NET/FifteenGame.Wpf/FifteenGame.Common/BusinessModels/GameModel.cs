using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGame.Common.Definitions;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];
        private bool[,] _revealed = new bool[Constants.RowCount, Constants.ColumnCount];
        private bool[,] _mines = new bool[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int MinesCount { get; set; }
        public int FlagsCount { get; set; }
        public GameState GameState { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public bool IsRevealed(int row, int column) => _revealed[row, column];
        public void SetRevealed(int row, int column, bool value) => _revealed[row, column] = value;

        public bool HasMine(int row, int column) => _mines[row, column];
        public void SetMine(int row, int column, bool value) => _mines[row, column] = value;
    }

    public enum GameState
    {
        Playing,
        Won,
        GameOver
    }
}