using Calculator.Business.Models;
using Calculator.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHello.Models;

namespace WebHello.Controllers
{
    public class CalculatorController : Controller
    {
        private CalculatorService _service = new CalculatorService();

        // GET: Calculator
        public ActionResult Index()
        {
            return View(ToViewModel(GetCalculatorState()));
        }

        public ActionResult PressDigit(string digitString)
        {
            var state = GetCalculatorState();
            _service.PressDigit(state, digitString);
            SaveCalculatorState(state);

            return View("Index", ToViewModel(state));
        }

        public ActionResult PressClear()
        {
            var state = GetCalculatorState();
            _service.Clear(state);
            SaveCalculatorState(state);

            return View("Index", ToViewModel(state));
        }

        public ActionResult PressOperation(string opCode)
        {
            var state = GetCalculatorState();
            _service.PressOperation(state, opCode);
            SaveCalculatorState(state);

            return View("Index", ToViewModel(state));
        }

        private CalculatorViewModel ToViewModel(CalculatorState state)
        {
            return new CalculatorViewModel { DisplayValue = state.RegisterX, };
        }

        private CalculatorState GetCalculatorState()
        {
            if (Session.IsNewSession)
            {
                Session["CalculatorState"] = new CalculatorState();
            }

            return (CalculatorState)Session["CalculatorState"];
        }

        private void SaveCalculatorState(CalculatorState state)
        {
            Session["CalculatorState"] = state;
        }
    }
}