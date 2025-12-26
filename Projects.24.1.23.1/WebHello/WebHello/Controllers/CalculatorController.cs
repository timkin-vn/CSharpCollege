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
            return View(GetViewModel(GetModel()));
        }

        public ActionResult PressDigit(string digitString)
        {
            var state = GetModel();
            _service.PressDigit(state, digitString);
            SaveModel(state);

            return View("Index", GetViewModel(state));
        }

        public ActionResult PressClear()
        {
            var state = GetModel();
            _service.Clear(state);
            SaveModel(state);

            return View("Index", GetViewModel(state));
        }

        public ActionResult PressOperation(string opCode)
        {
            var state = GetModel();
            _service.PressOperation(state, opCode);
            SaveModel(state);

            return View("Index", GetViewModel(state));
        }

        private CalculatorViewModel GetViewModel(CalculatorState state)
        {
            return new CalculatorViewModel { DisplayValue = state.RegisterX.ToString(), };
        }

        private CalculatorState GetModel()
        {
            if (Session.IsNewSession)
            {
                var model = new CalculatorState();
                Session["CalculatorState"] = model;
            }

            return (CalculatorState)Session["CalculatorState"];
        }

        private void SaveModel(CalculatorState model)
        {
            Session["CalculatorState"] = model;
        }
    }
}