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

        bool? IsGameOver(int gameId);

        GameModel MakeMove(int gameId, MoveDirection direction);

        void RemoveGame(int gameId);
    }
}
