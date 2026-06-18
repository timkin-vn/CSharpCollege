using System.Collections.Generic;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Interfaces.Services;
using Pacman.Common.Models;

namespace Pacman.Business.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ILeaderboardRepository _leaderboardRepository;

        public LeaderboardService(ILeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        public IReadOnlyList<GameDto> GetTopScores(int count = 10)
        {
            return _leaderboardRepository.GetTopScores(count);
        }
    }
}