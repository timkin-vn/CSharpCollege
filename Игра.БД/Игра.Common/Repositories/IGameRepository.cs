using Игра.Common.Dtos;
using System.Collections.Generic;

namespace Игра.Common.Repositories
{
    public interface IGameRepository
    {
        int Save(GameDto gameDto);

        GameDto GetByGameId(int gameId);

        IEnumerable<GameDto> GetByUserId(int userId);

        void Remove(int gameId);
    }
}