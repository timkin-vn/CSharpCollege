using System.Collections.Generic;
using SeaBattle.Common.Models;

namespace SeaBattle.Common.Interfaces
{
    public interface IGameService
    {
        GameModel SaveResult(int userId, int moveCount, bool won);
        IList<GameModel> GetHistory(int userId);
    }
}
