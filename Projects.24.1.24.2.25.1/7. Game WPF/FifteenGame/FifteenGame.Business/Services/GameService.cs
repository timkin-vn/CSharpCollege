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
        private readonly Random _rnd = new Random();

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = MoveDirection.Empty;
                }
            }
            model.Ships.Clear();

            var fleetTemplate = new Dictionary<int, int>
            {
                { 4, 1 },
                { 3, 2 },
                { 2, 3 },
                { 1, 4 }
            };

            foreach (var shipType in fleetTemplate)
            {
                int deckCount = shipType.Key;
                int shipCount = shipType.Value;

                for (int i = 0; i < shipCount; i++)
                {
                    PlaceShipRandomly(model, deckCount);
                }
            }
        }

        private void PlaceShipRandomly(GameModel model, int deckCount)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < 1000)
            {
                attempts++;
                int startRow = _rnd.Next(GameModel.RowCount);
                int startCol = _rnd.Next(GameModel.ColumnCount);
                bool isHorizontal = _rnd.Next(2) == 0;

                if (isHorizontal && startCol + deckCount > GameModel.ColumnCount) continue;
                if (!isHorizontal && startRow + deckCount > GameModel.RowCount) continue;

                var coords = new List<(int Row, int Column)>();
                for (int d = 0; d < deckCount; d++)
                {
                    int r = isHorizontal ? startRow : startRow + d;
                    int c = isHorizontal ? startCol + d : startCol;
                    coords.Add((r, c));
                }

                if (CanPlaceShip(model, coords))
                {
                    var newShip = new ShipModel();
                    // Используем .AddRange(), чтобы обойти ошибку CS0200 (доступ только для чтения)
                    newShip.Coordinates.AddRange(coords);
                    model.Ships.Add(newShip);

                    foreach (var coord in coords)
                    {
                        model[coord.Row, coord.Column] = MoveDirection.Ship;
                    }
                    placed = true;
                }
            }
        }

        private bool CanPlaceShip(GameModel model, List<(int Row, int Column)> coords)
        {
            foreach (var coord in coords)
            {
                for (int dr = -1; dr <= 1; dr++)
                {
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        int checkRow = coord.Row + dr;
                        int checkCol = coord.Column + dc;

                        if (checkRow >= 0 && checkRow < GameModel.RowCount &&
                            checkCol >= 0 && checkCol < GameModel.ColumnCount)
                        {
                            if (model[checkRow, checkCol] == MoveDirection.Ship)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
                return false;

            var currentState = model[row, column];

            if (currentState == MoveDirection.Miss || currentState == MoveDirection.Hit || currentState == MoveDirection.Destroyed)
                return false;

            if (currentState == MoveDirection.Ship)
            {
                model[row, column] = MoveDirection.Hit;

                // Находим корабль, в который попали
                var ship = model.Ships.FirstOrDefault(s => s.Coordinates.Contains((row, column)));

                // Проверяем уничтожение прямо здесь, чтобы исправить ошибку CS1061 (отсутствие метода IsSunk)
                if (ship != null && ship.Coordinates.All(c => model[c.Row, c.Column] == MoveDirection.Hit))
                {
                    foreach (var coord in ship.Coordinates)
                    {
                        model[coord.Row, coord.Column] = MoveDirection.Destroyed;
                    }
                }
                return true;
            }

            model[row, column] = MoveDirection.Miss;
            return false;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == MoveDirection.Ship)
                    {
                        return false;
                    }
                }
            }
            return model.Ships.Count > 0;
        }
    }
}

