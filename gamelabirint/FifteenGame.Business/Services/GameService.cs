using System;
using System.Linq;
using System.Text;
using FifteenGame.Business.Models;
using FifteenGame.Data;
using FifteenGame.Data.Entities;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private GameField _field;
        private Random _rnd = new Random();

        public GameField GetField() => _field;
        public int MovesCount { get; private set; }

        public GameService()
        {

            _field = new GameField(15, 15);
        }


        public void StartNewGame()
        {
            _field = new GameField(15, 15);
            MovesCount = 0;
            GenerateMaze();
        }

        //  Метод СОХРАНЕНИЯ в Базу
        public void SaveGame(int userId, string boardData, int score)
        {
            using (var context = new MazeDbContext())
            {

                var state = context.GameStates.FirstOrDefault(g => g.UserId == userId);

                if (state == null)
                {
                    state = new GameState { UserId = userId };
                    context.GameStates.Add(state);
                }


                state.BoardData = boardData;
                state.Score = score;

                context.SaveChanges();
            }
        }


        public GameState LoadGame(int userId)
        {
            using (var context = new MazeDbContext())
            {
                return context.GameStates.FirstOrDefault(g => g.UserId == userId);
            }
        }


        public void RestoreGame(string boardData, int score)
        {
            _field = new GameField(15, 15);
            MovesCount = score;


            string[] cells = boardData.Split(',');

 
            if (cells.Length < _field.Width * _field.Height)
            {
                StartNewGame();
                return;
            }

            int index = 0;
            for (int y = 0; y < _field.Height; y++)
            {
                for (int x = 0; x < _field.Width; x++)
                {

                    if (int.TryParse(cells[index], out int typeVal))
                    {
                        var type = (CellType)typeVal;
                        _field.Grid[x, y] = type;


                        if (type == CellType.Player)
                        {
                            _field.PlayerX = x;
                            _field.PlayerY = y;
                        }
                    }
                    index++;
                }
            }
            _field.IsGameFinished = false;
        }



        private void GenerateMaze()
        {
            for (int x = 0; x < _field.Width; x++)
                for (int y = 0; y < _field.Height; y++)
                    _field.Grid[x, y] = CellType.Wall;

            CarvePassage(1, 1);

            _field.PlayerX = 1;
            _field.PlayerY = 1;
            _field.Grid[1, 1] = CellType.Player;

            int exitX = _field.Width - 2;
            int exitY = _field.Height - 2;


            _field.Grid[exitX, exitY] = CellType.Exit;
            if (_field.Grid[exitX - 1, exitY] == CellType.Wall) _field.Grid[exitX - 1, exitY] = CellType.Empty;

            _field.IsGameFinished = false;
        }

        private void CarvePassage(int cx, int cy)
        {
            _field.Grid[cx, cy] = CellType.Empty;
            var directions = new[]
            {
                new { dx = 0, dy = -2 }, new { dx = 0, dy = 2 },
                new { dx = -2, dy = 0 }, new { dx = 2, dy = 0 }
            }.OrderBy(x => _rnd.Next()).ToArray();

            foreach (var dir in directions)
            {
                int nx = cx + dir.dx;
                int ny = cy + dir.dy;
                if (nx > 0 && nx < _field.Width - 1 && ny > 0 && ny < _field.Height - 1)
                {
                    if (_field.Grid[nx, ny] == CellType.Wall)
                    {
                        _field.Grid[cx + dir.dx / 2, cy + dir.dy / 2] = CellType.Empty;
                        CarvePassage(nx, ny);
                    }
                }
            }
        }

        public void Move(MoveDirection direction)
        {
            if (_field.IsGameFinished) return;
            int newX = _field.PlayerX;
            int newY = _field.PlayerY;

            switch (direction)
            {
                case MoveDirection.Up: newY--; break;
                case MoveDirection.Down: newY++; break;
                case MoveDirection.Left: newX--; break;
                case MoveDirection.Right: newX++; break;
            }

            if (_field.Grid[newX, newY] != CellType.Wall)
            {
                if (_field.Grid[newX, newY] == CellType.Exit) _field.IsGameFinished = true;


                _field.Grid[_field.PlayerX, _field.PlayerY] = CellType.Empty;


                _field.PlayerX = newX;
                _field.PlayerY = newY;

                if (!_field.IsGameFinished)
                    _field.Grid[newX, newY] = CellType.Player;

                MovesCount++;
            }
        }
    }
}