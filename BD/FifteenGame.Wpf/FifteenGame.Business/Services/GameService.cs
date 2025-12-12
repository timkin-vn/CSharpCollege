using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public enum ShotResult
    {
        Miss,
        Hit,
        Sunk,
        GameOver,
        AlreadyShot,
        Invalid
    }

    public struct AttackResult
    {
        public bool Hit { get; set; }
        public bool Sunk { get; set; }
        public bool GameOver { get; set; }
    }

    public class GameService
    {
        private readonly Dictionary<string, Game> _games = new Dictionary<string, Game>();
        private readonly Random _random = new Random();

        public Game CreateGame()
        {
            var game = new Game();
            PlaceShipsRandomly(game.PlayerField);
            PlaceShipsRandomly(game.OpponentField);
            _games[game.Id] = game;
            return game;
        }

        public Game GetGame(string id)
        {
            Game game;
            _games.TryGetValue(id, out game);
            return game;
        }

        public ShotResult Shoot(string gameId, int x, int y)
        {
            if (!_games.TryGetValue(gameId, out var game))
                throw new KeyNotFoundException("Игра не найдена");

            var targetField = game.IsPlayerTurn ? game.OpponentField : game.PlayerField;

            if (x < 0 || x >= Field.Size || y < 0 || y >= Field.Size)
                return ShotResult.Invalid;

            var cell = targetField.Cells[x, y];

            if (cell.State == CellState.Hit || cell.State == CellState.Miss || cell.State == CellState.Sunk)
                return ShotResult.AlreadyShot;

            if (cell.Ship == null)
            {
                cell.State = CellState.Miss;
                game.IsPlayerTurn = !game.IsPlayerTurn;
                return ShotResult.Miss;
            }

            cell.State = CellState.Hit;

            if (cell.Ship.IsSunk)
            {
                foreach (var c in cell.Ship.Cells)
                    c.State = CellState.Sunk;

                if (targetField.Ships.All(s => s.IsSunk))
                    return ShotResult.GameOver;

                return ShotResult.Sunk;
            }

            game.IsPlayerTurn = !game.IsPlayerTurn;
            return ShotResult.Hit;
        }

        public void PlaceShipsRandomly(Field field)
        {
            var sizes = new[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

            foreach (var size in sizes)
            {
                Ship ship;
                bool placed;
                do
                {
                    bool horizontal = _random.Next(2) == 0;
                    int startX = _random.Next(Field.Size);
                    int startY = _random.Next(Field.Size);
                    ship = new Ship(size);
                    placed = TryPlaceShip(field, ship, startX, startY, horizontal);
                } while (!placed);

                field.Ships.Add(ship);
            }
        }

        private bool TryPlaceShip(Field field, Ship ship, int startX, int startY, bool horizontal)
        {
            var cells = new List<Cell>();

            for (int i = 0; i < ship.Size; i++)
            {
                int x = horizontal ? startX + i : startX;
                int y = horizontal ? startY : startY + i;

                if (x >= Field.Size || y >= Field.Size)
                    return false;

                if (field.Cells[x, y].Ship != null)
                    return false;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        if (nx >= 0 && nx < Field.Size && ny >= 0 && ny < Field.Size)
                        {
                            if (field.Cells[nx, ny].Ship != null)
                                return false;
                        }
                    }
                }

                cells.Add(field.Cells[x, y]);
            }

            foreach (var cell in cells)
            {
                cell.Ship = ship;
                cell.State = CellState.Ship;
                ship.Cells.Add(cell);
            }

            return true;
        }

        // Новые методы для ViewModel

        public void Initialize(Field playerField, Field computerField)
        {
            PlaceShipsRandomly(playerField);
            PlaceShipsRandomly(computerField);
        }

        public AttackResult PlayerAttack(Field enemyField, int x, int y)
        {
            if (x < 0 || x >= Field.Size || y < 0 || y >= Field.Size)
                return new AttackResult { Hit = false, Sunk = false, GameOver = false };

            var cell = enemyField.Cells[x, y];

            if (cell.State == CellState.Hit || cell.State == CellState.Miss || cell.State == CellState.Sunk)
                return new AttackResult { Hit = false, Sunk = false, GameOver = false };

            if (cell.Ship == null)
            {
                cell.State = CellState.Miss;
                return new AttackResult { Hit = false, Sunk = false, GameOver = false };
            }

            cell.State = CellState.Hit;

            if (cell.Ship.IsSunk)
            {
                foreach (var c in cell.Ship.Cells)
                    c.State = CellState.Sunk;

                bool gameOver = enemyField.Ships.All(s => s.IsSunk);
                return new AttackResult { Hit = true, Sunk = true, GameOver = gameOver };
            }

            return new AttackResult { Hit = true, Sunk = false, GameOver = false };
        }

        public void ComputerAttack(Field playerField, ref int lastHitRow, ref int lastHitColumn, ref bool huntingMode)
        {
            int x, y;

            if (huntingMode && lastHitRow != -1)
            {
                var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
                var shuffledDirections = directions.OrderBy(d => _random.Next()).ToArray();

                bool shotTaken = false;
                foreach (var dir in shuffledDirections)
                {
                    x = lastHitRow + dir.Item1;
                    y = lastHitColumn + dir.Item2;

                    if (x >= 0 && x < Field.Size && y >= 0 && y < Field.Size)
                    {
                        var cell = playerField.Cells[x, y];
                        if (cell.State != CellState.Hit && cell.State != CellState.Miss && cell.State != CellState.Sunk)
                        {
                            PerformShot(playerField, x, y, ref lastHitRow, ref lastHitColumn, ref huntingMode);
                            shotTaken = true;
                            break;
                        }
                    }
                }

                if (shotTaken) return;
            }

            // Случайный выстрел
            do
            {
                x = _random.Next(Field.Size);
                y = _random.Next(Field.Size);
            } while (playerField.Cells[x, y].State == CellState.Hit || playerField.Cells[x, y].State == CellState.Miss || playerField.Cells[x, y].State == CellState.Sunk);

            PerformShot(playerField, x, y, ref lastHitRow, ref lastHitColumn, ref huntingMode);
        }

        private void PerformShot(Field field, int x, int y, ref int lastHitRow, ref int lastHitColumn, ref bool huntingMode)
        {
            var cell = field.Cells[x, y];

            if (cell.Ship == null)
            {
                cell.State = CellState.Miss;
                huntingMode = false;
            }
            else
            {
                cell.State = CellState.Hit;
                lastHitRow = x;
                lastHitColumn = y;
                huntingMode = true;

                if (cell.Ship.IsSunk)
                {
                    foreach (var c in cell.Ship.Cells)
                        c.State = CellState.Sunk;

                    huntingMode = false;
                    lastHitRow = -1;
                    lastHitColumn = -1;
                }
            }
        }

        public int CountShipsLeft(Field field)
        {
            return field.Ships.Sum(s => s.Cells.Count(c => c.State == CellState.Ship));
        }

        public bool IsGameOver(Field field)
        {
            return field.Ships.All(s => s.IsSunk);
        }

        public void ToggleFlag(Field field, int x, int y)
        {
            var cell = field.Cells[x, y];
            if (cell.State == CellState.Empty)
                cell.State = CellState.Miss; // или специальный флаг, но для простоты Miss как флаг
            else if (cell.State == CellState.Miss)
                cell.State = CellState.Empty;
        }
    }
}