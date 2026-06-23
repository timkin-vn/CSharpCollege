using System.Collections.Generic;
using SeaBattle.Common.Models;

namespace SeaBattle.Common.Interfaces
{
    public interface IGameRepository
    {
        GameModel Save(GameModel game);
        IList<GameModel> GetByUserId(int userId);
        void Remove(int gameId);
    }
}
