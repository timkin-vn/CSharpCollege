using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service;
        private GameModel _model;
        public CellViewModel[,] CellsArray { get; set; }

        public GameController()
        {
            _service = new GameService();
            _model = new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None);
            CellsArray = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
        }

        
        public ActionResult Index()
        {
            var model = GetModel();
            _service.Shuffle(model);
            SaveModel(model);
            return View(FromModel(model));
        }

        public ActionResult PressButton(string directionText, int X, int Y)
        {
            var model = GetModel();

            if (Enum.TryParse<MoveDirection>(directionText, out var moveDirection))
            {
                
                if (_service.MakeMove(model, X, Y, model))
                {
                    SaveModel(model); 

                    if (_service.IsGameOver(model))
                    {
                        ViewBag.IsGameOver = true; 
                    }
                }
            }

            return View("Index", FromModel(model));
        }

        private GameModel GetModel()
        {
            if (Session.IsNewSession)
            {
                Session["GameModel"] = new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None);
            }

            return (GameModel)Session["GameModel"];
        }

        private void SaveModel(GameModel model)
        {
            Session["GameModel"] = model; 
        }

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public GameViewModel FromModel(GameModel model)
        {
            Cells.Clear(); 

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != null)
                    {
                        var direction = MoveDirection.None;

                        
                        if (row == model.FreeCellRow)
                        {
                            if (column == model.FreeCellColumn - 1)
                            {
                                direction = MoveDirection.XRight;
                            }
                            else if (column == model.FreeCellColumn + 1)
                            {
                                direction = MoveDirection.XLeft;
                            }
                        }
                        else if (column == model.FreeCellColumn)
                        {
                            if (row == model.FreeCellRow - 1)
                            {
                                direction = MoveDirection.YDown;
                            }
                            else if (row == model.FreeCellRow + 1)
                            {
                                direction = MoveDirection.YUp;
                            }
                        }
                        else if (Math.Abs(row - model.FreeCellRow) == 1 && Math.Abs(column - model.FreeCellColumn) == 1)
                        {
                            if (row < model.FreeCellRow && column < model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownRight;
                            }
                            else if (row < model.FreeCellRow && column > model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownLeft;
                            }
                            else if (row > model.FreeCellRow && column < model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpRight;
                            }
                            else if (row > model.FreeCellRow && column > model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpLeft;
                            }
                        }

                        
                        var cellViewModel = new CellViewModel(row, column, model[row, column])
                        {
                            Direction = direction,
                            Type = model[row, column].Type 
                        };
                        Cells.Add(cellViewModel);
                        CellsArray[row, column] = cellViewModel; 
                    }
                    else
                    {
                        var emptyCellViewModel = new CellViewModel(row, column, new GameModel(" ", 0, 0, row, column, GameModel.UnitType.None))
                        {
                            Direction = MoveDirection.None,
                            Type = GameModel.UnitType.None 
                        };
                        Cells.Add(emptyCellViewModel);
                        CellsArray[row, column] = emptyCellViewModel; 
                    }
                }
            }

            
            return new GameViewModel
            {
                Cells = CellsArray, 
            };
        }
    }
}
