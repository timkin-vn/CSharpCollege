using FifteenGame.Business.Models;
using System;

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
                    moved = ShiftAndMergeLeft(model);
                    break;
                case MoveDirection.Right:
                    moved = ShiftAndMergeRight(model);
                    break;
                case MoveDirection.Up:
                    moved = ShiftAndMergeUp(model);
                    break;
                case MoveDirection.Down:
                    moved = ShiftAndMergeDown(model);
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

        private bool ShiftAndMergeLeft(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < GameModel.Size; row++)
            {
                int[] newRow = new int[GameModel.Size];
                int index = 0;
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model[row, col] != 0)
                    {
                        if (index > 0 && newRow[index - 1] == model[row, col])
                        {
                            newRow[index - 1] *= 2;
                            moved = true;
                        }
                        else
                        {
                            newRow[index++] = model[row, col];
                        }
                    }
                }
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model[row, col] != newRow[col])
                    {
                        moved = true;
                    }
                    model[row, col] = newRow[col];
                }
            }
            return moved;
        }

        private bool ShiftAndMergeRight(GameModel model)
        {
            bool moved = false;
            for (int row = 0; row < GameModel.Size; row++)
            {
                int[] newRow = new int[GameModel.Size];
                int index = GameModel.Size - 1;
                for (int col = GameModel.Size - 1; col >= 0; col--)
                {
                    if (model[row, col] != 0)
                    {
                        if (index < GameModel.Size - 1 && newRow[index + 1] == model[row, col])
                        {
                            newRow[index + 1] *= 2;
                            moved = true;
                        }
                        else
                        {
                            newRow[index--] = model[row, col];
                        }
                    }
                }
                for (int col = 0; col < GameModel.Size; col++)
                {
                    if (model[row, col] != newRow[col])
                    {
                        moved = true;
                    }
                    model[row, col] = newRow[col];
                }
            }
            return moved;
        }

        private bool ShiftAndMergeUp(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < GameModel.Size; col++)
            {
                int[] newCol = new int[GameModel.Size];
                int index = 0;
                for (int row = 0; row < GameModel.Size; row++)
                {
                    if (model[row, col] != 0)
                    {
                        if (index > 0 && newCol[index - 1] == model[row, col])
                        {
                            newCol[index - 1] *= 2;
                            moved = true;
                        }
                        else
                        {
                            newCol[index++] = model[row, col];
                        }
                    }
                }
                for (int row = 0; row < GameModel.Size; row++)
                {
                    if (model[row, col] != newCol[row])
                    {
                        moved = true;
                    }
                    model[row, col] = newCol[row];
                }
            }
            return moved;
        }

        private bool ShiftAndMergeDown(GameModel model)
        {
            bool moved = false;
            for (int col = 0; col < GameModel.Size; col++)
            {
                int[] newCol = new int[GameModel.Size];
                int index = GameModel.Size - 1;
                for (int row = GameModel.Size - 1; row >= 0; row--)
                {
                    if (model[row, col] != 0)
                    {
                        if (index < GameModel.Size - 1 && newCol[index + 1] == model[row, col])
                        {
                            newCol[index + 1] *= 2;
                            moved = true;
                        }
                        else
                        {
                            newCol[index--] = model[row, col];
                        }
                    }
                }
                for (int row = 0; row < GameModel.Size; row++)
                {
                    if (model[row, col] != newCol[row])
                    {
                        moved = true;
                    }
                    model[row, col] = newCol[row];
                }
            }
            return moved;
        }
    }
}
