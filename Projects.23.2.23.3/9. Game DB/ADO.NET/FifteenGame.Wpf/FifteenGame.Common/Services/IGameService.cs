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

        bool MakeMove(GameModel model, MoveDirection direction);

        void Shuffle(GameModel model);

        bool IsGameOver(GameModel model);

        GameModel GetByUserId(int userId);

        GameModel GetByGameId(int gameId);

        void RemoveGame(int gameId);
    }
}
