using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Definitions;

namespace Game2048.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }
    }
}
