using LightsOutGame.Common.Dtos;
using System.Collections.Generic;

namespace LightsOutGame.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        int Save(GameDto game);

        GameDto GetByGameId(int gameId);

        IEnumerable<GameDto> GetByUserId(int userId);

        void Remove(int gameId);
    }
}
