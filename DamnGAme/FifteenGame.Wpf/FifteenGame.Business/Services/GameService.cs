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
        private readonly int[] ShipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 }; // Стандартный набор кораблей

        public void Initialize(GameField playerField, GameField computerField)
        {
            PlaceShips(playerField);
            PlaceShips(computerField);
        }

        private void PlaceShips(GameField field)
        {
            var rnd = new Random();
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
            Console.WriteLine($"Атака: row={row}, col={column}, текущее состояние={computerField[row, column]}");
            if (computerField[row, column] == 'F') return false; // Уже помечено флагом

            if (computerField[row, column] == 'S')
            {
                computerField[row, column] = 'H'; // Попадание
                Console.WriteLine("Попадание!");
                return true;
            }
            else if (computerField[row, column] == ' ')
            {
                computerField[row, column] = 'M'; // Промах
                Console.WriteLine("Промах!");
                return false;
            }
            Console.WriteLine("Ячейка уже атакована!");
            return false; // Уже атаковано
        }

        public void ToggleFlag(GameField computerField, int row, int column)
        {
            if (computerField[row, column] == ' ') computerField[row, column] = 'F'; // Ставим флаг
            else if (computerField[row, column] == 'F') computerField[row, column] = ' '; // Убираем флаг
        }

        public void ComputerAttack(GameField playerField, ref int lastHitRow, ref int lastHitColumn, ref bool huntingMode)
        {
            var rnd = new Random();
            int row, column;

            if (huntingMode)
            {
                // Ищем вокруг последней удачной атаки
                int[][] directions = new int[][] {
                    new int[] { -1, 0 },
                    new int[] { 1, 0 },
                    new int[] { 0, -1 },
                    new int[] { 0, 1 }
                };
                var shuffledDirections = directions.OrderBy(d => rnd.Next()).ToArray();

                foreach (var dir in shuffledDirections)
                {
                    row = lastHitRow + dir[0];
                    column = lastHitColumn + dir[1];
                    if (row >= 0 && row < GameField.RowCount && column >= 0 && column < GameField.ColumnCount &&
                        playerField[row, column] != 'H' && playerField[row, column] != 'M')
                    {
                        if (playerField[row, column] == 'S')
                        {
                            playerField[row, column] = 'H';
                            lastHitRow = row;
                            lastHitColumn = column;
                            Console.WriteLine($"Компьютер попал: row={row}, col={column}");
                            return;
                        }
                        else
                        {
                            playerField[row, column] = 'M';
                            huntingMode = false; // Промах - выходим из режима охоты
                            Console.WriteLine($"Компьютер промахнулся: row={row}, col={column}");
                            return;
                        }
                    }
                }
                huntingMode = false; // Нет доступных соседей - выходим из режима
            }

            // Случайная атака
            do
            {
                row = rnd.Next(GameField.RowCount);
                column = rnd.Next(GameField.ColumnCount);
            } while (playerField[row, column] == 'H' || playerField[row, column] == 'M');

            if (playerField[row, column] == 'S')
            {
                playerField[row, column] = 'H';
                lastHitRow = row;
                lastHitColumn = column;
                huntingMode = true;
                Console.WriteLine($"Компьютер попал: row={row}, col={column}");
            }
            else
            {
                playerField[row, column] = 'M';
                Console.WriteLine($"Компьютер промахнулся: row={row}, col={column}");
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

        public int CountShipsLeft(GameField field)
        {
            int count = 0;
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    if (field[row, column] == 'S')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}