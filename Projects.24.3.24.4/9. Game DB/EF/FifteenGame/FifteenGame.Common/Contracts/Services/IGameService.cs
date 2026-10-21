using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        GameModel StartNewGame(int userId, int difficulty, int theme);

        bool IsGameOver(int gameId);

        GameModel MakeMove(int gameId, MoveDirection direction);

        GameModel OpenCard(int gameId, int row, int column, int elapsedSeconds = 0);

        GameModel UseHint(int gameId, int elapsedSeconds = 0);

        void RemoveGame(int gameId);
    }
}
