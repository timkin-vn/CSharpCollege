using LightsOutGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Common.Contracts.Repositories
{
    public interface IGameRepository
    {
        GameDto GetByGameId(int gameId);

        IEnumerable<GameDto> GetByUserId(int userId);

        void Remove(int gameId);

        int Save(GameDto gameDto);
    }
}
