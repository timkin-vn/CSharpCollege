using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IMazeGameService
    {
        GameManager GetMazeGameByUserId(int userId);
        GameManager MakeMazeMove(int userId, int deltaRow, int deltaCol);
        void StartNewMazeGame(int userId);
        bool IsMazeGameOver(int userId);
    }
}
