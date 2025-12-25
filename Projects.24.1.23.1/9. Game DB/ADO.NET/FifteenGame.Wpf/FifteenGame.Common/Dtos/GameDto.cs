using FifteenGame.Business.Models;
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

        public int MatchesCount { get; set; }
        public bool IsFinished { get; set; }

        public int[,] Cells { get; } = new int[Business.Models.GameModel.RowCount, Business.Models.GameModel.ColumnCount];
    }
}
