using FifteenGame.Common.Dto;
using FifteenGame.Common.Enums;
using FifteenGame.Common.Services;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class RelayCommand : ICommand
    {
        private Action<object> _exec;
        public RelayCommand(Action<object> e) => _exec = e;
        public bool CanExecute(object p) => true;
        public void Execute(object p) => _exec(p);
        public event EventHandler CanExecuteChanged;
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private readonly int _userId;
        private readonly GameMode _mode;
        private GameDto _game;

        public event Action RequestMenu;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string GameStatus => _game == null ? "" :
            (_game.Mode == GameMode.Classic ? "🎯 Цель: 1000" : "🏆 Рейтинг") +
            $"   |   ⭐ Счет: {_game.Score}   |   ⏳ Ходы: {_game.MovesLeft}";

        public Visibility GlobalBestVisibility => _mode == GameMode.Ranked ? Visibility.Visible : Visibility.Collapsed;
        public string GlobalBestInfo { get; private set; }

        public ICommand CellClickCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ExitCommand { get; }

        public MainWindowViewModel(int userId, GameMode mode)
        {
            _userId = userId;
            _mode = mode;
            _gameService = NinjectKernel.Instance.Get<IGameService>();
            _userService = NinjectKernel.Instance.Get<IUserService>();

            CellClickCommand = new RelayCommand(param => OnCellClick((CellViewModel)param));
            NewGameCommand = new RelayCommand(p => StartGame());
            ExitCommand = new RelayCommand(p => RequestMenu?.Invoke());

            LoadGlobalStats();
            StartGame();
        }

        private void LoadGlobalStats()
        {
            if (_mode == GameMode.Ranked)
            {
                var top = _userService.GetGlobalTopPlayer();
                GlobalBestInfo = top != null
                    ? $"👑 ЛИДЕР: {top.Username} ({top.BestScore})"
                    : "👑 ЛИДЕР: Нет данных";
                OnPropertyChanged(nameof(GlobalBestInfo));
            }
        }

        private void StartGame()
        {
            _game = _gameService.CreateGame(_userId, (int)_mode);
            UpdateBoard();
        }

        private void OnCellClick(CellViewModel cell)
        {
            if (_game.State != GameState.Playing) return;

            _game = _gameService.MakeMove(_game.Id, cell.Row, cell.Column);
            UpdateBoard();

            if (_game.State != GameState.Playing)
            {
                string msg = "";
                if (_game.State == GameState.Won) msg = $"🎉 ПОБЕДА! Вы набрали 1000 очков!";
                else if (_game.State == GameState.Lost)
                {
                    msg = _mode == GameMode.Ranked
                        ? $"🏁 Игра окончена! Счет: {_game.Score}"
                        : "😞 Вы проиграли.";
                }

                if (MessageBox.Show(msg + "\nСыграть еще раз?", "Конец игры", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    StartGame();
                }
            }
        }

        private void UpdateBoard()
        {
            Cells.Clear();
            if (_game?.Gems != null)
            {
                for (int i = 0; i < _game.Gems.Length; i++)
                {
                    int r = i / _game.ColumnCount;
                    int c = i % _game.ColumnCount;

                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        GemType = _game.Gems[i],
                        IsMatched = _game.Matched[i],
                        IsSelected = (r == _game.SelectedRow && c == _game.SelectedColumn),
                        ClickCommand = CellClickCommand
                    });
                }
            }
            OnPropertyChanged(nameof(GameStatus));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}