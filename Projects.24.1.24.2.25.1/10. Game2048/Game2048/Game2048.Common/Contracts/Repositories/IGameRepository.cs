using Game2048.Common.BusinessModels;

namespace Game2048.Common.Contracts.Repositories;

public interface IGameRepository
{
    GameModel? GetByGameId(int gameId);
    GameModel? GetByUserId(int userId);
    GameModel Create(int userId);
    GameModel Update(GameModel game);
    void Delete(int gameId);
}
