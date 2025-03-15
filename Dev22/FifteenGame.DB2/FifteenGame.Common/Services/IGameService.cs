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

        GameModel RevealCell(int gameId, int x, int y);

        bool IsGameOver(int gameId);

        void RemoveGame(int gameId);
    }
}
