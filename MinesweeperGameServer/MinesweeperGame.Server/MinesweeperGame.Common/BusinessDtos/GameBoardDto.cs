using System.Collections.Generic;

namespace MinesweeperGame.Common.BusinessDtos
{
    public class GameBoardDto
    {
        public GameDto Game { get; set; }
        public List<CellDto> Cells { get; set; } = new List<CellDto>();
    }
}
