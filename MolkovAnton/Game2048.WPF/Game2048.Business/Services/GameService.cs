using Game2048.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game2048.Business.Services
{
    public class GameService
    {
            private readonly Random _random = new Random();

            public void Initialize(GameModel model)
            {
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        model[row, column] = 0;
                    }
                }

                AddRandomTile(model);
                AddRandomTile(model);
            }

            public bool IsGameOver(GameModel model)
            {
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        if (model[row, column] == 0)
                        {
                            return false;
                        }
                    }
                }

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount - 1; column++)
                    {
                        if (model[row, column] == model[row, column + 1])
                        {
                            return false;
                        }
                    }
                }

                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    for (int row = 0; row < GameModel.RowCount - 1; row++)
                    {
                        if (model[row, column] == model[row + 1, column])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            int[,] oldCells = new int[GameModel.RowCount, GameModel.ColumnCount];
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    oldCells[row, column] = model[row, column];
                }
            }

            switch (direction)
            {
                case MoveDirection.Left:
                    MoveLeft(model);
                    break;
                case MoveDirection.Right:
                    MoveRight(model);
                    break;
                case MoveDirection.Up:
                    MoveUp(model);
                    break;
                case MoveDirection.Down:
                    MoveDown(model);
                    break;
            }

            bool has2048 = false;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == 2048)
                    {
                        has2048 = true;
                        break;
                    }
                }
                if (has2048) break;
            }

            if (has2048)
            {
                MessageBox.Show("Поздравляем! Вы прошли игру" , "Конец игры");
                Initialize(model);
                
            }

            if (!oldCells.Cast<int>().SequenceEqual(model.Cells.Cast<int>()))
            {
                AddRandomTile(model);
                return true;
            }

            return false;
        }

        private void MoveLeft(GameModel model)
            {
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    int[] line = new int[GameModel.ColumnCount];
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        line[column] = model[row, column];
                    }

                    line = MergeLine(line, model);
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        model[row, column] = line[column];
                    }
                }
            }

            private void MoveRight(GameModel model)
            {
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    int[] line = new int[GameModel.ColumnCount];
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        line[column] = model[row, GameModel.ColumnCount - 1 - column];
                    }

                    line = MergeLine(line, model);
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        model[row, GameModel.ColumnCount - 1 - column] = line[column];
                    }
                }
            }

            private void MoveUp(GameModel model)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    int[] line = new int[GameModel.RowCount];
                    for (int row = 0; row < GameModel.RowCount; row++)
                    {
                        line[row] = model[row, column];
                    }

                    line = MergeLine(line, model);
                    for (int row = 0; row < GameModel.RowCount; row++)
                    {
                        model[row, column] = line[row];
                    }
                }
            }

            private void MoveDown(GameModel model)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    int[] line = new int[GameModel.RowCount];
                    for (int row = 0; row < GameModel.RowCount; row++)
                    {
                        line[row] = model[GameModel.RowCount - 1 - row, column];
                    }

                    line = MergeLine(line, model);
                    for (int row = 0; row < GameModel.RowCount; row++)
                    {
                        model[GameModel.RowCount - 1 - row, column] = line[row];
                    }
                }
            }

            private int[] MergeLine(int[] line, GameModel model)
            {
                int[] newLine = line.Where(x => x != 0).ToArray();
                for (int i = 0; i < newLine.Length - 1; i++)
                {
                    if (newLine[i] == newLine[i + 1])
                    {
                        newLine[i] *= 2;
                        newLine[i + 1] = 0;
                    }
                }

                newLine = newLine.Where(x => x != 0).ToArray();
                return newLine.Concat(Enumerable.Repeat(0, GameModel.ColumnCount - newLine.Length)).ToArray();
            }

            private void AddRandomTile(GameModel model)
            {
                var emptyCells = new List<(int, int)>();
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        if (model[row, column] == 0)
                        {
                            emptyCells.Add((row, column));
                        }
                    }
                }

                if (emptyCells.Count > 0)
                {
                    var (row, column) = emptyCells[_random.Next(emptyCells.Count)];
                    model[row, column] = _random.Next(10) < 9 ? 2 : 4;
                }
            }
        }
    }



