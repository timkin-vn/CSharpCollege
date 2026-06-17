using FifteenGame.Common.Dtos;
using System.Collections.Generic;

namespace FifteenGame.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        GameDto GetByGameId(int gameId);
        IEnumerable<GameDto> GetByUserId(int userId);
        void Remove(int gameId);
        int Save(GameDto gameDto);
    }
}