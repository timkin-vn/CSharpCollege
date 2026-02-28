using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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