using BlackjackGame.Business.Models;
using BlackjackGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BlackjackGame.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BlackjackService _service = new BlackjackService();
        private GameState _state = new GameState();

        public ObservableCollection<Card> PlayerCards { get; private set; }
        public ObservableCollection<Card> DealerCards { get; private set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private int _playerScore;
        public int PlayerScore
        {
            get { return _playerScore; }
            set
            {
                _playerScore = value;
                OnPropertyChanged("PlayerScore");
            }
        }

        private int _dealerScore;
        public int DealerScore
        {
            get { return _dealerScore; }
            set
            {
                _dealerScore = value;
                OnPropertyChanged("DealerScore");
            }
        }

        private bool _isGameActive;
        public bool IsGameActive
        {
            get { return _isGameActive; }
            set
            {
                _isGameActive = value;
                OnPropertyChanged("IsGameActive");
            }
        }

        public ICommand HitCommand { get; private set; }
        public ICommand StandCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }

        public MainViewModel()
        {
            PlayerCards = new ObservableCollection<Card>();
            DealerCards = new ObservableCollection<Card>();

            HitCommand = new RelayCommand(ExecuteHit, CanExecuteHit);
            StandCommand = new RelayCommand(ExecuteStand, CanExecuteStand);
            NewGameCommand = new RelayCommand(ExecuteNewGame);

            StartNewGame();
        }

        private void StartNewGame()
        {
            _state = new GameState();
            _service.StartNewGame(_state);
            UpdateView();
        }

        private void ExecuteHit(object parameter)
        {
            _service.PlayerHit(_state);
            UpdateView();
        }

        private void ExecuteStand(object parameter)
        {
            _service.PlayerStand(_state);
            UpdateView();
        }

        private void ExecuteNewGame(object parameter)
        {
            StartNewGame();
        }

        private bool CanExecuteHit(object parameter)
        {
            return _state.IsPlayerTurn && _state.Status == GameStatus.Playing;
        }

        private bool CanExecuteStand(object parameter)
        {
            return _state.IsPlayerTurn && _state.Status == GameStatus.Playing;
        }

        private void UpdateView()
        {
            PlayerCards.Clear();
            foreach (var card in _state.PlayerHand)
            {
                PlayerCards.Add(card);
            }

            DealerCards.Clear();
            foreach (var card in _state.DealerHand)
            {
                DealerCards.Add(card);
            }

            PlayerScore = _service.CalculateScore(_state.PlayerHand);
            DealerScore = _service.CalculateScore(_state.DealerHand);

            Message = _state.Message;
            IsGameActive = _state.Status == GameStatus.Playing;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

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
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}