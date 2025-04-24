using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Dtos;

namespace Game2048.Common.Repositories
{
    public interface IGameRepository
    {
        int Save(GameDto gameDto);

        GameDto GetByGameId(int gameId);

        IEnumerable<GameDto> GetByUserId(int userId);

        void Remove(int gameId);
    }
}
