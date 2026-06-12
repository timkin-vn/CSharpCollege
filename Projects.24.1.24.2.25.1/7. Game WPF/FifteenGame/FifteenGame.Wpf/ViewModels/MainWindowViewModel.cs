using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Game2048.Wpf.Models;

namespace Game2048.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameModel _gameModel;
        private string _statusText;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int Score => _gameModel.Score;

        public string StatusText
        {
            get => _statusText;
            private set { _statusText = value; OnPropertyChanged(); }
        }

        public ICommand MoveCommand { get; }
        public ICommand RestartCommand { get; }

        public MainWindowViewModel()
        {
            _gameModel = new GameModel();

            MoveCommand = new RelayCommand(param => {
                if (param is MoveDirection direction) HandleKeyPress(direction);
            });
            RestartCommand = new RelayCommand(param => RestartGame());

            for (int i = 0; i < GameModel.BoardSize * GameModel.BoardSize; i++)
            {
                Cells.Add(new CellViewModel());
            }

            UpdateUiState();
        }

        public void HandleKeyPress(MoveDirection direction)
        {
            if (_gameModel.MakeMove(direction))
            {
                UpdateUiState();
            }
        }

        public void RestartGame()
        {
            _gameModel.Reset();
            UpdateUiState();
        }

        private void UpdateUiState()
        {
            for (int r = 0; r < GameModel.BoardSize; r++)
            {
                for (int c = 0; c < GameModel.BoardSize; c++)
                {
                    int index = r * GameModel.BoardSize + c;
                    int val = _gameModel.Board[r, c];

                    
                    Cells[index].Value = val;
                }
            }

            OnPropertyChanged(nameof(Score));
            if (_gameModel.IsWin) StatusText = "Вы победили! 2048 собрано!";
            else if (_gameModel.IsGameOver) StatusText = "Игра окончена. Ходов больше нет.";
            else StatusText = "Игра в процессе...";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
