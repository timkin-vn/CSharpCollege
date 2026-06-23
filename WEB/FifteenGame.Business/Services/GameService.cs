using FifteenGame.Business.Models;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _rnd = new Random();

        // =================== НОВАЯ ИГРА ===================

        public void NewGame(GameModel model)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    model.PlayerField[i, j] = CellState.Empty;
                    model.PlayerFog[i, j] = CellState.Empty;
                    model.ComputerField[i, j] = CellState.Empty;
                    model.ComputerFog[i, j] = CellState.Empty;
                }

            model.PlayerShips.Clear();
            model.ComputerShips.Clear();

            model.IsSetupPhase = true;
            model.IsPlayerTurn = true;
            model.GameOver = false;
            model.PlayerWon = false;
            model.ShipsToPlace = 10;
            model.IsHorizontal = true;

            PlaceComputerShips(model);
            model.CurrentPlacingShip = new Ship { Type = ShipType.Quad };
        }

        // =================== РАССТАНОВКА ПК ===================

        private void PlaceComputerShips(GameModel model)
        {
            var ships = new[]
            {
                ShipType.Quad,
                ShipType.Triple, ShipType.Triple,
                ShipType.Double, ShipType.Double, ShipType.Double,
                ShipType.Single, ShipType.Single, ShipType.Single, ShipType.Single
            };

            foreach (var type in ships)
            {
                while (true)
                {
                    bool hor = _rnd.Next(2) == 0;
                    int x = _rnd.Next(10);
                    int y = _rnd.Next(10);

                    if (CanPlace(model.ComputerField, x, y, type, hor))
                    {
                        PlaceShip(model.ComputerField, x, y, type, hor);
                        model.ComputerShips.Add(new Ship { X = x, Y = y, Type = type, IsHorizontal = hor });
                        break;
                    }
                }
            }
        }

        // =================== РАССТАНОВКА ИГРОКА ===================

        public bool TryPlacePlayerShip(GameModel model, int x, int y)
        {
            if (!model.IsSetupPhase || model.CurrentPlacingShip == null) return false;

            if (CanPlace(model.PlayerField, x, y, model.CurrentPlacingShip.Type, model.IsHorizontal))
            {
                PlaceShip(model.PlayerField, x, y, model.CurrentPlacingShip.Type, model.IsHorizontal);

                model.PlayerShips.Add(new Ship
                {
                    X = x,
                    Y = y,
                    Type = model.CurrentPlacingShip.Type,
                    IsHorizontal = model.IsHorizontal
                });

                model.ShipsToPlace--;

                if (model.ShipsToPlace == 0)
                {
                    model.IsSetupPhase = false;
                }
                else
                {
                    int quad = model.PlayerShips.Count(s => s.Type == ShipType.Quad);
                    int triple = model.PlayerShips.Count(s => s.Type == ShipType.Triple);
                    int dbl = model.PlayerShips.Count(s => s.Type == ShipType.Double);

                    ShipType next = ShipType.Single;
                    if (quad < 1) next = ShipType.Quad;
                    else if (triple < 2) next = ShipType.Triple;
                    else if (dbl < 3) next = ShipType.Double;

                    model.CurrentPlacingShip = new Ship { Type = next };
                }

                return true;
            }

            return false;
        }

        // =================== ВЫСТРЕЛ ИГРОКА ===================

        public void Shoot(GameModel model, int x, int y)
        {
            if (model.IsSetupPhase || !model.IsPlayerTurn || model.GameOver)
                return;

            if (model.PlayerFog[x, y] != CellState.Empty)
                return;

            bool hit = model.ComputerField[x, y] == CellState.Ship;
            model.PlayerFog[x, y] = hit ? CellState.Hit : CellState.Miss;

            if (hit)
            {
                var ship = model.ComputerShips.First(s => ShipContains(s, x, y));
                ship.Hits++;

                if (ship.IsKilled)
                {
                    MarkKilled(model.PlayerFog, ship);

                    if (model.ComputerShips.All(s => s.IsKilled))
                    {
                        model.GameOver = true;
                        model.PlayerWon = true;
                    }
                }

                // ПОПАДАНИЕ = ХОД ОСТАЁТСЯ У ИГРОКА
            }
            else
            {
                // ПРОМАХ = ХОД КОМПЬЮТЕРА
                model.IsPlayerTurn = false;
                ComputerTurn(model);
            }
        }

        // =================== ХОД КОМПЬЮТЕРА ===================

        private void ComputerTurn(GameModel model)
        {
            if (model.GameOver || model.IsPlayerTurn)
                return;

            bool shotDone = false;

            // 1. Сначала пытаемся добить корабль
            for (int x = 0; x < 10 && !shotDone; x++)
            {
                for (int y = 0; y < 10 && !shotDone; y++)
                {
                    if (model.ComputerFog[x, y] == CellState.Hit)
                    {
                        shotDone = TryFinishShip(model, x, y);
                    }
                }
            }

            // 2. Если не получилось — стреляем случайно
            if (!shotDone)
            {
                int rx, ry;

                do
                {
                    rx = _rnd.Next(10);
                    ry = _rnd.Next(10);
                }
                while (model.ComputerFog[rx, ry] != CellState.Empty);

                ShootAtPlayer(model, rx, ry);
            }

            // ВСЕГДА возвращаем ход игроку
            model.IsPlayerTurn = true;
        }


        private bool TryFinishShip(GameModel model, int x, int y)
        {
            int[,] directions =
            {
        { 1, 0 },
        { -1, 0 },
        { 0, 1 },
        { 0, -1 }
    };

            for (int i = 0; i < 4; i++)
            {
                int nx = x + directions[i, 0];
                int ny = y + directions[i, 1];

                if (nx < 0 || nx >= 10 || ny < 0 || ny >= 10)
                    continue;

                if (model.ComputerFog[nx, ny] == CellState.Empty)
                {
                    ShootAtPlayer(model, nx, ny);
                    return true;   
                }
            }

            return false; 
        }


        // =================== ВЫСТРЕЛ ПО ИГРОКУ ===================

        private void ShootAtPlayer(GameModel model, int x, int y)
        {
            bool hit = model.PlayerField[x, y] == CellState.Ship;
            model.ComputerFog[x, y] = hit ? CellState.Hit : CellState.Miss;

            if (hit)
            {
                var ship = model.PlayerShips.First(s => ShipContains(s, x, y));
                ship.Hits++;

                if (ship.IsKilled)
                {
                    MarkKilled(model.ComputerFog, ship);

                    if (model.PlayerShips.All(s => s.IsKilled))
                    {
                        model.GameOver = true;
                        model.PlayerWon = false;
                    }
                }
            }
        }

        // =================== ВСПОМОГАТЕЛЬНОЕ ===================

        private bool CanPlace(CellState[,] field, int x, int y, ShipType type, bool horizontal)
        {
            int len = (int)type;

            for (int i = 0; i < len; i++)
            {
                int cx = horizontal ? x + i : x;
                int cy = horizontal ? y : y + i;

                if (cx < 0 || cy < 0 || cx >= 10 || cy >= 10)
                    return false;

                if (field[cx, cy] != CellState.Empty)
                    return false;

                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = cx + dx;
                        int ny = cy + dy;

                        if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10)
                            if (field[nx, ny] == CellState.Ship)
                                return false;
                    }
            }

            return true;
        }

        private void PlaceShip(CellState[,] field, int x, int y, ShipType type, bool horizontal)
        {
            int len = (int)type;

            for (int i = 0; i < len; i++)
            {
                int cx = horizontal ? x + i : x;
                int cy = horizontal ? y : y + i;
                field[cx, cy] = CellState.Ship;
            }
        }

        private bool ShipContains(Ship ship, int x, int y)
        {
            if (ship.IsHorizontal)
                return y == ship.Y && x >= ship.X && x < ship.X + (int)ship.Type;

            return x == ship.X && y >= ship.Y && y < ship.Y + (int)ship.Type;
        }

        private void MarkKilled(CellState[,] field, Ship ship)
        {
            int len = (int)ship.Type;

            for (int i = 0; i < len; i++)
            {
                int cx = ship.IsHorizontal ? ship.X + i : ship.X;
                int cy = ship.IsHorizontal ? ship.Y : ship.Y + i;
                field[cx, cy] = CellState.Killed;
            }
        }
    }
}
