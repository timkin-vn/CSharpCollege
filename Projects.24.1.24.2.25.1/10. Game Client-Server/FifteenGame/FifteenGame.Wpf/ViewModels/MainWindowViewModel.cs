using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service = NinjectKernel.Instance.Get<IGameService>();
        private GameModel _model = new GameModel();
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private int _moveCount;
        public int MoveCount
        {
            get => _moveCount;
            set
            {
                _moveCount = value;
                OnPropertyChanged(nameof(MoveCount));
            }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        private bool _isWin;
        public bool IsWin
        {
            get => _isWin;
            set
            {
                _isWin = value;
                OnPropertyChanged(nameof(IsWin));
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public string GameStatus
        {
            get
            {
                if (IsWin) return "🏆 ПОБЕДА! Вы набрали 2048!";
                if (IsGameOver) return "💀 Игра окончена! Нет ходов.";
                return "";
            }
        }

        public MainWindowViewModel()
        {
            _model = new GameModel();
        }

        public void CommitUser(UserModel user)
        {
            _user = user;
            UserName = _user?.Name ?? "<нет>";
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void NewGame()
        {
            _service.RemoveGame(_model.Id);
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            if (IsGameOver || IsWin) return;

            try
            {
                _model = _service.MakeMove(_model.Id, direction);
                LoadViewModel(_model);

                if (_service.IsGameOver(_model.Id) ?? false)
                {
                    IsGameOver = true;
                    gameFinishedAction?.Invoke();
                }

                if (_model.IsWin)
                {
                    IsWin = true;
                    gameFinishedAction?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            System.Diagnostics.Debug.WriteLine("=== ЗАГРУЗКА КЛЕТОК ===");

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    var value = model[row, col];
                    System.Diagnostics.Debug.WriteLine($"Клетка [{row},{col}] = {value}");

                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Value = value,
                    });
                }
            }

            System.Diagnostics.Debug.WriteLine($"=== ВСЕГО КЛЕТОК: {Cells.Count} ===");

            Score = model.Score;
            MoveCount = model.MoveCount;
            IsWin = model.IsWin;
            IsGameOver = _service.IsGameOver(_model.Id) ?? false;
        }
    }
}