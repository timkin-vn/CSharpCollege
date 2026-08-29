using Microsoft.AspNetCore.Mvc;
using Minesweeper.Web.Models;
using Minesweeper.Web.Services;

namespace Minesweeper.Web.Controllers;

public sealed class GameController(GameService service) : Controller {
    public IActionResult Index() => View(service.GetView());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult NewGame(DifficultyRequest request) {
        service.StartNewGame(request);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Action(GameActionRequest request) {
        service.ApplyAction(request);
        return RedirectToAction(nameof(Index));
    }
}
