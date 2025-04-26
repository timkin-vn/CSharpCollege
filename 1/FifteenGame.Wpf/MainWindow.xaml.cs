using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private GameModel _model;
        private GameService _service;
        private Label[,] _labels;

        public MainWindow()
        {
            InitializeComponent();

            _model = new GameModel();
            _service = new GameService(_model);

            _labels = new Label[GameModel.Size, GameModel.Size];
            InitGrid();
            StartGame();
        }

        private void StartGame()
        {
            _service.StartNewGame();
            Render();
        }

        private void InitGrid()
        {
            GameGrid.Rows = GameModel.Size;
            GameGrid.Columns = GameModel.Size;
            GameGrid.Children.Clear();

            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    var label = new Label
                    {
                        FontSize = 32,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Background = Brushes.LightGray,
                        Margin = new Thickness(5),
                        FontWeight = FontWeights.Bold
                    };
                    _labels[i, j] = label;
                    GameGrid.Children.Add(label);
                }
            }
        }

        private void Render()
        {
            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    int value = _model.GetCell(i, j);
                    var label = _labels[i, j];
                    label.Content = value == 0 ? string.Empty : value.ToString();
                    label.Background = GetTileBrush(value);
                }
            }
        }

        private Brush GetTileBrush(int value)
        {
            switch (value)
            {
                case 0: return Brushes.LightGray;
                case 2: return Brushes.Beige;
                case 4: return Brushes.Bisque;
                case 8: return Brushes.Orange;
                case 16: return Brushes.OrangeRed;
                case 32: return Brushes.Red;
                case 64: return Brushes.DarkRed;
                case 128: return Brushes.Gold;
                case 256: return Brushes.Yellow;
                case 512: return Brushes.GreenYellow;
                case 1024: return Brushes.Green;
                case 2048: return Brushes.DarkGreen;
                default: return Brushes.Black;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool moved = false;

            switch (e.Key)
            {
                case Key.Up:
                    moved = _service.Move(MoveDirection.Up);
                    break;
                case Key.Down:
                    moved = _service.Move(MoveDirection.Down);
                    break;
                case Key.Left:
                    moved = _service.Move(MoveDirection.Left);
                    break;
                case Key.Right:
                    moved = _service.Move(MoveDirection.Right);
                    break;
            }

            if (moved)
                Render();
            if (_service.Has2048())
            {
                MessageBox.Show("Поздравляем! Вы собрали 2048!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (!_service.HasAvailableMoves())
            {
                MessageBox.Show("Ходов больше нет. Вы проиграли!", "Поражение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
