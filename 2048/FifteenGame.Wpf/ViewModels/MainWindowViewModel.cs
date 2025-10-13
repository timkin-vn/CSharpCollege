using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel(_model);
            OnPropertyChanged(nameof(Score));
        }

        public int Score => _service.Score;

        public void MakeMove(MoveDirection dir, Action gameFinishAction)
        {
            if (_service.MakeMove(_model, dir))
            {
                FromModel(_model);
                OnPropertyChanged(nameof(Score));

                if (_model.HasWon() || !_model.HasMoves())
                {
                    gameFinishAction?.Invoke();
                }
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Num = model[r, c]
                    });
                }
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
