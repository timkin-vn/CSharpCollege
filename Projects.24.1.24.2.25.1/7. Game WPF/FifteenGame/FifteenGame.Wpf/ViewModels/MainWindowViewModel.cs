using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service;
        private GameModel _model;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel() : this(null)
        {
        }

        public MainWindowViewModel(GameService service)
        {
            _service = service ?? new GameService();
            _model = new GameModel();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    Cells.Add(new CellViewModel { Row = row, Column = col, Direction = MoveDirection.Empty });
                }
            }

            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            LoadViewModel(_model);
        }

        public void FireAt(int row, int column, System.Action gameFinishedAction)
        {
            _service.MakeMove(_model, row, column);
            LoadViewModel(_model);

            if (_service.IsGameOver(_model))
            {
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            foreach (var cellVm in Cells)
            {
                var direction = model[cellVm.Row, cellVm.Column];
                cellVm.Direction = direction;
            }
        }
    }
}

