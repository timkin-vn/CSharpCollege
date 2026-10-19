using System;
using System.Threading.Tasks;
using MinesweeperGame.Common.BusinessModels;

namespace MinesweeperGame.Common.Contracts
{
    public interface IGameRepository
    {
        Task<GameBoard> CreateAsync(GameBoard gameBoard);
        Task<GameBoard> GetByIdAsync(Guid id);
        Task UpdateAsync(GameBoard gameBoard);
        Task DeleteAsync(Guid id);
    }
}
