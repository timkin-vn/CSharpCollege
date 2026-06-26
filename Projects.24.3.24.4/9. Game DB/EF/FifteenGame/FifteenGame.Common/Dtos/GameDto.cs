using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int Money { get; set; }

        public int MoveCount { get; set; }

        // Заглушка для старого интерфейса, чтобы не было ошибок сборки
        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        public int[,] PeopleCount { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        public bool[,] HasShop { get; } = new bool[Constants.RowCount, Constants.ColumnCount];

        public bool[,] IsVeggie { get; } = new bool[Constants.RowCount, Constants.ColumnCount];

        public bool[,] IsRevealed { get; } = new bool[Constants.RowCount, Constants.ColumnCount];
    }
}
