using FifteenGame.Common.BusinessModels;
using System.Collections.Generic;

namespace FifteenGame.Common.Services
{
    public interface IGameService
    {
        GameModel StartNewGame(int userId, int minesCount);
        GameModel RevealCell(int gameId, int row, int column);
        GameModel ToggleFlag(int gameId, int row, int column);
        GameModel GetCurrentGame(int userId);
        GameModel GetByGameId(int gameId);
        IEnumerable<GameModel> GetFinishedGames(int userId);
        void RemoveGame(int gameId);
        void SaveGameState(GameModel game);
    }
}