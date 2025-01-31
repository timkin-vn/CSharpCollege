using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetHello.Controllers
{
    public class GreetingController : Controller
    {
        // GET: Greeting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hello()
        {
            ViewBag.Greeting = "Привет!";
            return View("Index");
        }
    }
}