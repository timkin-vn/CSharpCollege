using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetModel();
            return View(model);
        }

        public ActionResult PlaceShip(int x, int y)
        {
            var model = GetModel();
            if (model.IsSetupPhase)
                _service.TryPlacePlayerShip(model, x, y);

            SaveModel(model);
            return RedirectToAction("Index");
        }

        public ActionResult RotateShip()
        {
            var model = GetModel();
            if (model.IsSetupPhase)
                model.IsHorizontal = !model.IsHorizontal;

            SaveModel(model);
            return RedirectToAction("Index");
        }

        public ActionResult AutoPlace()
        {
            var model = GetModel();

            // Начать новую игру заново
            _service.NewGame(model);

            // Автоматическая расстановка кораблей игрока через GameService
            var shipsToPlace = new[]
            {
        ShipType.Quad,
        ShipType.Triple, ShipType.Triple,
        ShipType.Double, ShipType.Double, ShipType.Double,
        ShipType.Single, ShipType.Single, ShipType.Single, ShipType.Single
    };

            foreach (var type in shipsToPlace)
            {
                while (true)
                {
                    bool hor = new Random().Next(2) == 0;
                    int x = new Random().Next(10);
                    int y = new Random().Next(10);

                    if (_service.TryPlacePlayerShip(model, x, y)) 
                    {
                        break;
                    }
                }
            }

            
            model.IsSetupPhase = false;

            SaveModel(model);

            return RedirectToAction("Index");
        }


        public ActionResult Shoot(int x, int y)
        {
            var model = GetModel();

            if (model.IsSetupPhase || model.GameOver)
                return RedirectToAction("Index");

            // Игрок стреляет — внутри вызовется и ход компьютера 
            _service.Shoot(model, x, y);

            
            SaveModel(model);

            return RedirectToAction("Index");
        }

        public ActionResult NewGame()
        {
            Session.Remove("GameModel");
            return RedirectToAction("Index");
        }

        private GameModel GetModel()
        {
            var model = Session["GameModel"] as GameModel;

            if (model == null)
            {
                model = new GameModel();
                _service.NewGame(model);
                Session["GameModel"] = model;
            }

            return model;
        }

        private void SaveModel(GameModel model)
        {
            Session["GameModel"] = model;
        }
    }
}
