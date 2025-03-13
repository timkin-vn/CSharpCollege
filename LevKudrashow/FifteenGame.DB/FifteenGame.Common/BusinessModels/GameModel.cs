using FifteenGame.Common.Definitions;
using System;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int GameId { get; set; }

        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int PlayerRow { get; set; }

        public int PlayerColumn { get; set; }

        public int PlayerCount = 0;

        public DateTime GameStart { get; set; }
    }
}
