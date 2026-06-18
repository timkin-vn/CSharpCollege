using System.Collections.Generic;
using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Repositories
{
    public interface ILeaderboardRepository
    {
        IReadOnlyList<GameDto> GetTopScores(int count);
    }
}