using Blackjack.Common.Models;
using System.Collections.Generic;

namespace Blackjack.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        GameModel GetById(int id);

        GameModel GetUnfinished(int userId);

        List<GameModel> GetFinished(int userId);

        // Добавляет новую игру (Id == 0) или обновляет существующую. Возвращает Id.
        int Save(GameModel game);
    }
}
