using FifteenGame.Common.Definitions;
using System;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private string[,] _cells = new string[Constants.RowCount, Constants.ColumnCount];

        public int GameId { get; set; }

        public int UserId { get; set; }

        public string this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

       

        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }
    }
}
