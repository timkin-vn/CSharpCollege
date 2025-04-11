using System;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    public partial class MainWindow : Window
    {
        private GameLogic game;
        private string playerName;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            var loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                playerName = loginWindow.PlayerName;
                Title = $"Крестики-нолики - {playerName}";
                game = new GameLogic();

                // Пытаемся загрузить сохраненную игру
                var savedState = DatabaseRepository.LoadGameState(playerName);
                if (savedState.HasValue)
                {
                    game.board = savedState.Value.board;
                    while (game.GetCurrentPlayer() != savedState.Value.player)
                    {
                        game.SwitchPlayer();
                    }
                    UpdateBoardDisplay();
                }
            }
            else
            {
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var col = Grid.GetColumn(button);

            if (game.MakeMove(row, col))
            {
                button.Content = game.GetCurrentPlayer();

                if (game.CheckForWin())
                {
                    GameOver(playerName);
                    return;
                }

                if (game.IsDraw())
                {
                    GameOver("Ничья");
                    return;
                }

                game.SwitchPlayer();
                ComputerTurn();
            }
        }

        private void ComputerTurn()
        {
            game.ComputerMove();
            UpdateBoardDisplay();

            if (game.CheckForWin())
            {
                GameOver("Компьютер");
                return;
            }

            if (game.IsDraw())
            {
                GameOver("Ничья");
                return;
            }

            game.SwitchPlayer();
        }

        private void GameOver(string winner)
        {
            if (winner != "Ничья")
            {
                DatabaseRepository.SaveWinner(winner);
                MessageBox.Show($"Победитель: {winner}!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ничья!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ResetGame();
        }

        private void ResetGame()
        {
            game = new GameLogic();
            UpdateBoardDisplay();
        }

        private void UpdateBoardDisplay()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    var button = (Button)FindName($"Button_{i}_{j}");
                    if (button != null)
                        button.Content = string.IsNullOrEmpty(game.board[i, j]) ? "" : game.board[i, j];
                }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!game.CheckForWin() && !game.IsDraw())
            {
                DatabaseRepository.SaveGameState(playerName, game.board, game.GetCurrentPlayer());
            }
        }
    }
}