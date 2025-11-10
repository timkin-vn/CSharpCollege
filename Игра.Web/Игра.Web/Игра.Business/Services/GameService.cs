using Игра.Business.Models;
using System;

namespace Игра.Business.Services
{
    public class GameService
    {
        private readonly GameModel _gameModel;

        public GameService(GameModel gameModel)
        {
            _gameModel = gameModel;
            InitializeBoard();
        }

        public void Toggle(int row, int col)
        {
            int[] TRow = { 0, 0, 0, 1, -1 };
            int[] TCol = { 0, 1, -1, 0, 0 };

            for (int i = 0; i < TRow.Length; i++)
            {
                int newRow = row + TRow[i];
                int newCol = col + TCol[i];

                if (newRow >= 0 && newRow < GameModel.Size && newCol >= 0 && newCol < GameModel.Size)
                {
                    _gameModel.Cells[newRow, newCol] = !_gameModel.Cells[newRow, newCol];
                }
            }
        }

        public bool CheckForWin()
        {
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    if (!_gameModel.Cells[r, c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void InitializeBoard()
        {
            var random = new Random();

            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    _gameModel.Cells[r, c] = false;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                int randRow = random.Next(GameModel.Size);
                int randCol = random.Next(GameModel.Size);
                Toggle(randRow, randCol);
            }
        }

        public bool GetCellState(int row, int col)
        {
            return _gameModel.Cells[row, col];
        }
    }
}