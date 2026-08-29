using Ninject;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Services;
using Pacman.Common.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pacman.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly int _userId;
        private readonly IGameService _gameService;
        private readonly ILeaderboardService _leaderboardService;
        private GameStateDto _currentGameState;
        private const int CellSize = 25;

        public MainWindow(int userId)
        {
            InitializeComponent();

            _userId = userId;
            _gameService = NinjectKernel.Instance.Get<IGameService>();
            _leaderboardService = NinjectKernel.Instance.Get<ILeaderboardService>();

            this.Loaded += (s, e) => this.Focus();

            LoadUserGames();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentGameState = _gameService.CreateNewGame(_userId);
                RenderGame();
                LoadUserGames();
                this.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания игры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GamesListBox.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите игру для загрузки", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                dynamic item = selectedItem;
                GameDto selected = item.Game;

                _currentGameState = _gameService.GetGameState(selected.Id);
                RenderGame();
                this.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GamesListBox.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите игру для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту игру?",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    dynamic item = selectedItem;
                    GameDto selected = item.Game;

                    _gameService.RemoveGame(selected.Id);
                    LoadUserGames();

                    if (_currentGameState?.Game.Id == selected.Id)
                    {
                        _currentGameState = null;
                        GameCanvas.Children.Clear();
                    }

                    this.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления игры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LeaderboardButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var topScores = _leaderboardService.GetTopScores(10);
                var message = "Топ-10 результатов:\n\n";

                for (int i = 0; i < topScores.Count; i++)
                {
                    message += $"{i + 1}. Счёт: {topScores[i].Score} (Игра #{topScores[i].Id})\n";
                }

                MessageBox.Show(message, "Таблица лидеров", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки таблицы лидеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUserGames()
        {
            try
            {
                var games = _gameService.GetUserGames(_userId);
                GamesListBox.ItemsSource = games.Select(g => new
                {
                    Game = g,
                    Display = $"Игра #{g.Id} - Счёт: {g.Score} - Жизни: {g.Lives} - Статус: {TranslateStatus(g.Status)}"
                }).ToList();
                GamesListBox.DisplayMemberPath = "Display";
                GamesListBox.SelectedValuePath = "Game";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки игр: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string TranslateStatus(GameStatus status)
        {
            switch (status)
            {
                case GameStatus.InProgress: return "В процессе";
                case GameStatus.Won: return "Победа";
                case GameStatus.Lost: return "Проигрыш";
                default: return status.ToString();
            }
        }


        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_currentGameState == null || _currentGameState.Game.Status != GameStatus.InProgress)
                return;

            Direction direction = Direction.None;

            switch (e.Key)
            {
                case Key.Up:
                    direction = Direction.Up;
                    e.Handled = true;
                    break;
                case Key.Down:
                    direction = Direction.Down;
                    e.Handled = true;
                    break;
                case Key.Left:
                    direction = Direction.Left;
                    e.Handled = true;
                    break;
                case Key.Right:
                    direction = Direction.Right;
                    e.Handled = true;
                    break;
                default:
                    return;
            }

            try
            {
                _currentGameState = _gameService.Move(_currentGameState.Game.Id, direction);
                RenderGame();

                if (_currentGameState.Game.Status == GameStatus.Won)
                {
                    MessageBox.Show($"Поздравляем! Вы выиграли с {_currentGameState.Game.Score} очками!",
                        "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUserGames();
                }
                else if (_currentGameState.Game.Status == GameStatus.Lost)
                {
                    MessageBox.Show($"Игра окончена! Финальный счёт: {_currentGameState.Game.Score}",
                        "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUserGames();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка движения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RenderGame()
        {
            if (_currentGameState == null) return;

            GameCanvas.Children.Clear();

            var map = _currentGameState.Map;
            GameCanvas.Width = map.ColCount * CellSize;
            GameCanvas.Height = map.RowCount * CellSize;

            foreach (var cell in map.Cells)
            {
                var rect = new Rectangle
                {
                    Width = CellSize,
                    Height = CellSize
                };

                rect.Fill = GetCellBrush(cell, _currentGameState.CollectibleStates);

                Canvas.SetLeft(rect, cell.Col * CellSize);
                Canvas.SetTop(rect, cell.Row * CellSize);
                GameCanvas.Children.Add(rect);
            }

            foreach (var actor in _currentGameState.Actors)
            {
                var ellipse = new Ellipse
                {
                    Width = CellSize - 4,
                    Height = CellSize - 4
                };

                ellipse.Fill = GetActorBrush(actor);

                Canvas.SetLeft(ellipse, actor.Col * CellSize + 2);
                Canvas.SetTop(ellipse, actor.Row * CellSize + 2);
                GameCanvas.Children.Add(ellipse);
            }

            ScoreText.Text = $"Счёт: {_currentGameState.Game.Score}";
            LivesText.Text = $"Жизни: {_currentGameState.Game.Lives}";
            StatusText.Text = $"Статус: {TranslateStatus(_currentGameState.Game.Status)}";
        }

        private Brush GetCellBrush(MapCellDto cell, System.Collections.Generic.List<GameCollectibleStateDto> collectibles)
        {
            var collectible = collectibles.FirstOrDefault(c => c.Row == cell.Row && c.Col == cell.Col);

            if (collectible != null && !collectible.IsEaten)
            {
                if (collectible.CollectibleType == CellType.Pellet)
                    return Brushes.Yellow;
                if (collectible.CollectibleType == CellType.PowerPellet)
                    return Brushes.Orange;
            }

            switch (cell.CellType)
            {
                case CellType.Wall: return Brushes.Blue;
                case CellType.Empty: return Brushes.Black;
                default: return Brushes.Black;
            }
        }

        private Brush GetActorBrush(GameActorDto actor)
        {
            if (actor.ActorType == ActorType.Pacman)
                return Brushes.Yellow;

            if (actor.FrightenedTicksLeft > 0)
                return Brushes.LightBlue;

            switch (actor.ActorType)
            {
                case ActorType.GhostBlinky: return Brushes.Red;
                case ActorType.GhostPinky: return Brushes.Pink;
                case ActorType.GhostInky: return Brushes.Cyan;
                case ActorType.GhostClyde: return Brushes.Orange;
                default: return Brushes.White;
            }
        }
    }
}
