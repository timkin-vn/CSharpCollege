using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Business.Models
{
    public class Game
    {
        public Board Board { get; set; }
        public CheckerColor CurrentPlayer { get; set; }
        public CheckerColor Winner { get; set; }
        public bool IsGameOver { get; set; }
        public List<(int fromRow, int fromCol, int toRow, int toCol)> CurrentMoves { get; set; }
        public object Id { get; set; }

        public Game()
        {
            Board = new Board();
            CurrentPlayer = CheckerColor.White;
            Winner = CheckerColor.None;
            IsGameOver = false;
            CurrentMoves = new List<(int fromRow, int fromCol, int toRow, int toCol)>();
        }
        public bool MakeMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            if (IsGameOver) return false;

            var move = (fromRow, fromCol, toRow, toCol);
            if (!CurrentMoves.Contains(move))
                return false;

            var checker = Board.GetChecker(fromRow, fromCol);
            if (checker == null || checker.Color != CurrentPlayer)
                return false;

            bool isJump = Math.Abs(toRow - fromRow) == 2;
            int midRow = (fromRow + toRow) / 2;
            int midCol = (fromCol + toCol) / 2;

            // Выполняем ход
            Board.SetChecker(toRow, toCol, checker);
            Board.RemoveChecker(fromRow, fromCol);

            // Удаляем побитую шашку
            if (isJump)
            {
                Board.RemoveChecker(midRow, midCol);
            }

            // Превращение в дамку
            if ((checker.Color == CheckerColor.White && toRow == 7) ||
                (checker.Color == CheckerColor.Black && toRow == 0))
            {
                checker.IsQueen = true;
            }

            // Проверяем, можно ли сделать ещё один прыжок
            bool canContinueJump = false;
            if (isJump)
            {
                var additionalJumps = GetValidJumps(toRow, toCol);
                if (additionalJumps.Any())
                {
                    // Сохраняем только возможные прыжки с текущей позиции
                    CurrentMoves = additionalJumps.Select(j => (toRow, toCol, j.toRow, j.toCol)).ToList();
                    canContinueJump = true;
                }
            }

            if (!canContinueJump)
            {
                // Смена игрока
                CurrentPlayer = CurrentPlayer == CheckerColor.White ? CheckerColor.Black : CheckerColor.White;
                UpdateValidMoves();
            }

            // Проверка на победу
            CheckWinner();

            return true;
        }

        public void UpdateValidMoves()
        {
            CurrentMoves.Clear();
            var allCheckers = Board.GetAllCheckers()
                .Where(c => c.Color == CurrentPlayer)
                .ToList();

            // Сначала проверяем обязательные прыжки
            var allJumps = new List<(int, int, int, int)>();
            foreach (var checker in allCheckers)
            {
                var jumps = GetValidJumps(checker.Row, checker.Col);
                allJumps.AddRange(jumps.Select(j => (checker.Row, checker.Col, j.toRow, j.toCol)));
            }

            if (allJumps.Any())
            {
                CurrentMoves.AddRange(allJumps);
                return;
            }

            // Если прыжков нет, добавляем обычные ходы
            foreach (var checker in allCheckers)
            {
                var moves = GetValidMoves(checker.Row, checker.Col);
                CurrentMoves.AddRange(moves.Select(m => (checker.Row, checker.Col, m.toRow, m.toCol)));
            }
        }

        private List<(int toRow, int toCol)> GetValidMoves(int row, int col)
        {
            var moves = new List<(int, int)>();
            var checker = Board.GetChecker(row, col);
            if (checker == null) return moves;

            int direction = checker.Color == CheckerColor.White ? 1 : -1;

            // Дамка может двигаться в любом направлении
            if (checker.IsQueen)
            {
                moves.AddRange(GetQueenMoves(row, col));
            }
            else
            {
                // Обычные шашки
                if (checker.Color == CheckerColor.White)
                    direction = 1;
                else
                    direction = -1;

                // Движение вперёд
                foreach (int dCol in new[] { -1, 1 })
                {
                    int newRow = row + direction;
                    int newCol = col + dCol;

                    if (Board.IsValidCell(newRow, newCol) && Board.GetChecker(newRow, newCol) == null)
                    {
                        moves.Add((newRow, newCol));
                    }
                }
            }

            return moves;
        }

        private List<(int toRow, int toCol)> GetQueenMoves(int row, int col)
        {
            var moves = new List<(int, int)>();
            var directions = new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) };

            foreach (var (dr, dc) in directions)
            {
                for (int step = 1; step < Board.Size; step++)
                {
                    int newRow = row + dr * step;
                    int newCol = col + dc * step;

                    if (!Board.IsValidCell(newRow, newCol))
                        break;

                    var target = Board.GetChecker(newRow, newCol);
                    if (target == null)
                    {
                        moves.Add((newRow, newCol));
                    }
                    else
                    {
                        break; // Встретили шашку - дальше нельзя
                    }
                }
            }

            return moves;
        }

        private List<(int toRow, int toCol)> GetValidJumps(int row, int col)
        {
            var jumps = new List<(int, int)>();
            var checker = Board.GetChecker(row, col);
            if (checker == null) return jumps;

            var directions = new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) };

            foreach (var (dr, dc) in directions)
            {
                int midRow = row + dr;
                int midCol = col + dc;
                int newRow = row + dr * 2;
                int newCol = col + dc * 2;

                if (Board.IsValidCell(newRow, newCol) && Board.IsValidCell(midRow, midCol))
                {
                    var midChecker = Board.GetChecker(midRow, midCol);
                    var targetChecker = Board.GetChecker(newRow, newCol);

                    if (midChecker != null &&
                        midChecker.Color != checker.Color &&
                        targetChecker == null)
                    {
                        // Для дамки нужно учитывать расстояние больше 2
                        if (checker.IsQueen)
                        {
                            // Дополнительные прыжки для дамки можно добавить при необходимости
                            jumps.Add((newRow, newCol));
                        }
                        else if (Math.Abs(newRow - row) == 2)
                        {
                            jumps.Add((newRow, newCol));
                        }
                    }
                }
            }

            return jumps;
        }

        private void CheckWinner()
        {
            var whiteCount = Board.GetAllCheckers().Count(c => c.Color == CheckerColor.White);
            var blackCount = Board.GetAllCheckers().Count(c => c.Color == CheckerColor.Black);

            if (whiteCount == 0)
            {
                Winner = CheckerColor.Black;
                IsGameOver = true;
            }
            else if (blackCount == 0)
            {
                Winner = CheckerColor.White;
                IsGameOver = true;
            }
            else if (CurrentMoves.Count == 0)
            {
                // Нет доступных ходов - поражение
                Winner = CurrentPlayer == CheckerColor.White ? CheckerColor.Black : CheckerColor.White;
                IsGameOver = true;
            }
        }

        public void Restart()
        {
            Board = new Board();
            CurrentPlayer = CheckerColor.White;
            Winner = CheckerColor.None;
            IsGameOver = false;
            UpdateValidMoves();
        }
    }
}
