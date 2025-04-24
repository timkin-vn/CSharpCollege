using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.BusinessModels;

namespace Game2048.Common.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        GameModel MakeMove(int gameId, MoveDirection direction);

        bool IsGameOver(int gameId);

        void RemoveGame(int gameId);
    }
}
