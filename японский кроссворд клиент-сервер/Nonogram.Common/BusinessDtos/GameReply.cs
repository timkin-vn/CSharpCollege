using System.Collections.Generic;

namespace Nonogram.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MistakesCount { get; set; }
        public int[] Cells { get; set; }
        public List<List<int>> RowClues { get; set; }
        public List<List<int>> ColumnClues { get; set; }
    }
}