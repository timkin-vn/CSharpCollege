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
        void Initialize(GameModel model);

        bool Swap(GameModel model, int r1, int c1, int r2, int c2);

        List<(int row, int col)> CheckMatches(GameModel model);

        void RemoveMatches(GameModel model, List<(int row, int col)> matches);

        void ProcessMatches(GameModel model);

        void AddMatches(GameModel model, int count);

        GameModel GetByUserId(int userId);

        GameModel GetByGameId(int gameId);

        void RemoveGame(int gameId);
    }
}