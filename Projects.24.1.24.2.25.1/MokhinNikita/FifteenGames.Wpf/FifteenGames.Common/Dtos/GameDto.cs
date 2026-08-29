using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FifteenGames.Common.Definitions.Constants;
namespace FifteenGames.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int[,] Cells { get; set; } = new int[RowCount, ColumnCount];
    }
}
