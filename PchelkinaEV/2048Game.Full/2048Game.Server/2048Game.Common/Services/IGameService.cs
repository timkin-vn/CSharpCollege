using _2048Game.Common.BusinessModels;
using _2048Game.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.Services
{
    public interface IGameService
    {
        GameModel Board { get; set; }
        event Action BoardUpdated;
        event Action GameOver;
        GameModel GetByUserId(int userId);
        GameModel GetByGameId(int gameId);
        void RemoveGame(int gameId);
        bool CheckGameOver(int gameId);
        void Restart(int userId);
        void StartGame(int userId);
        int Save();
        bool Move(MoveDirections.MoveDirection direction);
    }
}
