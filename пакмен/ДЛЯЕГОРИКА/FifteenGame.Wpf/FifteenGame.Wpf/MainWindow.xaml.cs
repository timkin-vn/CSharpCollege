using Pacman.Business.Services;
using StepByStepPacman.Business;
using StepByStepPacman.Business.Models;
using StepByStepPacman.Business.Services; 
using StepByStepPacman.WPF.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls; // Не используется, можно удалить
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

// NOTE: using StepByStepPacman.Business.Services; - уже был добавлен выше, дублирование убрано.

namespace StepByStepPacman.WPF
{
    public partial class MainWindow : Window
    {
        private readonly GameViewModel _vm;
        private readonly DispatcherTimer _renderTimer;

        // --- ПОЛЯ ДЛЯ АВТОРИЗАЦИИ И БД ---
        private readonly int _currentUserId;
        private readonly GameDataService _gameDataService;
        // ----------------------------------

        // КОНСТРУКТОР БЕЗ АРГУМЕНТОВ
        public MainWindow() : this(0)
        {
            // Используем основной конструктор
        }

        // КРИТИЧЕСКИ ВАЖНЫЙ КОНСТРУКТОР 
        public MainWindow(int userId)
        {
            InitializeComponent();

            _currentUserId = userId; // Устанавливаем ID пользователя

            // NOTE: Убедитесь, что класс GameDataService определен
            _gameDataService = new GameDataService();
            _vm = new GameViewModel(_currentUserId, _gameDataService);
            DataContext = _vm;

            // Инициализация игры с логикой загрузки
            InitializeGameLogic();

            // Запускаем таймер рендеринга
            _renderTimer = new DispatcherTimer();
            _renderTimer.Interval = TimeSpan.FromMilliseconds(50);
            _renderTimer.Tick += RenderTimer_Tick;
            _renderTimer.Start();

            
        }

        // --- ЛОГИКА ЗАГРУЗКИ ИЛИ НОВОЙ ИГРЫ ---
        private void InitializeGameLogic()
        {
            Debug.WriteLine($"Initializing Game for User ID: {_currentUserId}");

            bool loadedSuccessfully = false;

            if (_currentUserId > 0)
            {
                // Пытаемся загрузить состояние из БД
                GameStateData loadedData = _gameDataService.LoadGameState(_currentUserId);

                if (loadedData != null)
                {
                    // Если загрузка успешна, применяем состояние к ViewModel
                    _vm.LoadState(loadedData);
                    loadedSuccessfully = true;
                    Debug.WriteLine($"Game loaded successfully. Score: {_vm.State.Score}, Dots: {_vm.State.CollectedDots}/{_vm.State.TotalDots}");
                }
            }

            if (!loadedSuccessfully)
            {
                // Если новая игра или загрузка не удалась, начинаем с нуля.
                _vm.Restart();
                Debug.WriteLine("Starting new game.");
            }

            
        }

        // --- ЛОГИКА СОХРАНЕНИЯ В БД ---
        
        public void SaveGameToDatabase() // Сделал public, если GameViewModel вызывает его через сервис
        {
            if (_currentUserId <= 0)
            {
                Debug.WriteLine("Warning: Not saving game, user ID is invalid (0).");
                return;
            }

            try
            {
                // 1. Получаем состояние игры из ViewModel
                GameStateData stateToSave = _vm.GetGameStateData();

                // 2. Сохраняем в БД
                _gameDataService.SaveGameState(_currentUserId, stateToSave);

                Debug.WriteLine("Progress saved to database.");
                // MessageBox.Show("Игра сохранена!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information); // Можно добавить для обратной связи
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database Save Error: {ex.Message}");
                MessageBox.Show($"Ошибка сохранения прогресса: {ex.Message}", "Ошибка");
            }
        }

        

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            DrawGame();
        }

        private void DrawGame()
        {
            var state = _vm.State;
            GameCanvas.Children.Clear();

            // Рисуем стены и точки
            for (int y = 0; y < GameState.GRID_HEIGHT; y++)
            {
                for (int x = 0; x < GameState.GRID_WIDTH; x++)
                {
                    DrawCell(x, y, state.GameBoard[y, x]);
                }
            }

            // Рисуем Pacman
            DrawPacman(state);

            // Рисуем призраков
            DrawGhosts(state);

            

            // Проверяем состояние игры
            CheckGameOver(state);
        }

        private void DrawCell(int x, int y, int cellType)
        {
            
            if (cellType < 0)
            {
                int originalValue = -cellType;
                switch (originalValue)
                {
                    case 2:
                        DrawDot(x, y, true);
                        break;
                    case 3:
                        DrawEnergizer(x, y, true);
                        break;
                }
                return;
            }

            switch (cellType)
            {
                case 0:
                    DrawWall(x, y);
                    break;
                case 2:
                    DrawDot(x, y, false);
                    break;
                case 3:
                    DrawEnergizer(x, y, false);
                    break;
                    // case 1 (путь) и 5 (игрок) не рисуются
            }
        }

        private void DrawWall(int x, int y)
        {
            var wallRect = new Rectangle
            {
                Width = GameState.TILE_SIZE,
                Height = GameState.TILE_SIZE,
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 139)),
                Stroke = Brushes.Blue,
                StrokeThickness = 1
            };
            Canvas.SetLeft(wallRect, x * GameState.TILE_SIZE);
            Canvas.SetTop(wallRect, y * GameState.TILE_SIZE);
            GameCanvas.Children.Add(wallRect);
        }

        private void DrawDot(int x, int y, bool isHidden)
        {
            var dotEllipse = new Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = isHidden ? Brushes.LightGray : Brushes.White
            };
            Canvas.SetLeft(dotEllipse, x * GameState.TILE_SIZE + GameState.TILE_SIZE / 2 - 3);
            Canvas.SetTop(dotEllipse, y * GameState.TILE_SIZE + GameState.TILE_SIZE / 2 - 3);
            GameCanvas.Children.Add(dotEllipse);
        }

        private void DrawEnergizer(int x, int y, bool isHidden)
        {
            var energizerEllipse = new Ellipse
            {
                Width = 16,
                Height = 16,
                Fill = isHidden ? Brushes.LightGray : Brushes.White
            };
            Canvas.SetLeft(energizerEllipse, x * GameState.TILE_SIZE + GameState.TILE_SIZE / 2 - 8);
            Canvas.SetTop(energizerEllipse, y * GameState.TILE_SIZE + GameState.TILE_SIZE / 2 - 8);
            GameCanvas.Children.Add(energizerEllipse);
        }

        private void DrawPacman(GameState state)
        {
            var pacmanEllipse = new Ellipse
            {
                Width = state.Player.Size,
                Height = state.Player.Size,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(pacmanEllipse, state.Player.X * GameState.TILE_SIZE + 2);
            Canvas.SetTop(pacmanEllipse, state.Player.Y * GameState.TILE_SIZE + 2);
            GameCanvas.Children.Add(pacmanEllipse);
        }

        private void DrawGhosts(GameState state)
        {
            foreach (var ghost in state.Ghosts)
            {
                Brush ghostBrush = GetGhostBrush(ghost.Color);

                var ghostEllipse = new Ellipse
                {
                    Width = ghost.Size,
                    Height = ghost.Size,
                    Fill = ghostBrush
                };
                Canvas.SetLeft(ghostEllipse, ghost.X * GameState.TILE_SIZE + 2);
                Canvas.SetTop(ghostEllipse, ghost.Y * GameState.TILE_SIZE + 2);
                GameCanvas.Children.Add(ghostEllipse);
            }
        }

        private Brush GetGhostBrush(ColorType color)
        {
            switch (color)
            {
                case ColorType.Red: return Brushes.Red;
                case ColorType.Pink: return Brushes.HotPink;
                case ColorType.Cyan: return Brushes.Cyan;
                case ColorType.Orange: return Brushes.Orange;
                default: return Brushes.Red;
            }
        }
     
        // Статистика теперь обновляется через Data Binding в XAML.

        private void CheckGameOver(GameState state)
        {
            if (state.IsGameOver)
            {
                // Проверка на null для безопасности, хотя XAML должен гарантировать наличие
                if (GameOverPanel != null)
                {
                    GameOverPanel.Visibility = Visibility.Visible;
                }

                if (state.Lives <= 0)
                {
                    if (GameOverText != null)
                    {
                        GameOverText.Text = "Игра окончена!";
                        GameOverText.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    if (GameOverText != null)
                    {
                        GameOverText.Text = "ПОБЕДА! 🎉";
                        GameOverText.Foreground = Brushes.Green;
                    }
                }
            }
            else
            {
                if (GameOverPanel != null)
                {
                    GameOverPanel.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_vm.State.IsGameOver || !_vm.State.GameRunning) return;

            switch (e.Key)
            {
                case Key.Up:
                    _vm.SetDirection(Direction.Up);
                    break;
                case Key.Down:
                    _vm.SetDirection(Direction.Down);
                    break;
                case Key.Left:
                    _vm.SetDirection(Direction.Left);
                    break;
                case Key.Right:
                    _vm.SetDirection(Direction.Right);
                    break;
            }
        }


        // Кнопка загрузки, которая просто информирует, что загрузка автоматическая
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Загрузка прогресса происходит автоматически при входе в систему. Для загрузки сохраненного состояния, пожалуйста, выйдите и войдите снова.",
                            "Информация",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        // Кнопка ВЫХОДА (Logout)
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _renderTimer.Stop();
            SaveGameToDatabase(); // Сохраняем перед выходом

            

            this.Close();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameOverPanel != null)
            {
                GameOverPanel.Visibility = Visibility.Collapsed;
            }
            _vm.Restart();
        }
        
    }
}