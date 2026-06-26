using System.Collections.Generic;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        GameDto GetByGameId(int gameId);
        IEnumerable<GameDto> GetByUserId(int userId);
        void Remove(int gameId);
        int Save(GameDto gameDto);
    }
}
