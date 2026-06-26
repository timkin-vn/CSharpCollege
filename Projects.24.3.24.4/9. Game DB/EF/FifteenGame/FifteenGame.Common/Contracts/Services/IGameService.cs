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
        void Initialize(GameModel model);

        void Move(GameModel model, int row, int column);

        bool IsGameOver(int gameId);

        bool IsGameOver(GameModel model);

        GameModel MakeMove(int gameId, MoveDirection direction);
        bool MakeMove(GameModel model, MoveDirection direction);
        void RemoveGame(int gameId);
        void Shuffle(GameModel model);
    }
}
