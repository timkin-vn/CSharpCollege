using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        GameDto GetByGameId(int gameId);

        IEnumerable<GameDto> GetByUserId(int userId);

        void Remove(int gameId);

        int Save(GameDto gameDto);
        int GetWinStreak(int userId);
        void UpdateWinStreak(int userId, int streak);
    }
}
