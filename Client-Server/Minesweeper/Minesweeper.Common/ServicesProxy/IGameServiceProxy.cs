using Minesweeper.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.BusinessProxy.Services
{
    public interface IGameServiceProxy
    {
        Task<GameResponse> CreateGame(int userId, int size, int mines);
        Task<GameResponse> GetGame(int gameId);
        Task<GameResponse> OpenCell(int gameId, int row, int col);
        Task<GameResponse> ToggleFlag(int gameId, int row, int col);
        Task<GameResponse> GetLastActiveGame(int userId);
        Task<bool> IsGameOver(int gameId);
        Task<bool> IsGameWon(int gameId);
    }
}
