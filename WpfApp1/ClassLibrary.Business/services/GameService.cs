using SimpleMinesweeper.Models;
using System;

namespace SimpleMinesweeper.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void InitializeGame(GameModel model)
        {
            model.Moves = 0;
            model.State = GameState.Playing;

            // Очистка доски
            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    model.Board[i, j] = 0;
                    model.OpenedCells[i, j] = false;
                    model.FlaggedCells[i, j] = false;
                }
            }

            // Расставляем бомбы
            PlaceBombs(model);
        }

        private void PlaceBombs(GameModel model)
        {
            int placed = 0;
            while (placed < model.BombsCount)
            {
                int r = _random.Next(GameModel.Size);
                int c = _random.Next(GameModel.Size);
                if (model.Board[r, c] == 0)
                {
                    model.Board[r, c] = 1; // бомба
                    placed++;
                }
            }
        }

        public void OpenCell(GameModel model, int row, int col)
        {
            if (model.State != GameState.Playing ||
                model.OpenedCells[row, col] ||
                model.FlaggedCells[row, col])
                return;

            model.Moves++;
            model.OpenedCells[row, col] = true;

            if (model.Board[row, col] == 1) // Бомба
            {
                model.State = GameState.Lost;
                RevealAllBombs(model); // Раскрываем все бомбы при проигрыше
                return;
            }

            // Проверяем победу
            if (CheckWinCondition(model))
            {
                model.State = GameState.Won;
            }
        }

        public void ToggleFlag(GameModel model, int row, int col)
        {
            if (model.State != GameState.Playing || model.OpenedCells[row, col])
                return;

            model.FlaggedCells[row, col] = !model.FlaggedCells[row, col];
        }

        public int CountAdjacentBombs(GameModel model, int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < GameModel.Size && j >= 0 && j < GameModel.Size &&
                        model.Board[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        private bool CheckWinCondition(GameModel model)
        {
            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    // Если есть неоткрытая клетка без бомбы - игра не окончена
                    if (!model.OpenedCells[i, j] && model.Board[i, j] == 0)
                        return false;
                }
            }
            return true;
        }

        // Новый метод: раскрыть все бомбы при проигрыше
        public void RevealAllBombs(GameModel model)
        {
            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    if (model.Board[i, j] == 1) // Если это бомба
                    {
                        model.OpenedCells[i, j] = true; // Раскрываем клетку с бомбой
                    }
                }
            }
        }
    }
}