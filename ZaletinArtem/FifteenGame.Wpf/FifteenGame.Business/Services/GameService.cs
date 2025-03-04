using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        public bool Move(GameModel model, MoveDirection direction)
        {
            bool moved = false;
            switch (direction)
            {
                case MoveDirection.Left:
                    moved = ShiftLeft(model);
                    break;
                case MoveDirection.Right:
                    moved = ShiftRight(model);
                    break;
                case MoveDirection.Up:
                    moved = ShiftUp(model);
                    break;
                case MoveDirection.Down:
                    moved = ShiftDown(model);
                    break;
            }

            if (moved)
            {
                model.AddRandomTile();
            }

            return moved;
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model[row, col] == 0)
                        return false;
                    if (col < GameModel.Size - 1 && model[row, col] == model[row, col + 1])
                        return false;
                    if (row < GameModel.Size - 1 && model[row, col] == model[row + 1, col])
                        return false;
                }
            }
            return true;
        }

        public bool HasWon(GameModel model)
        {
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model[row, col] == 2048)
                        return true;
                }
            }
            return false;
        }

        private bool ShiftLeft(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 1; col < GameModel.Size; col++)
                {
                    if (model[row, col] != 0)
                    {
                        int targetCol = col;
                        while (targetCol > 0 && model[row, targetCol - 1] == 0)
                        {
                            model[row, targetCol - 1] = model[row, targetCol];
                            model[row, targetCol] = 0;
                            targetCol--;
                            moved = true;
                        }
                        if (targetCol > 0 && model[row, targetCol - 1] == model[row, targetCol])
                        {
                            model[row, targetCol - 1] *= 2;
                            model[row, targetCol] = 0;
                            moved = true;
                        }
                    }
                }
            }
            return moved;
        }

        private bool ShiftRight(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = GameModel.Size - 2; col >= 0; col--)
                {
                    if (model[row, col] != 0)
                    {
                        int targetCol = col;
                        while (targetCol < GameModel.Size - 1 && model[row, targetCol + 1] == 0)
                        {
                            model[row, targetCol + 1] = model[row, targetCol];
                            model[row, targetCol] = 0;
                            targetCol++;
                            moved = true;
                        }
                        if (targetCol < GameModel.Size - 1 && model[row, targetCol + 1] == model[row, targetCol])
                        {
                            model[row, targetCol + 1] *= 2;
                            model[row, targetCol] = 0;
                            moved = true;
                        }
                    }
                }
            }
            return moved;
        }

        private bool ShiftUp(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < GameModel.Size; col++)
            {
                for (int row = 1; row < GameModel.Size; row++)
                {
                    if (model[row, col] != 0)
                    {
                        int targetRow = row;
                        while (targetRow > 0 && model[targetRow - 1, col] == 0)
                        {
                            model[targetRow - 1, col] = model[targetRow, col];
                            model[targetRow, col] = 0;
                            targetRow--;
                            moved = true;
                        }
                        if (targetRow > 0 && model[targetRow - 1, col] == model[targetRow, col])
                        {
                            model[targetRow - 1, col] *= 2;
                            model[targetRow, col] = 0;
                            moved = true;
                        }
                    }
                }
            }
            return moved;
        }

        private bool ShiftDown(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < GameModel.Size; col++)
            {
                for (int row = GameModel.Size - 2; row >= 0; row--)
                {
                    if (model[row, col] != 0)
                    {
                        int targetRow = row;
                        while (targetRow < GameModel.Size - 1 && model[targetRow + 1, col] == 0)
                        {
                            model[targetRow + 1, col] = model[targetRow, col];
                            model[targetRow, col] = 0;
                            targetRow++;
                            moved = true;
                        }
                        if (targetRow < GameModel.Size - 1 && model[targetRow + 1, col] == model[targetRow, col])
                        {
                            model[targetRow + 1, col] *= 2;
                            model[targetRow, col] = 0;
                            moved = true;
                        }
                    }
                }
            }
            return moved;
        }
    }
}