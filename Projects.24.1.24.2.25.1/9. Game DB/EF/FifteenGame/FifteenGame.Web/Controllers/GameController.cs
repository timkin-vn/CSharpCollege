//using FifteenGame.Business.Models;
//using FifteenGame.Business.Services;
//using FifteenGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CheckersGame.Business.Models;
using CheckersGame.Business.Services;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService = new GameService();
        private const string GameSessionKey = "CheckersGameModel";
        private const string SelectedPieceKey = "SelectedPiece";

        private GameModel GetModel()
        {
            var model = Session[GameSessionKey] as GameModel;
            if (model == null)
            {
                model = new GameModel();
                _gameService.Initialize(model);
                Session[GameSessionKey] = model;
            }
            return model;
        }

        private (int row, int col)? GetSelectedPiece()
        {
            return Session[SelectedPieceKey] as (int row, int col)?;
        }

        private void SetSelectedPiece((int row, int col)? piece)
        {
            Session[SelectedPieceKey] = piece;
        }

        public ActionResult Index()
        {
            SetSelectedPiece(null);
            return View(GetModel());
        }

        public ActionResult Select(int row, int col)
        {
            var model = GetModel();
            var moves = _gameService.GetAvailableMoves(model, row, col);
            if (moves.Count > 0)
            {
                SetSelectedPiece((row, col));
            }
            else
            {
                SetSelectedPiece(null);
            }
            return View("Index", model);
        }

        public ActionResult Move(int toRow, int toCol)
        {
            var model = GetModel();
            var selected = GetSelectedPiece();
            if (selected == null)
                return RedirectToAction("Index");

            bool success = _gameService.MakeMove(model, selected.Value.row, selected.Value.col, toRow, toCol);
            SetSelectedPiece(null);
            if (success)
            {
                ViewBag.Message = model.IsGameOver() ? $"Победитель: {model.Winner}" : null;
            }
            return View("Index", model);
        }

        public ActionResult FinishCapture()
        {
            var model = GetModel();
            _gameService.FinishCapture(model);
            SetSelectedPiece(null);
            return View("Index", model);
        }
    }
}