using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Data.Entities;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private readonly User _currentUser;

        public ObservableCollection<CellViewModel> Cells { get; set; }

        private string _movesText;
        public string MovesText
        {
            get => _movesText;
            set { _movesText = value; OnPropertyChanged(); }
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }


        public MainWindowViewModel(User user)
        {
            _currentUser = user;
            _gameService = new GameService();
            Cells = new ObservableCollection<CellViewModel>();


            LoadSavedGame();
        }


        private void LoadSavedGame()
        {
            // 1. Спрашиваем у базы, есть ли сохранение
            var savedState = _gameService.LoadGame(_currentUser.Id);

            if (savedState != null && !string.IsNullOrEmpty(savedState.BoardData))
            {
                // 2. Если есть — восстанавливаем
                _gameService.RestoreGame(savedState.BoardData, savedState.Score);
                StatusText = "Игра загружена!";
            }
            else
            {
                // 3. Если нет — новая игра
                _gameService.StartNewGame();
                StatusText = "Новая игра";
            }

            UpdateUserInfo();
            UpdateBoard();
        }

        public void StartNewGame()
        {
            _gameService.StartNewGame();
            UpdateUserInfo();
            UpdateBoard();
        }

        public void Move(MoveDirection direction)
        {
            _gameService.Move(direction);
            UpdateUserInfo();
            UpdateBoard();

            if (_gameService.GetField().IsGameFinished)
            {
                StatusText = "ПОБЕДА!";
                MessageBox.Show("Поздравляем! Вы прошли лабиринт.");
            }
        }

        // КНОПКА СОХРАНИТЬ
        public void SaveGame()
        {
            if (_currentUser == null) return;

            var field = _gameService.GetField();
            StringBuilder sb = new StringBuilder();


            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    sb.Append((int)field.Grid[x, y] + ",");
                }
            }

            string mapData = sb.ToString().TrimEnd(',');


            _gameService.SaveGame(_currentUser.Id, mapData, _gameService.MovesCount);

            MessageBox.Show("Игра сохранена! При следующем входе вы продолжите с этого места.");
        }

        private void UpdateUserInfo()
        {
            MovesText = $"Игрок: {_currentUser.Username} | Ходы: {_gameService.MovesCount}";
        }

        private void UpdateBoard()
        {
            Cells.Clear();
            var field = _gameService.GetField();

            // Отрисовка тоже Y потом X
            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    Cells.Add(new CellViewModel(field.Grid[x, y]));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}