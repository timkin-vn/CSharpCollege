using Pacmen.Business.Models;
using Pacmen.WPF.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pacmen.WPF
{
    public partial class MainWindow : Window
    {
        private readonly GameViewModel _viewModel;
        private readonly DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new GameViewModel();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200) // Скорость движения
            };
            _timer.Tick += Timer_Tick;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGame();
            _timer.Start();
        }

        private void DrawGame()
        {
            GameCanvas.Children.Clear();
            for (int i = 0; i < _viewModel.GameState.Maze.GetLength(0); i++)
            {
                for (int j = 0; j < _viewModel.GameState.Maze.GetLength(1); j++)
                {
                    if (_viewModel.GameState.Maze[i, j] == 0) // Стена
                    {
                        Rectangle wall = new Rectangle
                        {
                            Width = _viewModel.GameState.CellSize,
                            Height = _viewModel.GameState.CellSize,
                            Fill = Brushes.Blue
                        };
                        Canvas.SetLeft(wall, j * _viewModel.GameState.CellSize);
                        Canvas.SetTop(wall, i * _viewModel.GameState.CellSize);
                        GameCanvas.Children.Add(wall);
                    }
                    else if (_viewModel.GameState.Maze[i, j] == 2) // Монетка
                    {
                        Ellipse coin = new Ellipse
                        {
                            Width = 8,
                            Height = 8,
                            Fill = Brushes.Yellow
                        };
                        Canvas.SetLeft(coin, j * _viewModel.GameState.CellSize + _viewModel.GameState.CellSize / 2 - 4);
                        Canvas.SetTop(coin, i * _viewModel.GameState.CellSize + _viewModel.GameState.CellSize / 2 - 4);
                        GameCanvas.Children.Add(coin);
                    }
                }
            }

            // Отрисовка Pac-Man
            Ellipse pacman = new Ellipse
            {
                Width = _viewModel.GameState.CellSize - 4,
                Height = _viewModel.GameState.CellSize - 4,
                Fill = Brushes.Yellow
            };
            Canvas.SetLeft(pacman, _viewModel.GameState.PacmanPosition.Y * _viewModel.GameState.CellSize + 2);
            Canvas.SetTop(pacman, _viewModel.GameState.PacmanPosition.X * _viewModel.GameState.CellSize + 2);
            GameCanvas.Children.Add(pacman);

            // Отрисовка призраков
            foreach (var ghost in _viewModel.GameState.GhostPositions)
            {
                Ellipse ghostEllipse = new Ellipse
                {
                    Width = _viewModel.GameState.CellSize - 4,
                    Height = _viewModel.GameState.CellSize - 4,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(ghostEllipse, ghost.Y * _viewModel.GameState.CellSize + 2);
                Canvas.SetTop(ghostEllipse, ghost.X * _viewModel.GameState.CellSize + 2);
                GameCanvas.Children.Add(ghostEllipse);
            }

            ScoreText.Text = $"Score: {_viewModel.GameState.Score}";

            if (_viewModel.GameState.IsGameOver)
            {
                GameOverText.Text = _viewModel.GameState.HasWon ? "You Win!" : "Game Over!";
                GameOverText.Visibility = Visibility.Visible;
                _timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _viewModel.GameService.MovePacman(_viewModel.CurrentDirection);
            _viewModel.GameService.MoveGhosts();
            DrawGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_viewModel.GameState.IsGameOver) return;

            switch (e.Key)
            {
                case Key.Up:
                    _viewModel.CurrentDirection = Direction.Up;
                    break;
                case Key.Down:
                    _viewModel.CurrentDirection = Direction.Down;
                    break;
                case Key.Left:
                    _viewModel.CurrentDirection = Direction.Left;
                    break;
                case Key.Right:
                    _viewModel.CurrentDirection = Direction.Right;
                    break;
            }
        }
    }
}