using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using FifteenGame.Wpf.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace FifteenGame.Web.Controllers
{                  
    public class GameController : Controller
    {
        private GameService _service = new GameService();
        

        public ActionResult Index()
        {
            var model = GetGameModel();
           
            _service.Initialize(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult PressButton(int colum,int row)
        {
           
            var model = GetGameModel();
            _service.Search_for_parents(model, colum , row);
            if (_service.IsGameOver(model))
            {
                ViewBag.IsGameOver = true;
            }



            SaveGameModel(model);
            

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            
            if (Session.IsNewSession)
            {
                Session["GameModel"] = new GameModel();
            }

            return (GameModel)Session["GameModel"];
        }

        private void SaveGameModel(GameModel model)
        {
           
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if ((model.OneRowCol[0] == row && 
                        model.OneRowCol[1] == column)
                        || (model.TwoRowCol[0] == row 
                        && model.TwoRowCol[1] == column))
                    {
                        result.Cells[row, column] = new CellViewModel
                        {
                            Text = model[row, column],
                            ColumnRow = new int[] { row, column },
                            Row = row,
                            Column = column,
                            IsFaceUp = true
                        };
                    }
                    else { 
                    result.Cells[row, column] = new CellViewModel
                    {
                        Text = model[row, column],
                        ColumnRow = new int[] { row, column },
                        Row = row,
                        Column = column,
                        IsFaceUp = false
                    };
                    }
                }
            }
            
            return result;
        }
    }
}