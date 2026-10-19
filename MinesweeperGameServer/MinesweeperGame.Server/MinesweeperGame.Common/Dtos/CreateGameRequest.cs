using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.Common.Dtos
{
    public class CreateGameRequest
    {
        public GameDifficulty Difficulty { get; set; } = GameDifficulty.Beginner;
    }
}
