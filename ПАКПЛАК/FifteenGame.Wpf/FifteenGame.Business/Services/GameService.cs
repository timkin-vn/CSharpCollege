using Pacmen.Business.Models;
using System;

namespace Pacmen.Business.Services
{
    public class GameService
    {
        private readonly GameState _gameState;
        private readonly Random _random;

        public GameService(GameState gameState)
        {
            _gameState = gameState;
            _random = new Random();
        }

        public void MovePacman(Direction direction)
        {
            if (_gameState.IsGameOver) return;

            var (newX, newY) = GetNewPosition(_gameState.PacmanPosition, direction);
            if (IsValidMove(newX, newY))
            {
                if (_gameState.Maze[newX, newY] == 2) // Монетка
                {
                    _gameState.Score += 10;
                    _gameState.Maze[newX, newY] = 1; // Убираем монетку
                    if (_gameState.GetCoinCount() == 0)
                    {
                        _gameState.IsGameOver = true;
                        _gameState.HasWon = true;
                    }
                }
                _gameState.PacmanPosition = (newX, newY);
            }
        }

        public void MoveGhosts()
        {
            if (_gameState.IsGameOver) return;

            for (int i = 0; i < _gameState.GhostPositions.Count; i++)
            {
                var ghost = _gameState.GhostPositions[i];
                var directions = new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
                var direction = directions[_random.Next(directions.Length)];
                var (newX, newY) = GetNewPosition(ghost, direction);
                if (IsValidMove(newX, newY))
                {
                    _gameState.GhostPositions[i] = (newX, newY);
                    if (newX == _gameState.PacmanPosition.X && newY == _gameState.PacmanPosition.Y)
                    {
                        _gameState.IsGameOver = true;
                        _gameState.HasWon = false;
                    }
                }
            }
        }

        private (int X, int Y) GetNewPosition((int X, int Y) position, Direction direction)
        {
            var (x, y) = position;
            switch (direction)
            {
                case Direction.Up: return (x - 1, y);
                case Direction.Down: return (x + 1, y);
                case Direction.Left: return (x, y - 1);
                case Direction.Right: return (x, y + 1);
                default: return (x, y);
            }
        }

        private bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < _gameState.Maze.GetLength(0) && y >= 0 && y < _gameState.Maze.GetLength(1) && _gameState.Maze[x, y] != 0;
        }
    }
}