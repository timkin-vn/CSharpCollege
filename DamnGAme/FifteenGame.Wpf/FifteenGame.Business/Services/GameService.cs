using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Dictionary<string, Game> _games = new Dictionary<string, Game>();

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
            _games.TryGetValue(id, out var game);
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

        private void PlaceShipsRandomly(Field field)
        {
            var sizes = new[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            var random = new Random();

            foreach (var size in sizes)
            {
                Ship ship;
                bool placed;

                do
                {
                    bool horizontal = random.Next(2) == 0;
                    int startX = random.Next(Field.Size);
                    int startY = random.Next(Field.Size);

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

                // проверка окружения (не касаемся других кораблей)
                for (int dx = -1; dx <= 1; dx++)
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
    }

    public enum ShotResult
    {
        Miss,
        Hit,
        Sunk,
        GameOver,
        AlreadyShot,
        Invalid
    }
}