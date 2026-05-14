using Minesweeper.Common.Dto;
using System;
using System.Collections.Generic;

namespace Minesweeper.Common.Repositories
{
    public interface IGameRepository
    {
        GameStateDto GetById(int gameId);
        IEnumerable<GameStateDto> GetByUserId(int userId);
        GameStateDto Create(GameStateDto game);
        GameStateDto Update(GameStateDto game);
        void Delete(int gameId);

        GameStateDto GetLastActiveGame(int userId);
        IEnumerable<GameStateDto> GetCompletedGames(int userId, int limit = 10);
        int GetActiveGamesCount(int userId);

        void UpdateGameStats(int gameId, TimeSpan playTime, bool isGameOver, bool isGameWon);
    }
}