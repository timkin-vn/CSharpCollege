using Game2048.Common.BusinessModels;
using Game2048.Common.Definitions;

namespace Game2048.Common.Contracts.Services;

public interface IGameService
{
    GameModel GetByGameId(int gameId);
    GameModel GetByUserId(int userId);
    bool? IsGameOver(int gameId);
    bool? IsGameWon(int gameId);
    GameModel MakeMove(int gameId, MoveDirection direction);
    void RemoveGame(int gameId);
}
