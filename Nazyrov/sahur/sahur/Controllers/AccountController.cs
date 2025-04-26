using Microsoft.AspNetCore.Mvc;
using gg_web_business.Services;
using gg_web_business.Models;
using System.Threading.Tasks;

namespace gg_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _authService.ValidateUserAsync(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Game");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
