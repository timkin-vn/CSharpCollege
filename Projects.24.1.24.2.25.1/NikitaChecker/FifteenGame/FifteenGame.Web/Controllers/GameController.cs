using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Checkers.Business.Models;
using FifteenGame.Web.Models;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            var model = GetGame();
            if(model.Board == null)
            {
                InitializeGame(model);
                SaveGame(model);

            }
            return View(ToViewModel(model));
        }
        [HttpPost]
        public ActionResult MakeMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            var model = GetGame();
            if (MakeMoveInternal(model, fromRow, fromCol, toRow, toCol))
                SaveGame(model);
            else
                TempData["ErrorMessage"] = "Неверный ход!";

            return View("Index", ToViewModel(model));
        }

        private GameViewModel ToViewModel(Game model)
        {
            var vm = new GameViewModel
            {
                CurrentPlayer = model.CurrentPlayer.ToString(),
                IsGameOver = model.IsGameOver,
                Winner = model.Winner.ToString(),
                Cells = new CellViewModel[8, 8]
            };
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    var c = model.Board.GetChecker(i, j);
                    vm.Cells[i, j] = new CellViewModel
                    {
                        HasChecker = c != null,
                        CheckerColor = c?.Color.ToString(),
                        IsKing = c?.IsQueen ?? false,
                        IsValidFrom = model.CurrentMoves.Any(m => m.fromRow == i && m.fromCol == j),
                        IsValidTo = model.CurrentMoves.Any(m => m.toRow == i && m.toCol == j),
                    };
                }
            }
            return vm;
        }

        [HttpPost]
        public ActionResult Restart()
        {
            var model = GetGame();
            InitializeGame(model);
            SaveGame(model);
            return RedirectToAction("Index");
        }

        private void InitializeGame(Game model)
        {
            model.Board = new Board();
            model.CurrentPlayer = CheckerColor.White;
            model.Winner = CheckerColor.None;
            model.IsGameOver = false;
            UpdateValidMoves(model);
        }

        private bool MakeMoveInternal(Game model, int fromRow, int fromCol, int toRow, int toCol)
        {
            if (model.IsGameOver) return false;

            if (!model.CurrentMoves.Any(m => m.fromRow == fromRow && m.fromCol == fromCol && m.toRow == toRow && m.toCol == toCol))
                return false;

            var checker = model.Board.GetChecker(fromRow, fromCol);
            if (checker == null || checker.Color != model.CurrentPlayer) return false;

            bool isJump = Math.Abs(toRow - fromRow) == 2;
            int midRow = (fromRow + toRow) / 2;
            int midCol = (fromCol + toCol) / 2;

            model.Board.SetChecker(toRow, toCol, checker);
            model.Board.RemoveChecker(fromRow, fromCol);

            if (isJump) model.Board.RemoveChecker(midRow, midCol);

            if ((checker.Color == CheckerColor.White && toRow == 7) || (checker.Color == CheckerColor.Black && toRow == 0))
                checker.IsQueen = true;

            if (isJump)
            {
                var additionalJumps = GetValidJumps(model, toRow, toCol);
                if (additionalJumps.Any())
                {
                    model.CurrentMoves = additionalJumps.Select(j => (toRow, toCol, j.toRow, j.toCol)).ToList();
                    SaveGame(model);
                    return true;
                }
            }

            model.CurrentPlayer = model.CurrentPlayer == CheckerColor.White ? CheckerColor.Black : CheckerColor.White;
            UpdateValidMoves(model);
            CheckWinner(model);

            return true;
        }

        private void UpdateValidMoves(Game model)
        {
            model.CurrentMoves.Clear();
            var checkers = model.Board.GetAllCheckers().Where(c => c.Color == model.CurrentPlayer).ToList();

            var jumps = new List<(int fromRow, int fromCol, int toRow, int toCol)>();
            foreach (var c in checkers)
                jumps.AddRange(GetValidJumps(model, c.Row, c.Col));

            if (jumps.Any())
            {
                model.CurrentMoves.AddRange(jumps);
                return;
            }

            foreach (var c in checkers)
                foreach (var m in GetValidMoves(model, c.Row, c.Col))
                    model.CurrentMoves.Add(m);
        }

        private List<(int fromRow, int fromCol, int toRow, int toCol)> GetValidMoves(Game model, int row, int col)
        {
            var moves = new List<(int fromRow, int fromCol, int toRow, int toCol)>();
            var checker = model.Board.GetChecker(row, col);
            if (checker == null) return moves;

            if (checker.IsQueen)
                return GetQueenMoves(model, row, col);

            int dir = checker.Color == CheckerColor.White ? 1 : -1;
            foreach (int dc in new[] { -1, 1 })
            {
                int newRow = row + dir, newCol = col + dc;
                if (model.Board.IsValidCell(newRow, newCol) && model.Board.GetChecker(newRow, newCol) == null)
                    moves.Add((row, col, newRow, newCol));
            }
            return moves;
        }

        private List<(int fromRow, int fromCol, int toRow, int toCol)> GetQueenMoves(Game model, int row, int col)
        {
            var moves = new List<(int fromRow, int fromCol, int toRow, int toCol)>();
            var dirs = new[] { new[] { -1, -1 }, new[] { -1, 1 }, new[] { 1, -1 }, new[] { 1, 1 } };

            foreach (var d in dirs)
                for (int step = 1; step < 8; step++)
                {
                    int r = row + d[0] * step, c = col + d[1] * step;
                    if (!model.Board.IsValidCell(r, c)) break;
                    if (model.Board.GetChecker(r, c) == null)
                        moves.Add((row, col, r, c));  // Используем кортеж
                    else break;
                }
            return moves;
        }

        private List<(int fromRow, int fromCol, int toRow, int toCol)> GetValidJumps(Game model, int row, int col)
        {
            var jumps = new List<(int fromRow, int fromCol, int toRow, int toCol)>();
            var checker = model.Board.GetChecker(row, col);
            if (checker == null) return jumps;

            var dirs = new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) };  // Используем кортежи

            foreach (var (dr, dc) in dirs)
            {
                int midRow = row + dr, midCol = col + dc;
                int newRow = row + dr * 2, newCol = col + dc * 2;

                if (model.Board.IsValidCell(newRow, newCol) && model.Board.IsValidCell(midRow, midCol))
                {
                    var mid = model.Board.GetChecker(midRow, midCol);
                    var target = model.Board.GetChecker(newRow, newCol);
                    if (mid != null && mid.Color != checker.Color && target == null)
                        jumps.Add((row, col, newRow, newCol));
                }
            }
            return jumps;
        }

        private void CheckWinner(Game model)
        {
            int white = model.Board.GetAllCheckers().Count(c => c.Color == CheckerColor.White);
            int black = model.Board.GetAllCheckers().Count(c => c.Color == CheckerColor.Black);

            if (white == 0) { model.Winner = CheckerColor.Black; model.IsGameOver = true; }
            else if (black == 0) { model.Winner = CheckerColor.White; model.IsGameOver = true; }
            else if (model.CurrentMoves.Count == 0) { model.Winner = model.CurrentPlayer == CheckerColor.White ? CheckerColor.Black : CheckerColor.White; model.IsGameOver = true; }
        }
        private Game GetGame() => Session["Game"] as Game ?? new Game();
        private void SaveGame(Game game) => Session["Game"] = game;
    }
}
