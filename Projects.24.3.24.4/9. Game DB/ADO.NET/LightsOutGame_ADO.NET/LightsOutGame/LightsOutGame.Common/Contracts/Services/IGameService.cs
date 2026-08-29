using LightsOutGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        bool IsGameOver(int gameId);

        GameModel PressCell(int gameId, int row, int column);

        void RemoveGame(int gameId);
    }
}
