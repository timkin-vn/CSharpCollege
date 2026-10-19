using MinesweeperGame.Common.BusinessDtos;

namespace MinesweeperGame.Common.Dtos
{
    public class GameResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GameBoardDto Data { get; set; }
    }
}
