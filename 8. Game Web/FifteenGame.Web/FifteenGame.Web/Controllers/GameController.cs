using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKey = "GameModel_2048";
        private readonly GameService _service = new GameService();

        
        public ActionResult Index()
        {
            var model = GetGameModel();
            if (model == null)
            {
                model = new GameModel();
                _service.Initialize(model);
                SaveGameModel(model);
            }

            return View(ToViewModel(model));
        }

     
        [HttpPost]
        public ActionResult New()
        {
            var model = new GameModel();
            _service.Initialize(model);
            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        
        [HttpPost]
        public ActionResult Move(string directionText)
        {
            var model = GetGameModel();
            if (model == null)
            {
                model = new GameModel();
                _service.Initialize(model);
            }

            MoveDirection direction = MoveDirection.None;
            if (!string.IsNullOrEmpty(directionText))
            {
                MoveDirection parsed;
                if (Enum.TryParse(directionText, true, out parsed))
                {
                    direction = parsed;
                }
            }

            _service.MakeMove(model, direction);
            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            return Session[SessionKey] as GameModel;
        }

        private void SaveGameModel(GameModel model)
        {
            Session[SessionKey] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var vm = new GameViewModel();
            vm.Score = model.Score;
            vm.IsWin = model.IsWin;
            vm.IsLose = model.IsLose;

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    vm.Cells[r, c] = new CellViewModel
                    {
                        Value = model[r, c]
                    };
                }
            }

            return vm;
        }
    }
}