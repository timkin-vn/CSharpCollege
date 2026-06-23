using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using SeaBattle.Common;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;

namespace SeaBattle.Client.ViewModels
{
    public enum GamePhase { Setup, PlayerTurn, AiTurn, Over }

    public class MainViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly User _user;
        private readonly DispatcherTimer _aiTimer;

        private Board _playerBoard;
        private Board _enemyBoard;
        private AiPlayer _ai;
        private GamePhase _phase;
        private string _status;
        private string _historyText;
        private int _moveCount;

        public ObservableCollection<CellViewModel> PlayerCells { get; private set; }
        public ObservableCollection<CellViewModel> EnemyCells { get; private set; }

        public ICommand NewGameCommand { get; private set; }
        public ICommand ShuffleCommand { get; private set; }
        public ICommand StartCommand { get; private set; }

        public string UserName { get { return _user.Name; } }

        public MainViewModel(IGameService gameService, User user)
        {
            _gameService = gameService;
            _user = user;

            PlayerCells = new ObservableCollection<CellViewModel>();
            EnemyCells = new ObservableCollection<CellViewModel>();
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                {
                    PlayerCells.Add(new CellViewModel(r, c, false));
                    EnemyCells.Add(new CellViewModel(r, c, true));
                }

            NewGameCommand = new RelayCommand(p => NewGame());
            ShuffleCommand = new RelayCommand(p => Shuffle(), p => _phase == GamePhase.Setup);
            StartCommand = new RelayCommand(p => StartBattle(), p => _phase == GamePhase.Setup);

            _aiTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            _aiTimer.Tick += OnAiTick;

            LoadHistory();
            NewGame();
        }

        public string Status { get { return _status; } private set { _status = value; OnPropertyChanged(); } }
        public string HistoryText { get { return _historyText; } private set { _historyText = value; OnPropertyChanged(); } }

        public void NewGame()
        {
            _aiTimer.Stop();
            _playerBoard = new Board();
            _enemyBoard = new Board();
            GameLogic.PlaceFleet(_playerBoard);
            GameLogic.PlaceFleet(_enemyBoard);
            _ai = new AiPlayer();
            _moveCount = 0;
            _phase = GamePhase.Setup;
            Status = "Расставьте флот: «Перетасовать», затем «В бой».";
            RefreshPlayer();
            RefreshEnemy();
        }

        private void Shuffle()
        {
            if (_phase != GamePhase.Setup) return;
            GameLogic.PlaceFleet(_playerBoard);
            RefreshPlayer();
        }

        private void StartBattle()
        {
            if (_phase != GamePhase.Setup) return;
            _phase = GamePhase.PlayerTurn;
            Status = "Бой! Стреляйте по полю противника.";
        }

        public void FireAtEnemy(int row, int col)
        {
            if (_phase != GamePhase.PlayerTurn) return;

            ShotResult result = _enemyBoard.Shoot(row, col);
            if (result == ShotResult.Invalid) return;

            _moveCount++;
            RefreshEnemy();

            if (_enemyBoard.AllSunk()) { EndGame(true); return; }

            if (result == ShotResult.Miss)
            {
                _phase = GamePhase.AiTurn;
                Status = "Мимо. Ходит противник…";
                _aiTimer.Start();
            }
            else
            {
                Status = result == ShotResult.Sunk ? "Потоплен! Стреляйте ещё." : "Попадание! Стреляйте ещё.";
            }
        }

        private void OnAiTick(object sender, EventArgs e)
        {
            _aiTimer.Stop();
            AiMove();
        }

        private void AiMove()
        {
            int r, c;
            ShotResult result = _ai.Fire(_playerBoard, out r, out c);
            RefreshPlayer();

            if (_playerBoard.AllSunk()) { EndGame(false); return; }

            if (result == ShotResult.Miss)
            {
                _phase = GamePhase.PlayerTurn;
                Status = "Противник промахнулся. Ваш ход.";
            }
            else
            {
                Status = "Противник попал! Стреляет снова…";
                _aiTimer.Start();
            }
        }

        private void EndGame(bool won)
        {
            _aiTimer.Stop();
            _phase = GamePhase.Over;
            Status = won ? "Победа! Флот противника потоплен." : "Поражение. Ваш флот уничтожен.";
            try
            {
                _gameService.SaveResult(_user.Id, _moveCount, won);
                LoadHistory();
            }
            catch (Exception ex)
            {
                Status += " (не удалось сохранить результат: " + ex.Message + ")";
            }
        }

        private void LoadHistory()
        {
            try
            {
                var history = _gameService.GetHistory(_user.Id);
                int wins = history.Count(g => g.Won);
                HistoryText = "Партий: " + history.Count + ", побед: " + wins;
            }
            catch
            {
                HistoryText = "История недоступна";
            }
        }

        private void RefreshPlayer()
        {
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    PlayerCells[r * Board.Size + c].State = _playerBoard.Cells[r, c];
        }

        private void RefreshEnemy()
        {
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    EnemyCells[r * Board.Size + c].State = _enemyBoard.Cells[r, c];
        }
    }
}
