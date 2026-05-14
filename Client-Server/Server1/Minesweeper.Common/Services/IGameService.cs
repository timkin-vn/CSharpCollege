using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using System.Collections.Generic;

namespace Minesweeper.Common.Services
{
    public interface IGameService
    {
        GameResponse CreateGame(CreateGameRequest request);
        GameResponse GetGame(int gameId);
        IEnumerable<GameResponse> GetUserGames(int userId);
        void DeleteGame(int gameId);

        GameResponse MakeMove(MakeMoveRequest request);
        GameResponse ToggleFlag(int gameId, int row, int column);

        GameResponse GetLastActiveGame(int userId);
        bool IsGameOver(int gameId);
        bool IsGameWon(int gameId);
    }
}