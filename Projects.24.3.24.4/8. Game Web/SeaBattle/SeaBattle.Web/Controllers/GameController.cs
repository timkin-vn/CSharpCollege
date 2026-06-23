using System.Web.Mvc;
using SeaBattle.Business;
using SeaBattle.Web.Models;

namespace SeaBattle.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();
        private const string SessionKey = "battleship";

        private BattleshipGame Current
        {
            get { return Session[SessionKey] as BattleshipGame; }
            set { Session[SessionKey] = value; }
        }

        public ActionResult Index()
        {
            var game = Current;
            if (game == null)
            {
                game = _service.NewGame();
                Current = game;
            }
            return View(BuildViewModel(game));
        }

        public ActionResult New()
        {
            Current = _service.NewGame();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Fire(string cell)
        {
            var game = Current;
            if (game != null && !string.IsNullOrEmpty(cell))
            {
                var parts = cell.Split('_');
                int row, col;
                if (parts.Length == 2 && int.TryParse(parts[0], out row) && int.TryParse(parts[1], out col))
                {
                    _service.Fire(game, row, col);
                    Current = game;
                }
            }
            return RedirectToAction("Index");
        }

        private GameViewModel BuildViewModel(BattleshipGame game)
        {
            var enemy = new CellState[Board.Size, Board.Size];
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                {
                    var s = game.EnemyBoard.Cells[r, c];
                    enemy[r, c] = s == CellState.Ship ? CellState.Empty : s;
                }

            string status;
            if (game.Finished)
                status = game.Won ? "Победа! Флот противника потоплен." : "Поражение. Ваш флот уничтожен.";
            else
                status = "Стреляйте по полю противника";

            return new GameViewModel
            {
                PlayerCells = game.PlayerBoard.Cells,
                EnemyCells = enemy,
                MoveCount = game.MoveCount,
                Status = status,
                Finished = game.Finished
            };
        }
    }
}
