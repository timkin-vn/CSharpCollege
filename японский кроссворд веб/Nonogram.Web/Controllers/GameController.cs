using Nonogram.Business.Web.Models;
using Nonogram.Business.Web.Services;
using NonogramWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NonogramWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly NonogramService _service = new NonogramService();
        private const string ModelSessionKey = "NonogramModel";

        public ActionResult Index()
        {
            try
            {
                InitializeGame();
                var businessModel = GetCurrentBusinessModel();

                if (businessModel == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                var model = CreateGameModel(businessModel);

                if (model == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка в Index: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult MakeMove(int row, int column)
        {
            try
            {
                var businessModel = GetCurrentBusinessModel();

                if (businessModel == null)
                {
                    return Json(new MoveResultModel
                    {
                        Success = false,
                        ErrorMessage = "Игра не инициализирована"
                    });
                }

                // Делаем ход (всегда пытаемся закрасить)
                var businessMoveResult = _service.MakeMove(businessModel, row, column, true);
                SaveBusinessModel(businessModel);

                // Получаем состояние клетки из бизнес-модели
                string cellStateStr = ConvertBusinessCellStateToString(businessModel[row, column]);

                // Проверяем завершенные строки и столбцы
                var completedRows = new List<int>();
                var completedColumns = new List<int>();

                if (businessMoveResult == Nonogram.Business.Web.Services.MoveResult.Correct)
                {
                    if (_service.CheckRowComplete(businessModel, row))
                    {
                        completedRows.Add(row);
                    }

                    if (_service.CheckColumnComplete(businessModel, column))
                    {
                        completedColumns.Add(column);
                    }
                }

                return Json(new MoveResultModel
                {
                    Success = true,
                    CellState = cellStateStr,
                    MistakesCount = businessModel.MistakesCount,
                    IsGameOver = _service.IsGameOver(businessModel),
                    IsGameWon = _service.IsGameWon(businessModel),
                    CompletedRows = completedRows,
                    CompletedColumns = completedColumns
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MoveResultModel
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewGame()
        {
            try
            {
                InitializeGame();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reset()
        {
            try
            {
                var businessModel = GetCurrentBusinessModel();
                if (businessModel != null)
                {
                    // Создаем новую модель и снова инициализируем
                    var newModel = new NonogramModel();
                    _service.Initialize(newModel);
                    SaveBusinessModel(newModel);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private void InitializeGame()
        {
            var model = new NonogramModel();
            _service.Initialize(model);
            SaveBusinessModel(model);
        }

        private NonogramModel GetCurrentBusinessModel()
        {
            if (Session[ModelSessionKey] is NonogramModel model)
            {
                return model;
            }

            InitializeGame();
            return Session[ModelSessionKey] as NonogramModel;
        }

        private void SaveBusinessModel(NonogramModel model)
        {
            Session[ModelSessionKey] = model;
        }

        private GameModel CreateGameModel(NonogramModel businessModel)
        {
            if (businessModel == null)
            {
                return null;
            }

            try
            {
                var model = new GameModel
                {
                    Cells = new List<CellModel>(),
                    RowClues = new List<RowClueModel>(),
                    ColumnClues = new List<ColumnClueModel>(),
                    MistakesCount = businessModel.MistakesCount,
                    Size = NonogramModel.Size
                };

                // Заполняем клетки
                for (int row = 0; row < NonogramModel.Size; row++)
                {
                    for (int col = 0; col < NonogramModel.Size; col++)
                    {
                        int cellValue = businessModel[row, col];
                        GameCellState cellState;

                        if (cellValue == NonogramModel.FilledCell)
                            cellState = GameCellState.Filled;
                        else if (cellValue == NonogramModel.CrossedCell)
                            cellState = GameCellState.Crossed;
                        else
                            cellState = GameCellState.Empty;

                        model.Cells.Add(new CellModel
                        {
                            Row = row,
                            Column = col,
                            State = cellState
                        });
                    }
                }

                // Заполняем подсказки для строк
                if (businessModel.RowClues != null)
                {
                    for (int row = 0; row < businessModel.RowClues.Count && row < NonogramModel.Size; row++)
                    {
                        var rowClue = businessModel.RowClues[row];
                        model.RowClues.Add(new RowClueModel
                        {
                            Index = row,
                            Values = rowClue ?? new List<int>(),
                            IsCompleted = _service.CheckRowComplete(businessModel, row)
                        });
                    }
                }

                // Заполняем подсказки для столбцов
                if (businessModel.ColumnClues != null)
                {
                    for (int col = 0; col < businessModel.ColumnClues.Count && col < NonogramModel.Size; col++)
                    {
                        var colClue = businessModel.ColumnClues[col];
                        model.ColumnClues.Add(new ColumnClueModel
                        {
                            Index = col,
                            Values = colClue ?? new List<int>(),
                            IsCompleted = _service.CheckColumnComplete(businessModel, col)
                        });
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка в CreateGameModel: {ex.Message}");
                return null;
            }
        }

        private string ConvertBusinessCellStateToString(int cellState)
        {
            if (cellState == NonogramModel.FilledCell)
                return "Filled";
            if (cellState == NonogramModel.CrossedCell)
                return "Crossed";
            return "Empty";
        }
    }
}