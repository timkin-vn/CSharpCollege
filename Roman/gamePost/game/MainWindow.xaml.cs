using System;
using System.Windows;
using System.Windows.Controls;
using game;
using Npgsql;
namespace game
{
    public partial class MainWindow : Window
    {
        private GameLogic gameLogic;
        private string currentPlayerName;

        public MainWindow()
        {
            InitializeComponent();
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                currentPlayerName = loginWindow.PlayerName;
            }
            else
            {
                Close();
            }

            gameLogic = new GameLogic();

            string lastWinner = DatabaseRepository.GetLastWinner();
            MessageBox.Show($"Последний победитель: {lastWinner}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (gameLogic.MakeMove(row, col))
            {
                button.Content = gameLogic.GetCurrentPlayer();

                if (gameLogic.CheckForWin())
                {
                    string winner = currentPlayerName; 
                    MessageBox.Show($"Игрок {winner} победил!");

                    DatabaseRepository.SaveWinner(winner); 

                    ResetGame();
                    return;
                }

                if (gameLogic.IsDraw())
                {
                    MessageBox.Show("Ничья!");
                    ResetGame();
                    return;
                }

                gameLogic.SwitchPlayer();
                ComputerTurn();
            }
        }

        private void ComputerTurn()
        {
            gameLogic.ComputerMove();
            UpdateUI();

            if (gameLogic.CheckForWin())
            {
                MessageBox.Show("Компьютер победил!");
                DatabaseRepository.SaveWinner("Компьютер");
                ResetGame();
                return;
            }

            if (gameLogic.IsDraw())
            {
                MessageBox.Show("Ничья!");
                ResetGame();
                return;
            }

            gameLogic.SwitchPlayer();
        }

        private void UpdateUI()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = (Button)FindName($"Button{i}{j}");
                    button.Content = gameLogic.board[i, j];
                }
            }
        }

        private void ResetGame()
        {
            gameLogic = new GameLogic();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = (Button)FindName($"Button{i}{j}");
                    button.Content = "";
                }
            }
        }
    }
}
