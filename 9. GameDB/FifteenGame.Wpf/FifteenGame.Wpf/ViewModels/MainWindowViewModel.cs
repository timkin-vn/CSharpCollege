using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Data.Entities;
using FifteenGame.Wpf.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();
        private readonly AuthService _authService = new AuthService();
        private readonly GameModel _model = new GameModel();
        private readonly User _currentUser;
        private readonly GameMode _currentMode;

        public event Action RequestExit;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public ICommand CellClickCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ExitCommand { get; }

        public string GlobalBestInfo { get; private set; }
        public Visibility GlobalBestVisibility => _currentMode == GameMode.Ranked ? Visibility.Visible : Visibility.Collapsed;

        public string GameStatus
        {
            get
            {
                string modeText = _currentMode == GameMode.Classic ? "🎯 Цель: 1000" : $"🏆 Мой рекорд: {_currentUser.BestScore}";
                return $"{modeText}   |   ⭐ Счет: {_model.Score}   |   ⏳ Ходы: {_model.MovesLeft}";
            }
        }

        public MainWindowViewModel(User user, GameMode mode)
        {
            _currentUser = user;
            _currentMode = mode;

            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            NewGameCommand = new RelayCommand(Initialize);
            ExitCommand = new RelayCommand(() => RequestExit?.Invoke());

            LoadGlobalStats();
            Initialize();
        }

        private void LoadGlobalStats()
        {
            if (_currentMode == GameMode.Ranked)
            {
                var topUser = _authService.GetGlobalTopPlayer();
                if (topUser != null)
                {
                    GlobalBestInfo = $"👑 ЛИДЕР: {topUser.Username} ({topUser.BestScore})";
                }
                else
                {
                    GlobalBestInfo = "👑 ЛИДЕР: Нет данных";
                }
                OnPropertyChanged(nameof(GlobalBestInfo));
            }
        }

        private void Initialize()
        {
            _service.Initialize(_model, _currentMode);
            UpdateView();
            LoadGlobalStats();
        }

        private void OnCellClick(CellViewModel cellVm)
        {
            if (_model.GameState != GameState.Playing) return;

            bool moveMade = _service.SelectGem(_model, cellVm.Row, cellVm.Column);
            UpdateView();

            if (moveMade && _model.GameState != GameState.Playing)
            {
                HandleGameOver();
            }
        }

        private void HandleGameOver()
        {
            string message;

            if (_currentMode == GameMode.Classic)
            {
                message = _model.GameState == GameState.Won
                    ? "🎉 ПОБЕДА! Вы набрали 1000 очков!"
                    : "😞 Игра окончена. Попробуйте еще раз.";
            }
            else
            {
                message = $"🏁 Игра окончена. Ваш счет: {_model.Score}.";
                if (_model.Score > _currentUser.BestScore)
                {
                    message += $"\n🚀 НОВЫЙ ЛИЧНЫЙ РЕКОРД! (Был: {_currentUser.BestScore})";
                    _authService.UpdateBestScore(_currentUser.Id, _model.Score);
                    _currentUser.BestScore = _model.Score;
                    LoadGlobalStats();
                }
            }

            if (MessageBox.Show(message + "\n\nСыграть еще раз?", "Результат",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Initialize();
            }
        }

        private void UpdateView()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        GemType = _model[row, col],
                        IsSelected = (row == _model.SelectedRow && col == _model.SelectedColumn),
                        IsMatched = _model.Matched[row, col],
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