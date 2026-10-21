using MemoryGame.Business.Models;
using MemoryGame.Business.Services;
using System;
using System.Collections.ObjectModel;

namespace MemoryGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameService _service = new GameService();

        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int MoveCount => _model.MoveCount;

        public string PairsText => $"{_model.MatchedPairCount} из {GameModel.PairCount}";

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Shuffle(_model);
            LoadViewModel(_model);
        }

        public void MakeMove(int row, int column, Action gameFinishedAction)
        {
            if (!_service.MakeMove(_model, row, column))
            {
                return;
            }

            LoadViewModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
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
                        Value = model[row, column],
                        State = model.GetState(row, column),
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(PairsText));
        }
    }
}
