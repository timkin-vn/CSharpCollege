using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Money { get; set; }

        public int MoveCount { get; set; }

        // Наши двумерные массивы для полной синхронизации карты
        public int[,] PeopleCount { get; set; } = new int[5, 5]; // Размерность бери из Constants.RowCount/ColumnCount, если они фиксированы, либо просто инициализируй так
        public bool[,] HasShop { get; set; } = new bool[5, 5];
        public bool[,] IsVeggie { get; set; } = new bool[5, 5];
        public bool[,] IsRevealed { get; set; } = new bool[5, 5];
    }
}