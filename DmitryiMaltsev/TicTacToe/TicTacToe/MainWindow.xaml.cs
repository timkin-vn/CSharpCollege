using TicTacToe.Business.Models;
using TicTacToe.Business.Services;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly GameModel _model;
        private readonly GameService _gameService;

        public MainWindow()
        {
            InitializeComponent();
            _model = new GameModel();
            _gameService = new GameService(_model);
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            _gameService.MakeMove(row, column);
            button.Content = _model[row, column];

            if (_gameService.IsGameOver())
            {
                MessageBox.Show("Игрок " + (_model[row, column] == "X" ? "X" : "O") + " победил!");
            }
            else if (_gameService.IsDraw())
            {
                MessageBox.Show("Ничья!");
            }
        }

        private void ResetGame_Click(object sender, RoutedEventArgs e)
        {
            _gameService.ResetGame();
            foreach (var button in GameGrid.Children)
            {
                if (button is Button btn)
                {
                    btn.Content = string.Empty;
                }
            }
        }
    }
}
