using FifteenGame.Common.Dtos;
using System.Collections.Generic;

namespace FifteenGame.Common.Repositories
{
    public interface IGameRepository
    {
        int Save(GameDto gameDto);
        GameDto GetByGameId(int gameId);
        IEnumerable<GameDto> GetByUserId(int userId);
        IEnumerable<GameDto> GetFinishedGamesByUserId(int userId);
        void Remove(int gameId);
    }
}