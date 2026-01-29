using Игра.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра.Common.Services
{
    public interface IGameService
    {
        GameModel MakeMove(int gameId, int row, int column);
        bool IsGameOver(int gameId);
        GameModel GetByUserId(int userId);
        GameModel GetByGameId(int gameId);
        void RemoveGame(int gameId);
    }
}