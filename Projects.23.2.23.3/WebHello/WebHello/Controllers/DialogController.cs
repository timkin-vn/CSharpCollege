using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHello.Models;

namespace WebHello.Controllers
{
    public class DialogController : Controller
    {
        // GET: Dialog
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DialogViewModel model)
        {
            return View("Greeting", model);
        }
    }
}