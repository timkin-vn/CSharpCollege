using Microsoft.AspNetCore.Mvc;

namespace LightsOutGame.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Описание приложения.";
            return View();
        }
    }
}
