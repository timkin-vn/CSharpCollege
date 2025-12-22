using Minesweeper.Business.Core;
using Minesweeper.Common.Models;
using Minesweeper.Common.Repositories;
using Minesweeper.Common.Dto;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.View
{
    public partial class MainWindow : Window
    {
        private Business.Core.Field Field;
        private Border[,] Border;
        private bool _debugMode = false;
        private User _currentUser;
        private GameStateRepository _gameStateRepository;
        private UserRepository _userRepository;
        private Stopwatch _gameTimer;

        public MainWindow()
        {
            InitializeComponent();
            _gameTimer = new Stopwatch();
            StartNewGame();
        }

        public MainWindow(User user, bool loadLastGame = true)
        {
            InitializeComponent();

            _currentUser = user;
            _gameStateRepository = new GameStateRepository();
            _userRepository = new UserRepository();
            _gameTimer = new Stopwatch();

            Debug.WriteLine($"Пользователь: {user.Username}, ID: {user.Id}, Загрузить игру: {loadLastGame}");

            if (loadLastGame)
            {
                LoadLastGame();
            }
            else
            {
                StartNewGame();
            }
        }

        private void LoadLastGame()
        {
            if (_currentUser == null)
            {
                Debug.WriteLine("Ошибка: _currentUser = null");
                StartNewGame();
                return;
            }

            Debug.WriteLine($"Загрузка игры для пользователя ID: {_currentUser.Id}");

            try
            {
                var lastGameState = _gameStateRepository.GetLastGameState(_currentUser.Id);

                if (lastGameState != null)
                {
                    Debug.WriteLine($"Найдена сохраненная игра: ID={lastGameState.Id}, GameOver={lastGameState.IsGameOver}, GameWon={lastGameState.IsGameWon}");

                    if (!lastGameState.IsGameOver && !lastGameState.IsGameWon)
                    {
                        Field = _gameStateRepository.LoadGameStateFromDto(lastGameState.GameData);
                        _gameTimer.Restart();
                        _gameTimer.Elapsed.Add(lastGameState.PlayTime);
                        Debug.WriteLine($"Игра загружена, мин: {Field.MineCount}, размер: {Field.Size}");
                        CreateGrid();
                        RestoreGridState();
                        return;
                    }
                    else
                    {
                        Debug.WriteLine($"Игра завершена (GameOver={lastGameState.IsGameOver}, GameWon={lastGameState.IsGameWon}) - создаем новую");
                    }
                }
                else
                {
                    Debug.WriteLine("Сохраненная игра не найдена");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки игры: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки игры: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            StartNewGame();
        }

        private void SaveGameState(bool isGameOver = false, bool isGameWon = false)
        {
            if (Field == null)
            {
                Debug.WriteLine("Ошибка сохранения: Field = null");
                return;
            }

            if (_currentUser == null)
            {
                Debug.WriteLine("Ошибка сохранения: _currentUser = null");
                return;
            }

            try
            {
                Debug.WriteLine($"Сохранение игры: UserID={_currentUser.Id}, GameOver={isGameOver}, GameWon={isGameWon}, Время={_gameTimer.Elapsed}");

                _gameStateRepository.SaveGameState(
                    _currentUser.Id,
                    Field,
                    _gameTimer.Elapsed,
                    isGameOver,
                    isGameWon
                );

                Debug.WriteLine("Игра успешно сохранена");

                if (isGameOver || isGameWon)
                {
                    _userRepository.UpdateUserStats(_currentUser.Id, isGameWon);
                    Debug.WriteLine($"Статистика обновлена: побед={isGameWon}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка сохранения: {ex.Message}");
                MessageBox.Show($"Ошибка сохранения игры: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void StartNewGame()
        {
            Debug.WriteLine("Создание новой игры");
            Field = new Business.Core.Field(10, 15);
            CreateGrid();

            if (_gameTimer != null)
            {
                _gameTimer.Restart();
            }
        }

        public void CreateGrid()
        {
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();

            MainGrid.HorizontalAlignment = HorizontalAlignment.Center;
            MainGrid.VerticalAlignment = VerticalAlignment.Center;
            MainGrid.Margin = new Thickness(40);

            for (int i = 0; i < 10; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            }

            Border = new Border[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var cell = CreateCell(row, col);
                    Border[row, col] = cell;

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    MainGrid.Children.Add(cell);
                }
            }

            if (_debugMode)
            {
                ShowAllMinesDebug();
            }
        }

        private void RestoreGridState()
        {
            if (Field == null || Border == null) return;

            Debug.WriteLine("Восстановление состояния сетки");

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var cell = Border[row, col];

                    if (Field.Revealed[row, col])
                    {
                        cell.Background = Brushes.White;

                        if (Field.GetMine(row, col))
                        {
                            ShowMine(cell);
                        }
                        else
                        {
                            int minesCount = Field.GetNumber(row, col);
                            if (minesCount > 0)
                            {
                                ShowNumber(cell, minesCount);
                            }
                        }
                    }
                    else if (Field.Flag[row, col])
                    {
                        var textBlock = new TextBlock
                        {
                            Text = "🚩",
                            FontSize = 14,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        cell.Child = textBlock;
                        cell.Background = Brushes.LightYellow;
                    }
                }
            }
        }

        private Border CreateCell(int row, int col)
        {
            var cell = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.LightBlue,
                Tag = new System.Windows.Point(row, col)
            };

            cell.MouseLeftButtonDown += Cell_MouseLeftButtonDown;
            cell.MouseRightButtonDown += Cell_MouseRightButtonDown;

            return cell;
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Field == null || Field.GameOver || Field.GameWon)
                return;

            var cell = (Border)sender;
            var point = (System.Windows.Point)cell.Tag;
            int row = (int)point.X;
            int col = (int)point.Y;

            OpenCell(row, col);
        }

        private void Cell_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Field == null || Field.GameOver || Field.GameWon)
                return;

            var cell = (Border)sender;
            var point = (System.Windows.Point)cell.Tag;
            int row = (int)point.X;
            int col = (int)point.Y;

            ToggleFlag(row, col);
        }

        private void OpenCell(int row, int col)
        {
            if (Border == null || Border[row, col] == null)
                return;

            var cell = Border[row, col];

            if (cell.Background == Brushes.White || (cell.Child is TextBlock textBlock && textBlock.Text == "🚩"))
                return;

            Field.RevealCell(row, col);


            UpdateAllRevealedCells();

            CheckGameStatus();
        }
        private void UpdateAllRevealedCells()
        {
            if (Field == null || Border == null)
                return;

            for (int row = 0; row < Field.Size; row++)
            {
                for (int col = 0; col < Field.Size; col++)
                {
                    if (Field.Revealed[row, col])
                    {
                        UpdateCellDisplay(row, col);
                    }
                }
            }
        }
        private void UpdateCellDisplay(int row, int col)
        {
            if (Border == null || Border[row, col] == null || Field == null)
                return;

            var cell = Border[row, col];

            if (Field.Revealed[row, col])
            {
                cell.Background = Brushes.White;

                if (Field.GetMine(row, col))
                {
                    ShowMine(cell);
                }
                else
                {
                    int minesCount = Field.GetNumber(row, col);
                    if (minesCount > 0)
                    {
                        ShowNumber(cell, minesCount);
                    }
                }
            }
        }

        private void CheckGameStatus()
        {
            if (Field == null) return;

            if (Field.GameOver)
            {
                SaveGameState(true, false);
                ShowAllMines();
                MessageBox.Show("Поражение", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (Field.GameWon)
            {
                SaveGameState(false, true);
                MessageBox.Show("Победа", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                SaveGameState();
            }
        }

        private void ShowNumber(Border cell, int number)
        {
            var textBlock = new TextBlock
            {
                Text = number.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = GetNumberColor(number)
            };
            cell.Child = textBlock;
        }

        private void ShowMine(Border cell)
        {
            var textBlock = new TextBlock
            {
                Text = "💥",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            cell.Child = textBlock;
            cell.Background = Brushes.Red;
        }

        private void ShowMineDebug(Border cell)
        {
            var textBlock = new TextBlock
            {
                Text = "💥",
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Purple
            };
            cell.Child = textBlock;
            cell.Background = Brushes.LightPink;
        }

        private void ShowAllMines()
        {
            if (Field == null || Border == null) return;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        if (cell != null && cell.Child == null)
                        {
                            ShowMine(cell);
                        }
                    }
                }
            }
        }

        private void ShowAllMinesDebug()
        {
            if (Field == null || Border == null) return;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        if (cell != null)
                        {
                            ShowMineDebug(cell);
                        }
                    }
                }
            }
        }

        private void HideAllMinesDebug()
        {
            if (Field == null || Border == null) return;

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        if (cell != null && cell.Background != Brushes.Red && cell.Background != Brushes.White && cell.Background != Brushes.LightGreen)
                        {
                            cell.Child = null;
                            cell.Background = Brushes.LightBlue;
                        }
                    }
                }
            }
        }

        private void ToggleFlag(int row, int col)
        {
            if (Border == null || Border[row, col] == null || Field == null)
                return;

            var cell = Border[row, col];

            if (Field.Revealed[row, col])
                return;

            Field.ToggleFlag(row, col);

            if (Field.Flag[row, col])
            {
                var textBlock = new TextBlock
                {
                    Text = "🚩",
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                cell.Child = textBlock;
                cell.Background = Brushes.LightYellow;
            }
            else
            {
                cell.Child = null;
                cell.Background = Brushes.LightBlue;
            }

            CheckGameStatus();
        }

        private SolidColorBrush GetNumberColor(int number)
        {
            switch (number)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.DarkRed;
                case 6: return Brushes.Teal;
                case 7: return Brushes.Black;
                case 8: return Brushes.Gray;
                default: return Brushes.Black;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Нажата кнопка 'Новая игра'");

            if (_currentUser != null)
            {
                SaveGameState(true, false);
            }

            if (_gameTimer != null)
            {
                _gameTimer.Restart();
            }

            StartNewGame();
        }

        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            if (Field == null || Field.GameOver || Field.GameWon)
                return;

            _debugMode = !_debugMode;

            if (_debugMode)
            {
                DebugButton.Content = "Скрыть мины";
                DebugButton.Background = Brushes.LightGreen;
                ShowAllMinesDebug();
            }
            else
            {
                DebugButton.Content = "Показать мины";
                DebugButton.Background = Brushes.LightCoral;
                HideAllMinesDebug();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Debug.WriteLine("Окно закрывается");

            if (Field != null && !Field.GameOver && !Field.GameWon && _currentUser != null)
            {
                Debug.WriteLine("Сохранение игры при закрытии");
                SaveGameState();
            }

            if (_gameTimer != null)
            {
                _gameTimer.Stop();
                Debug.WriteLine("Таймер остановлен");
            }
        }
    }
}