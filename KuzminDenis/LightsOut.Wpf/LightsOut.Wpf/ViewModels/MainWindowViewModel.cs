using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LightsOut.Business.Models;
using LightsOut.Business.Service;

namespace LightsOut.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        private int _maxMoves = 50;
        public int MaxMoves
        {
            get => _maxMoves;
            set { _maxMoves = value; OnPropertyChanged(); }
        }

        private int _movesLeft;
        public int MovesLeft
        {
            get => _movesLeft;
            set { _movesLeft = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Shuffle(_model);
            MovesLeft = MaxMoves;
            FromModel(_model);
        }

        public void MakeMoveAt(int row, int column, Action gameWinAction, Action gameLoseAction)
        {
            if (MovesLeft <= 0)
                return;

            _service.MakeMoveAt(_model, row, column);
            MovesLeft--;
            FromModel(_model);

            if (_service.IsGameOver(_model))
            {
                gameWinAction?.Invoke();
            }
            else if (MovesLeft == 0)
            {
                gameLoseAction?.Invoke();
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}