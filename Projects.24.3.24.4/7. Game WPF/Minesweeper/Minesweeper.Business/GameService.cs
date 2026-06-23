using System;
using System.Collections.Generic;

namespace Minesweeper.Business
{

    public class GameService
    {
        private readonly Random _random = new Random();

        public GameModel Initialize()
        {
            return new GameModel();
        }

        public bool Apply(GameModel game, int row, int col, CellAction action)
        {
            if (action == CellAction.Flag)
                return ToggleFlag(game, row, col);
            return Reveal(game, row, col);
        }

        public bool Reveal(GameModel game, int row, int col)
        {
            if (game.IsLost || IsWon(game)) return false;
            if (!InBounds(row, col)) return false;

            var start = game.Field[row, col];
            if (start.IsRevealed || start.IsFlagged) return false;

            if (!game.MinesPlaced)
            {
                PlaceMines(game, row, col);
                game.MinesPlaced = true;
            }

            game.MoveCount++;

            if (start.IsMine)
            {
                start.IsRevealed = true;
                game.IsLost = true;
                RevealAllMines(game);
                return true;
            }

            FloodReveal(game, row, col);
            return true;
        }

        public bool ToggleFlag(GameModel game, int row, int col)
        {
            if (game.IsLost || IsWon(game)) return false;
            if (!InBounds(row, col)) return false;

            var cell = game.Field[row, col];
            if (cell.IsRevealed) return false;

            cell.IsFlagged = !cell.IsFlagged;
            game.MoveCount++;
            return true;
        }

        public bool IsWon(GameModel game)
        {
            if (game.IsLost) return false;
            int revealedSafe = 0;
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                {
                    var cell = game.Field[r, c];
                    if (!cell.IsMine && cell.IsRevealed) revealedSafe++;
                }
            return revealedSafe == n * n - GameModel.MineCount;
        }

        public bool IsGameOver(GameModel game)
        {
            return game.IsLost;
        }

        public int MinesRemaining(GameModel game)
        {
            int flags = 0;
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (game.Field[r, c].IsFlagged) flags++;
            return GameModel.MineCount - flags;
        }

        private void PlaceMines(GameModel game, int safeRow, int safeCol)
        {
            int n = GameModel.Size;
            var candidates = new List<int>();
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (r != safeRow || c != safeCol) candidates.Add(r * n + c);

            int placed = 0;
            while (placed < GameModel.MineCount && candidates.Count > 0)
            {
                int pickIndex = _random.Next(candidates.Count);
                int pick = candidates[pickIndex];
                candidates.RemoveAt(pickIndex);
                game.Field[pick / n, pick % n].IsMine = true;
                placed++;
            }

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    game.Field[r, c].AdjacentMines = CountAdjacentMines(game, r, c);
        }

        private static int CountAdjacentMines(GameModel game, int row, int col)
        {
            int count = 0;
            for (int dr = -1; dr <= 1; dr++)
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    int nr = row + dr;
                    int nc = col + dc;
                    if (InBounds(nr, nc) && game.Field[nr, nc].IsMine) count++;
                }
            return count;
        }

        private static void FloodReveal(GameModel game, int row, int col)
        {
            var stack = new Stack<int>();
            int n = GameModel.Size;
            stack.Push(row * n + col);

            while (stack.Count > 0)
            {
                int current = stack.Pop();
                int r = current / n;
                int c = current % n;
                var cell = game.Field[r, c];

                if (cell.IsRevealed || cell.IsFlagged || cell.IsMine) continue;

                cell.IsRevealed = true;

                if (cell.AdjacentMines != 0) continue;

                for (int dr = -1; dr <= 1; dr++)
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0) continue;
                        int nr = r + dr;
                        int nc = c + dc;
                        if (InBounds(nr, nc))
                        {
                            var neighbour = game.Field[nr, nc];
                            if (!neighbour.IsRevealed && !neighbour.IsFlagged && !neighbour.IsMine)
                                stack.Push(nr * n + nc);
                        }
                    }
            }
        }

        private static void RevealAllMines(GameModel game)
        {
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (game.Field[r, c].IsMine) game.Field[r, c].IsRevealed = true;
        }

        private static bool InBounds(int row, int col)
        {
            return row >= 0 && row < GameModel.Size && col >= 0 && col < GameModel.Size;
        }
    }
}
