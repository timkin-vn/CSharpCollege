using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class TicTacToeController : Controller
    {
        private readonly TicTacToeService _ticTacToeService;

        public TicTacToeController(TicTacToeService ticTacToeService)
        {
            _ticTacToeService = ticTacToeService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
} 