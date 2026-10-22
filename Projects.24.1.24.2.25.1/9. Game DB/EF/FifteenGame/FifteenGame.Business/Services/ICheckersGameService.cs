using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersGame.Business.Models;

namespace CheckersGame.Business.Contracts
{
    public interface ICheckersGameService
    {
        GameModel CreateNewGame(int userId);
        GameModel LoadGame(int gameId);
        GameModel? GetLastActiveGame(int userId);   // Сохранение
        void MakeMove(int gameId, int fromRow, int fromCol, int toRow, int toCol);
        void FinishCapture(int gameId);
        bool IsGameOver(int gameId);
        void DeleteGame(int gameId);
    }
}