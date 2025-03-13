using FifteenGame.Business.Models;
using System.Collections.Generic;
using System;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _random = new Random();

        public (int row, int col) GetBotMove(GameModel model)
        {
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model.Board[row, col] == GameModel.EmptyCell)
                    {
                        model.Board[row, col] = GameModel.Bot;
                        if (model.CheckWin(GameModel.Bot))
                        {
                            model.Board[row, col] = GameModel.EmptyCell;
                            return (row, col);
                        }
                        model.Board[row, col] = GameModel.EmptyCell;
                    }
                }
            }

            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model.Board[row, col] == GameModel.EmptyCell)
                    {
                        model.Board[row, col] = GameModel.Player;
                        if (model.CheckWin(GameModel.Player))
                        {
                            model.Board[row, col] = GameModel.EmptyCell;
                            return (row, col);
                        }
                        model.Board[row, col] = GameModel.EmptyCell;
                    }
                }
            }

            List<(int row, int col)> emptyCells = new List<(int row, int col)>();
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model.Board[row, col] == GameModel.EmptyCell)
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                int index = _random.Next(emptyCells.Count);
                return emptyCells[index];
            }
            return (-1, -1);
        }
    }
}