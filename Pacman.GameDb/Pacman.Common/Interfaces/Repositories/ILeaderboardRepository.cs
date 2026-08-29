using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Repositories
{
    public interface ILeaderboardRepository
    {
        IReadOnlyList<GameDto> GetTopScores(int count);
    }
}