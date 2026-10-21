using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using Blackjack.Common.Rules;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Blackjack.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly UserModel _user;
        private GameModel _game;

        private string _message;
        private int _playerScore;
        private int _dealerScore;
        private string _statsText;

        public MainWindowViewModel(IGameService gameService, UserModel user)
        {
            _gameService = gameService;
            _user = user;

            PlayerHand = new ObservableCollection<CardViewModel>();
            DealerHand = new ObservableCollection<CardViewModel>();
            History = new ObservableCollection<string>();

            HitCommand = new RelayCommand(_ => Run(() => _gameService.Hit(_game.Id)), _ => CanPlay());
            StandCommand = new RelayCommand(_ => Run(() => _gameService.Stand(_game.Id)), _ => CanPlay());
            NewGameCommand = new RelayCommand(_ => Run(() => _gameService.StartNewGame(_user.Id)));

            // Продолжаем незаконченную партию, если она есть в базе
            Run(() => _gameService.GetCurrentGame(_user.Id));
            UpdateStats();
        }

        public string Title => $"Игра 21 — {_user.Name}";

        public string UserName => _user.Name;

        public ObservableCollection<CardViewModel> PlayerHand { get; }

        public ObservableCollection<CardViewModel> DealerHand { get; }

        public ObservableCollection<string> History { get; }

        public ICommand HitCommand { get; }

        public ICommand StandCommand { get; }

        public ICommand NewGameCommand { get; }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public int PlayerScore
        {
            get => _playerScore;
            set { _playerScore = value; OnPropertyChanged(); }
        }

        public int DealerScore
        {
            get => _dealerScore;
            set { _dealerScore = value; OnPropertyChanged(); }
        }

        public string StatsText
        {
            get => _statsText;
            set { _statsText = value; OnPropertyChanged(); }
        }

        private bool CanPlay()
        {
            return _game != null && !_game.IsFinished;
        }

        private void Run(Func<GameModel> action)
        {
            try
            {
                _game = action();
                UpdateView();
                if (_game.IsFinished)
                {
                    UpdateStats();
                }
            }
            catch (Exception ex)
            {
                Message = "Ошибка: " + ex.Message;
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateView()
        {
            PlayerHand.Clear();
            foreach (var card in _game.PlayerCards)
            {
                PlayerHand.Add(CardViewModel.FromCard(card));
            }

            // Пока партия идёт, вторая карта дилера закрыта и в очки не идёт
            bool hideDealerCard = !_game.IsFinished;

            DealerHand.Clear();
            for (int i = 0; i < _game.DealerCards.Count; i++)
            {
                DealerHand.Add(hideDealerCard && i == 1
                    ? CardViewModel.Hidden()
                    : CardViewModel.FromCard(_game.DealerCards[i]));
            }

            PlayerScore = _game.PlayerScore;
            DealerScore = hideDealerCard
                ? ScoreCalculator.Calculate(_game.DealerCards.Take(1))
                : _game.DealerScore;

            Message = _game.Message;
        }

        private void UpdateStats()
        {
            try
            {
                var stats = _gameService.GetStats(_user.Id);
                StatsText = $"Игр: {stats.Total}\nПобед: {stats.Wins}\nПоражений: {stats.Losses}\nНичьих: {stats.Draws}";

                History.Clear();
                foreach (var item in _gameService.GetHistory(_user.Id))
                {
                    History.Add($"{item.CreatedAt.ToLocalTime():dd.MM HH:mm}  {ResultText(item.Status)}  {item.PlayerScore}:{item.DealerScore}");
                }
            }
            catch (Exception ex)
            {
                StatsText = "Не удалось загрузить статистику: " + ex.Message;
            }
        }

        private static string ResultText(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.PlayerWin: return "Победа";
                case GameStatus.DealerWin: return "Поражение";
                case GameStatus.Draw: return "Ничья";
                default: return "Идёт";
            }
        }
    }
}
