using Nonogram.Common.BusinessModels;
using Nonogram.Common.Infrastructure;
using Nonogram.Common.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Nonogram.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private GameModel _currentGame;
        private UserModel _user;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler GameOver;
        public event EventHandler GameWon;

        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ObservableCollection<RowClueViewModel> RowClues { get; set; }
        public ObservableCollection<ColumnClueViewModel> ColumnClues { get; set; }

        private string _userName = "TestUser";
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private int _mistakesCount;
        public int MistakesCount
        {
            get => _mistakesCount;
            set
            {
                _mistakesCount = value;
                OnPropertyChanged(nameof(MistakesCount));
            }
        }

        public MainWindowViewModel(IGameService gameService)
        {
            Debug.WriteLine("=== MainWindowViewModel создан через конструктор с параметрами ===");

            _gameService = gameService;
            Debug.WriteLine($"GameService получен: {_gameService != null}");

            Cells = new ObservableCollection<CellViewModel>();
            RowClues = new ObservableCollection<RowClueViewModel>();
            ColumnClues = new ObservableCollection<ColumnClueViewModel>();
        }

        public MainWindowViewModel() : this(null)
        {
            Debug.WriteLine("=== MainWindowViewModel создан пустым конструктором (для дизайнера) ===");
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            UserName = user?.Name ?? "Без имени";
            Debug.WriteLine($"Пользователь установлен: {UserName}");

            LoadGame();
        }

        private void LoadGame()
        {
            if (_user == null || _gameService == null)
            {
                Debug.WriteLine("Ошибка: пользователь или GameService не установлен");
                return;
            }

            try
            {
                _currentGame = _gameService.GetByUserId(_user.Id);
                if (_currentGame == null)
                {
                    Debug.WriteLine("Ошибка: игра не загружена");
                    return;
                }

                MistakesCount = _currentGame.MistakesCount;
                FillCells();
                FillClues();
                CheckCompletedRowsAndColumns();

                Debug.WriteLine($"Игра загружена. ID: {_currentGame.Id}, Ошибок: {MistakesCount}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки игры: {ex.Message}");
            }
        }

        private void FillCells()
        {
            Cells.Clear();

            for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
            {
                for (int col = 0; col < Common.Definitions.Constants.ColumnCount; col++)
                {
                    var cell = new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        State = (CellState)_currentGame[row, col]
                    };

                    Cells.Add(cell);
                }
            }
        }

        private void FillClues()
        {
            RowClues.Clear();
            ColumnClues.Clear();

            for (int i = 0; i < _currentGame.RowClues.Count; i++)
            {
                RowClues.Add(new RowClueViewModel
                {
                    RowIndex = i,
                    Clues = _currentGame.RowClues[i],
                    IsCompleted = false
                });
            }

            for (int i = 0; i < _currentGame.ColumnClues.Count; i++)
            {
                ColumnClues.Add(new ColumnClueViewModel
                {
                    ColumnIndex = i,
                    Clues = _currentGame.ColumnClues[i],
                    IsCompleted = false
                });
            }
        }

        public void MakeMove(int row, int column)
        {
            Debug.WriteLine($"Ход: строка {row}, столбец {column}");

            if (_currentGame == null || _gameService == null)
            {
                Debug.WriteLine("Ошибка: игра не загружена");
                return;
            }

            try
            {
                var updatedGame = _gameService.MakeMove(_currentGame.Id, row, column);
                if (updatedGame == null)
                {
                    Debug.WriteLine("Ошибка: ход не выполнен");
                    return;
                }

                _currentGame = updatedGame;
                MistakesCount = _currentGame.MistakesCount;

                // Обновляем состояние клетки
                var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == column);
                if (cell != null)
                {
                    cell.State = (CellState)_currentGame[row, column];
                    Debug.WriteLine($"Состояние клетки изменено на: {cell.State}");
                }

                // Проверяем завершенные строки и столбцы
                CheckCompletedRowsAndColumns();

                // Проверяем окончание игры
                if (_gameService.IsGameOver(_currentGame.Id))
                {
                    Debug.WriteLine("=== ИГРА ОКОНЧЕНА - слишком много ошибок ===");
                    GameOver?.Invoke(this, EventArgs.Empty);

                    // Показываем сообщение об окончании игры и сбрасываем
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Игра окончена! Вы сделали {MistakesCount} ошибок. Игра будет сброшена.",
                            "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Сбрасываем игру после проигрыша
                        Initialize();
                    });
                }
                else if (_gameService.IsGameWon(_currentGame.Id))
                {
                    Debug.WriteLine("=== ИГРА ВЫИГРАНА ===");
                    GameWon?.Invoke(this, EventArgs.Empty);

                    // Показываем сообщение о победе
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Поздравляем! Вы успешно разгадали кроссворд!",
                            "Победа!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при выполнении хода: {ex.Message}");
            }
        }

        private void CheckCompletedRowsAndColumns()
        {
            // Проверяем завершенные строки
            for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
            {
                bool isRowCompleted = IsRowCompleted(row);
                RowClues[row].IsCompleted = isRowCompleted;

                // Обновляем клетки в этой строке
                for (int col = 0; col < Common.Definitions.Constants.ColumnCount; col++)
                {
                    var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == col);
                    if (cell != null)
                    {
                        cell.UpdateCompletedStatus(isRowCompleted, cell.IsInCompletedColumn);
                    }
                }
            }

            // Проверяем завершенные столбцы
            for (int col = 0; col < Common.Definitions.Constants.ColumnCount; col++)
            {
                bool isColumnCompleted = IsColumnCompleted(col);
                ColumnClues[col].IsCompleted = isColumnCompleted;

                // Обновляем клетки в этом столбце
                for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                {
                    var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == col);
                    if (cell != null)
                    {
                        cell.UpdateCompletedStatus(cell.IsInCompletedRow, isColumnCompleted);
                    }
                }
            }
        }

        private bool IsRowCompleted(int row)
        {
            // Получаем подсказку для строки
            var clues = _currentGame.RowClues[row];

            // Считаем группы закрашенных клеток в строке
            int currentGroup = 0;
            var groups = new System.Collections.Generic.List<int>();

            for (int col = 0; col < Common.Definitions.Constants.ColumnCount; col++)
            {
                if (_currentGame[row, col] == Common.Definitions.Constants.FilledCell)
                {
                    currentGroup++;
                }
                else if (currentGroup > 0)
                {
                    groups.Add(currentGroup);
                    currentGroup = 0;
                }
            }

            if (currentGroup > 0)
            {
                groups.Add(currentGroup);
            }

            // Сравниваем с подсказкой
            if (groups.Count != clues.Count)
                return false;

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i] != clues[i])
                    return false;
            }

            return true;
        }

        private bool IsColumnCompleted(int column)
        {
            // Получаем подсказку для столбца
            var clues = _currentGame.ColumnClues[column];

            // Считаем группы закрашенных клеток в столбце
            int currentGroup = 0;
            var groups = new System.Collections.Generic.List<int>();

            for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
            {
                if (_currentGame[row, column] == Common.Definitions.Constants.FilledCell)
                {
                    currentGroup++;
                }
                else if (currentGroup > 0)
                {
                    groups.Add(currentGroup);
                    currentGroup = 0;
                }
            }

            if (currentGroup > 0)
            {
                groups.Add(currentGroup);
            }

            // Сравниваем с подсказкой
            if (groups.Count != clues.Count)
                return false;

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i] != clues[i])
                    return false;
            }

            return true;
        }

        public void Initialize()
        {
            Debug.WriteLine("=== Инициализация новой игры ===");

            if (_user == null || _gameService == null)
            {
                Debug.WriteLine("Ошибка: пользователь не установлен");
                return;
            }

            try
            {
                if (_currentGame != null)
                {
                    _gameService.RemoveGame(_currentGame.Id);
                }

                _currentGame = _gameService.GetByUserId(_user.Id);
                MistakesCount = _currentGame.MistakesCount;

                FillCells();
                FillClues();
                CheckCompletedRowsAndColumns();

                Debug.WriteLine($"Новая игра создана. ID: {_currentGame.Id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка инициализации игры: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}