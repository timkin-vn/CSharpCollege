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
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        GameModel CheckMatch(int gameId, int[] OneColumnRow, int[] TwoColumnRow);

        bool IsGameOver(int gameId);

        void RemoveGame(int gameId);
    }
}
