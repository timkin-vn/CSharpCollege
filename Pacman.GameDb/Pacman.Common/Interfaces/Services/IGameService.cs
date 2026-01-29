using System.Collections.Generic;
using Pacman.Common.Enums;
using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Services
{
    public interface IGameService
    {
        GameStateDto CreateNewGame(int userId);
        GameStateDto GetGameState(int gameId);
        GameStateDto Move(int gameId, Direction direction);
        void RemoveGame(int gameId);
        IReadOnlyList<GameDto> GetUserGames(int userId);
    }
}