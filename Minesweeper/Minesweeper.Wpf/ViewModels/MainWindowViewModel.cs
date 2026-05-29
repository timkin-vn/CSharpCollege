using Minesweeper.Business.Models;
using Minesweeper.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Minesweeper.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service;
        private GameModel _model;
        private ObservableCollection<CellViewModel> _cells;
        private string _remainingMinesText;
        private string _gameStateText;

        public ObservableCollection<CellViewModel> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value, nameof(Cells));
        }

        public string RemainingMinesText
        {
            get => _remainingMinesText;
            set => SetProperty(ref _remainingMinesText, value, nameof(RemainingMinesText));
        }

        public string GameStateText
        {
            get => _gameStateText;
            set => SetProperty(ref _gameStateText, value, nameof(GameStateText));
        }

        // Это свойство нужно, чтобы MainWindow мог узнать, выиграл ты или проиграл
        public GameState CurrentState => _model.State;

        public MainWindowViewModel()
        {
            _service = new GameService();
            _model = new GameModel();
            Cells = new ObservableCollection<CellViewModel>();

            InitializeGame();
        }

        public void InitializeGame()
        {
            _service.InitializeGame(_model);
            LoadViewModel();
            UpdateGameStateText();
        }

        public void HandleLeftClick(int row, int column, Action gameFinishedAction)
        {
            if (_model.State != GameState.Playing)
            {
                return;
            }

            _service.RevealCell(_model, row, column);
            LoadViewModel();
            UpdateGameStateText();

            if (_model.State != GameState.Playing)
            {
                gameFinishedAction?.Invoke();
            }
        }

        public void HandleRightClick(int row, int column)
        {
            if (_model.State != GameState.Playing)
            {
                return;
            }

            _service.ToggleFlag(_model, row, column);
            LoadViewModel();
            UpdateRemainingMinesText();
        }

        private void LoadViewModel()
        {
            Cells.Clear();

            for (int row = 0; row < _model.RowCount; row++)
            {
                for (int column = 0; column < _model.ColumnCount; column++)
                {
                    var cellModel = _model[row, column];
                    var cellViewModel = new CellViewModel(cellModel);
                    Cells.Add(cellViewModel);
                }
            }

            UpdateRemainingMinesText();
        }

        private void UpdateRemainingMinesText()
        {
            int remaining = _service.GetRemainingMines(_model);
            RemainingMinesText = $"Мины: {remaining}";
        }

        private void UpdateGameStateText()
        {
            switch (_model.State)
            {
                case GameState.Playing:
                    GameStateText = "Игра идёт";
                    break;
                case GameState.Won:
                    GameStateText = "Победа!";
                    break;
                case GameState.Lost:
                    GameStateText = "Проигрыш!";
                    break;
            }
        }

        public void NewGame()
        {
            InitializeGame();
        }
    }
}