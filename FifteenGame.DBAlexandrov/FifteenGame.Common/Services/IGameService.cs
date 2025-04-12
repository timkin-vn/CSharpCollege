using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Services
{
    public interface IGameService
    {
        GameModel GetByUserId(int userId);

        GameModel GetByGameId(int gameId);

        GameModel MakeMove(int gameId, int X, int Y, Units[,]model, MoveDirection direction);

        bool IsGameOver(int gameId);

        void RemoveGame(int gameId);
    }
}
