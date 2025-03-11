using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WordGame.Models;
using WordGame.Services;

namespace WordGame.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService = new GameService();
        private readonly GameModel _model = new GameModel();
        private string _targetWord;
        private int _score;
        private int _timeRemaining;
        private bool _isGameRunning;
        private CellViewModel _lastSelectedCell;
        private int _currentIndex;
        private DispatcherTimer _timer;
        private ICommand _startGameCommand;
        private ICommand _makeMoveCommand;
        private string _errorMessage;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string TargetWord
        {
            get => _targetWord;
            set
            {
                _targetWord = value;
                OnPropertyChanged(nameof(TargetWord));
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public int TimeRemaining
        {
            get => _timeRemaining;
            set
            {
                _timeRemaining = value;
                OnPropertyChanged(nameof(TimeRemaining));
            }
        }

        public bool IsGameRunning
        {
            get => _isGameRunning;
            set
            {
                _isGameRunning = value;
                OnPropertyChanged(nameof(IsGameRunning));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand StartGameCommand
        {
            get
            {
                if (_startGameCommand == null)
                {
                    _startGameCommand = new RelayCommand(param => StartGame(1), param => !IsGameRunning);
                }
                return _startGameCommand;
            }
        }

        public ICommand MakeMoveCommand
        {
            get
            {
                if (_makeMoveCommand == null)
                {
                    _makeMoveCommand = new RelayCommand(param => MakeMove((CellViewModel)param), param => IsGameRunning);
                }
                return _makeMoveCommand;
            }
        }

        public MainWindowViewModel()
        {
            IsGameRunning = false;
        }

        public void StartGame(int difficulty)
        {
            _gameService.InitializeGame(_model, difficulty);
            TargetWord = _model.TargetWord;
            Score = _model.Score;
            TimeRemaining = 30;
            IsGameRunning = true;
            _lastSelectedCell = null;
            _currentIndex = 0;
            ErrorMessage = string.Empty;
            StartTimer();
            UpdateCells();
        }

        public async void MakeMove(CellViewModel cell)
        {
            if (_lastSelectedCell == null || IsValidMove(cell))
            {
                if (cell.Letter == TargetWord[_currentIndex])
                {
                    cell.IsSelected = !cell.IsSelected; 
                    if (cell.IsSelected)
                    {
                        _lastSelectedCell = cell;
                        _currentIndex++;
                    }
                    else
                    {
                        _lastSelectedCell = null;
                        _currentIndex--;
                    }

                    if (IsWordCompleted())
                    {
                        EndGame(true);
                    }
                }
                else
                {
                    ShowErrorMessage("Собирать слово надо из тех, что в списке!");
                }
            }
            else
            {
                ShowErrorMessage("Нажимать можно только на клетку по одной линии!");
            }
        }

        private bool IsValidMove(CellViewModel cell)
        {
            return _lastSelectedCell == null || cell.Row == _lastSelectedCell.Row || cell.Column == _lastSelectedCell.Column;
        }

        private bool IsWordCompleted()
        {
            string selectedLetters = "";
            foreach (var cell in Cells)
            {
                if (cell.IsSelected)
                {
                    selectedLetters += cell.Letter;
                }
            }
            return selectedLetters == TargetWord;
        }

        private void UpdateCells()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    Cells.Add(new CellViewModel { Row = row, Column = col, Letter = _model[row, col] });
                }
            }
        }

        private void StartTimer()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += OnTimedEvent;
            _timer.Start();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            TimeRemaining--;
            if (TimeRemaining <= 0)
            {
                EndGame(false);
            }
        }

        private void EndGame(bool isWinner)
        {
            _timer.Stop();
            IsGameRunning = false;
            if (isWinner)
            {
                Score++;
                if (TimeRemaining > 15)
                {
                    Score++;
                }
            }
        }

        private async void ShowErrorMessage(string message)
        {
            ErrorMessage = message;
            await Task.Delay(2000);
            ErrorMessage = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}