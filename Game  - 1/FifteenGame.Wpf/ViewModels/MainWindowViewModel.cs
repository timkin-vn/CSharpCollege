using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService = new GameService();
        private readonly GameModel _gameModel = new GameModel();

        private bool _openMode = true;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            SelectSize(9); 
        }

        public int GameSize => _gameModel.Size;

        private string _gameStatus = "";
        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public void SelectSize(int size)
        {
            _gameService.Initialize(_gameModel, size);
            UpdateCells();
        }

        public void ToggleMode(string mode)
        {
            _openMode = (mode == "open");
        }

        public void HandleCellClick(CellViewModel cellViewModel)
        {
            if (_openMode)
            {
                _gameService.OpenCell(_gameModel, cellViewModel.Row, cellViewModel.Column);

                if (_gameModel.GameOver)
                {
                    if (_gameModel.GameWon)
                    {
                        GameStatus = "Победа!";
                    }
                    else
                    {
                        GameStatus = "Проигрыш!";
                        _gameService.RevealAllMines(_gameModel);
                    }
                }
                else
                {
                    _gameService.CheckWin(_gameModel);
                    if (_gameModel.GameWon)
                    {
                        GameStatus = "Победа!";
                    }
                }
            }
            else
            {
                _gameService.ToggleFlag(_gameModel, cellViewModel.Row, cellViewModel.Column);
            }

            UpdateAllCells();
        }

        public void HandleCellRightClick(CellViewModel cellViewModel)
        {
            _gameService.ToggleFlag(_gameModel, cellViewModel.Row, cellViewModel.Column);
            UpdateAllCells();
        }

        private void UpdateCells()
        {
            Cells.Clear();

            for (int r = 0; r < _gameModel.Size; r++)
            {
                for (int c = 0; c < _gameModel.Size; c++)
                {
                    var cell = _gameModel[r, c];
                    cell.Row = r;
                    cell.Column = c;
                    Cells.Add(new CellViewModel(cell));
                }
            }
        }

        private void UpdateAllCells()
        {
            foreach (var cellViewModel in Cells)
            {
                cellViewModel.Update();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}