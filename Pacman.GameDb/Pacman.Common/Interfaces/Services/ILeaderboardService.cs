using System.Collections.Generic;
using Pacman.Common.Models;

namespace Pacman.Common.Interfaces.Services
{
    public interface ILeaderboardService
    {
        IReadOnlyList<GameDto> GetTopScores(int count = 10);
    }
}
