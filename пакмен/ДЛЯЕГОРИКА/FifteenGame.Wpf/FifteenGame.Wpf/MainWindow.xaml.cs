using StepByStepPacman.Business;
using StepByStepPacman.Business.Models;
using StepByStepPacman.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StepByStepPacman.WPF
{
    public partial class MainWindow : Window
    {
        private readonly GameViewModel _vm;
        private readonly DispatcherTimer _renderTimer;

        public MainWindow()
        {
            InitializeComponent(); // Эта строка должна быть!

            _vm = new GameViewModel();
            DataContext = _vm;

            _renderTimer = new DispatcherTimer();
            _renderTimer.Interval = TimeSpan.FromMilliseconds(50);
            _renderTimer.Tick += RenderTimer_Tick;
            _renderTimer.Start();
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

            // Обновляем статистику
            UpdateHUD(state);

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
                        DrawDot(x, y, true); // скрытая точка под призраком
                        break;
                    case 3:
                        DrawEnergizer(x, y, true); // скрытый энерджайзер под призраком
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
                    DrawDot(x, y, false); // обычная точка
                    break;
                case 3:
                    DrawEnergizer(x, y, false); // обычный энерджайзер
                    break;
            }
        }

        private void DrawWall(int x, int y)
        {
            var wallRect = new Rectangle
            {
                Width = GameState.TILE_SIZE,
                Height = GameState.TILE_SIZE,
                Fill = new SolidColorBrush(Color.FromRgb(0, 0, 139)), // темно-синий
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

        private void UpdateHUD(GameState state)
        {
            ScoreText.Text = state.Score.ToString();
            LivesText.Text = state.Lives.ToString();
            DotsText.Text = $"{state.CollectedDots}/{state.TotalDots}";
        }

        private void CheckGameOver(GameState state)
        {
            if (state.IsGameOver)
            {
                GameOverPanel.Visibility = Visibility.Visible;
                if (state.Lives <= 0)
                {
                    GameOverText.Text = "Игра окончена!";
                    GameOverText.Foreground = Brushes.Red;
                }
                else
                {
                    GameOverText.Text = "ПОБЕДА! 🎉";
                    GameOverText.Foreground = Brushes.Green;
                }
            }
            else
            {
                GameOverPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_vm.State.IsGameOver) return;

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

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            GameOverPanel.Visibility = Visibility.Collapsed;
            _vm.Restart();
        }
    }
}