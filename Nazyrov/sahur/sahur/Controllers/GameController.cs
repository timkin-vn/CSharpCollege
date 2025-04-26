using Microsoft.AspNetCore.Mvc;

namespace gg_web.Controllers
{
    public class GameController : Controller
    {
        private const int BoardSize = 3;

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}

        