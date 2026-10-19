using System;
using System.Threading.Tasks;
using MinesweeperGame.Common.Definitions;
using MinesweeperGame.Common.Dtos;

namespace MinesweeperGame.Common.Contracts
{
    public interface IGameService
    {
        Task<GameResponse> CreateGameAsync(GameDifficulty difficulty);
        Task<GameResponse> RevealCellAsync(Guid gameId, int row, int column);
        Task<GameResponse> ToggleFlagAsync(Guid gameId, int row, int column);
        Task<GameResponse> GetGameAsync(Guid gameId);
    }
}
