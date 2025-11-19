using FifteenGame.Business.Models;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly int[] ShipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
        private readonly Random rnd = new Random();

        public void Initialize(GameField playerField, GameField computerField)
        {
            PlaceAllShips(playerField);
            PlaceAllShips(computerField);
        }

        private void PlaceAllShips(GameField field)
        {
            field.Clear();

            foreach (int size in ShipSizes)
            {
                bool placed = false;
                int maxAttempts = 10000;

                for (int attempt = 0; attempt < maxAttempts && !placed; attempt++)
                {
                    bool horizontal = rnd.Next(2) == 0;

                    int maxRow = horizontal ? GameField.Size : GameField.Size - size + 1;
                    int maxCol = horizontal ? GameField.Size - size + 1 : GameField.Size;

                    if (maxRow <= 0 || maxCol <= 0) continue;

                    int row = rnd.Next(maxRow);
                    int col = rnd.Next(maxCol);

                    if (field.CanPlaceShip(row, col, size, horizontal))
                    {
                        field.PlaceShip(row, col, size, horizontal);
                        placed = true;
                    }
                }

                // Если вдруг не получилось — форсируем в левый верхний угол
                if (!placed)
                {
                    for (int row = 0; row < GameField.Size && !placed; row++)
                    {
                        for (int col = 0; col < GameField.Size && !placed; col++)
                        {
                            foreach (bool hor in new[] { true, false })
                            {
                                if ((hor && col + size <= GameField.Size) ||
                                    (!hor && row + size <= GameField.Size))
                                {
                                    if (field.CanPlaceShip(row, col, size, hor))
                                    {
                                        field.PlaceShip(row, col, size, hor);
                                        placed = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool PlayerAttack(GameField enemyField, int row, int col)
        {
            return enemyField.Shoot(row, col);
        }

        public void ComputerAttack(GameField playerField, ref int lastHitRow, ref int lastHitCol, ref bool hunting)
        {
            int r, c;

            if (hunting && lastHitRow != -1)
            {
                int[][] directions = { new[] { -1, 0 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { 0, 1 } };
                var shuffled = directions.OrderBy(x => rnd.Next()).ToArray();

                foreach (var dir in shuffled)
                {
                    r = lastHitRow + dir[0];
                    c = lastHitCol + dir[1];

                    if (GameField.IsValid(r, c) && playerField[r, c] != 'H' && playerField[r, c] != 'M')
                    {
                        bool hit = playerField.Shoot(r, c);
                        if (hit)
                        {
                            lastHitRow = r;
                            lastHitCol = c;
                            return;
                        }
                        else
                        {
                            hunting = false;
                            return;
                        }
                    }
                }
                hunting = false;
            }

            // Случайный выстрел
            do
            {
                r = rnd.Next(GameField.Size);
                c = rnd.Next(GameField.Size);
            } while (playerField[r, c] == 'H' || playerField[r, c] == 'M');

            bool shipHit = playerField.Shoot(r, c);
            if (shipHit)
            {
                lastHitRow = r;
                lastHitCol = c;
                hunting = true;
            }
        }

        public bool IsGameOver(GameField field)
        {
            return field.GetRemainingShips() == 0;
        }
    }
}