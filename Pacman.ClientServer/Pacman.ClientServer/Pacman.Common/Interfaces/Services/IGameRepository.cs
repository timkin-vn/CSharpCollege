using System.Collections.Generic;
using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Repositories
{
    public interface IGameRepository
    {
        GameStateDto CreateGame(int userId, int mapId);
        GameStateDto GetGameState(int gameId);
        void UpdateGameState(GameStateDto gameState);
        void RemoveGame(int gameId);
        IReadOnlyList<GameDto> GetGamesByUserId(int userId);
    }
}