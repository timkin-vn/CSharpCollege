using Minesweeper.Business.Models;
using Minesweeper.Business.Services;
using MinesweeperWeb.Models;

namespace MinesweeperWeb.Services
{
    public class MinesweeperService
    {
        private readonly GameService _gameService;

        public MinesweeperService()
        {
            _gameService = new GameService();
        }

        public GameModel CreateNewGame(int rows = 10, int cols = 10, int mines = 15)
        {
            var model = new GameModel(rows, cols, mines);
            _gameService.InitializeGame(model);
            return model;
        }

        public GameViewModel ToViewModel(GameModel model)
        {
            var viewModel = new GameViewModel
            {
                RowCount = model.RowCount,
                ColumnCount = model.ColumnCount,
                MineCount = model.MineCount,
                FlagsPlaced = model.FlagsPlaced,
                State = model.State,
                Cells = new CellViewModel[model.RowCount, model.ColumnCount]
            };

            for (int row = 0; row < model.RowCount; row++)
            {
                for (int col = 0; col < model.ColumnCount; col++)
                {
                    var cell = model[row, col];
                    viewModel.Cells[row, col] = new CellViewModel
                    {
                        Row = cell.Row,
                        Column = cell.Column,
                        IsMine = cell.IsMine,
                        IsRevealed = cell.IsRevealed,
                        IsFlagged = cell.IsFlagged,
                        AdjacentMinesCount = cell.AdjacentMinesCount
                    };
                }
            }

            return viewModel;
        }

        public void HandleClick(GameModel model, int row, int col)
        {
            _gameService.RevealCell(model, row, col);
        }

        public void HandleFlag(GameModel model, int row, int col)
        {
            _gameService.ToggleFlag(model, row, col);
        }
    }
}