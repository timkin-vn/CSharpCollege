using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly int[] ShipSizes = new int[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 }; // Стандартный набор кораблей

        public void Initialize(GameField playerField, GameField computerField)
        {
            PlaceShips(playerField);
            PlaceShips(computerField);
        }

        private void PlaceShips(GameField field)
        {
            Random rnd = new Random();
            foreach (int size in ShipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = rnd.Next(GameField.RowCount);
                    int col = rnd.Next(GameField.ColumnCount);
                    bool horizontal = rnd.Next(2) == 0;

                    if (CanPlaceShip(field, row, col, size, horizontal))
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (horizontal)
                            {
                                field[row, col + i] = 'S';
                            }
                            else
                            {
                                field[row + i, col] = 'S';
                            }
                        }
                        placed = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(GameField field, int row, int col, int size, bool horizontal)
        {
            if (horizontal)
            {
                if (col + size > GameField.ColumnCount) return false;
                for (int i = -1; i <= size; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newRow = row + j;
                        int newCol = col + i;
                        if (newRow >= 0 && newRow < GameField.RowCount && newCol >= 0 && newCol < GameField.ColumnCount && field[newRow, newCol] == 'S')
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (row + size > GameField.RowCount) return false;
                for (int i = -1; i <= size; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int newRow = row + i;
                        int newCol = col + j;
                        if (newRow >= 0 && newRow < GameField.RowCount && newCol >= 0 && newCol < GameField.ColumnCount && field[newRow, newCol] == 'S')
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool PlayerAttack(GameField computerField, int row, int column)
        {
            if (computerField[row, column] == 'S')
            {
                computerField[row, column] = 'H'; // Попадание
                return true;
            }
            else if (computerField[row, column] == ' ')
            {
                computerField[row, column] = 'M'; // Промах
                return false;
            }
            return false; // Уже атаковано
        }

        public void ComputerAttack(GameField playerField)
        {
            Random rnd = new Random();
            int row, column;
            do
            {
                row = rnd.Next(GameField.RowCount);
                column = rnd.Next(GameField.ColumnCount);
            } while (playerField[row, column] == 'H' || playerField[row, column] == 'M');

            if (playerField[row, column] == 'S')
            {
                playerField[row, column] = 'H';
            }
            else
            {
                playerField[row, column] = 'M';
            }
        }

        public bool IsGameOver(GameField field)
        {
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    if (field[row, column] == 'S')
                    {
                        return false; // Ещё есть корабли
                    }
                }
            }
            return true;
        }
    }
}