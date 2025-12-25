using System.Web.Mvc;

namespace NonogramWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Описание игры 'Японский кроссворд'";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контактная информация";
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Произошла ошибка при выполнении операции";
            return View();
        }
    }
}