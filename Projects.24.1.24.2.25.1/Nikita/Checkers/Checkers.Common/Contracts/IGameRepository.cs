using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;

namespace Checkers.Common.Contracts
{
    public interface IGameRepository
    {
        void SaveGame(Game game);
        Game LoadGame(int gameId);
    }
}
