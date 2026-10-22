using System;
using System.Collections.Generic;

namespace Game2048.Business
{

    public class GameService
    {
        public const int WinValue = 2048;
        private readonly Random _random = new Random();

        public GameModel Initialize()
        {
            var game = new GameModel();
            SpawnTile(game);
            SpawnTile(game);
            return game;
        }

        public bool MakeMove(GameModel game, MoveDirection direction)
        {
            bool changed = false;
            int gained = 0;
            int n = GameModel.Size;

            if (direction == MoveDirection.Left || direction == MoveDirection.Right)
            {
                for (int r = 0; r < n; r++)
                {
                    var line = new int[n];
                    for (int c = 0; c < n; c++) line[c] = game.Field[r, c];

                    if (direction == MoveDirection.Right) Array.Reverse(line);

                    int g;
                    var slid = SlideLeft(line, out g);
                    gained += g;

                    if (direction == MoveDirection.Right) Array.Reverse(slid);

                    for (int c = 0; c < n; c++)
                    {
                        if (game.Field[r, c] != slid[c]) changed = true;
                        game.Field[r, c] = slid[c];
                    }
                }
            }
            else
            {
                for (int c = 0; c < n; c++)
                {
                    var line = new int[n];
                    for (int r = 0; r < n; r++) line[r] = game.Field[r, c];

                    if (direction == MoveDirection.Down) Array.Reverse(line);

                    int g;
                    var slid = SlideLeft(line, out g);
                    gained += g;

                    if (direction == MoveDirection.Down) Array.Reverse(slid);

                    for (int r = 0; r < n; r++)
                    {
                        if (game.Field[r, c] != slid[r]) changed = true;
                        game.Field[r, c] = slid[r];
                    }
                }
            }

            if (changed)
            {
                game.Score += gained;
                game.MoveCount++;
                SpawnTile(game);
            }

            return changed;
        }

        public bool IsWon(GameModel game)
        {
            foreach (var value in game.Field)
                if (value >= WinValue) return true;
            return false;
        }

        public bool IsGameOver(GameModel game)
        {
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (game.Field[r, c] == 0) return false;
                    if (c + 1 < n && game.Field[r, c] == game.Field[r, c + 1]) return false;
                    if (r + 1 < n && game.Field[r, c] == game.Field[r + 1, c]) return false;
                }
            }
            return true;
        }

        private int[] SlideLeft(int[] line, out int gained)
        {
            gained = 0;
            int n = line.Length;
            var result = new int[n];
            int index = 0;
            int pending = 0;

            for (int i = 0; i < n; i++)
            {
                int v = line[i];
                if (v == 0) continue;

                if (pending == 0)
                {
                    pending = v;
                }
                else if (pending == v)
                {
                    int merged = pending * 2;
                    result[index++] = merged;
                    gained += merged;
                    pending = 0;
                }
                else
                {
                    result[index++] = pending;
                    pending = v;
                }
            }

            if (pending != 0)
                result[index++] = pending;

            return result;
        }

        private void SpawnTile(GameModel game)
        {
            int n = GameModel.Size;
            var empties = new List<int>();

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (game.Field[r, c] == 0) empties.Add(r * n + c);

            if (empties.Count == 0) return;

            int pick = empties[_random.Next(empties.Count)];
            int row = pick / n;
            int col = pick % n;
            game.Field[row, col] = _random.Next(10) == 0 ? 4 : 2;
        }
    }
}
