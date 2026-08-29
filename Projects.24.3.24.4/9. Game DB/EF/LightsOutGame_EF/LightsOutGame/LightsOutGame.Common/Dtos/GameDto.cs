using LightsOutGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool[,] Cells { get; } = new bool[Constants.RowCount, Constants.ColumnCount];

        public int MoveCount { get; set; }
    }
}
