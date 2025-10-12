using System;
using System.Collections.ObjectModel;
using LightsOut.Business.Models;
using LightsOut.Business.Service;

namespace LightsOut.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Shuffle(_model);
            FromModel(_model);
        }

        public void MakeMoveAt(int row, int column, Action gameFinishAction)
        {
            _service.MakeMoveAt(_model, row, column);
            FromModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishAction?.Invoke();
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        IsOn = model[row, column]
                    });
                }
            }
        }
    }
}