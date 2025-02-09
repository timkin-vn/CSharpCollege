using System;
using System.Windows;
using System.Windows.Controls;

namespace game
{
    public partial class MainWindow : Window
    {
        private GameLogic gameLogic;

        public MainWindow()
        {
            InitializeComponent();
            gameLogic = new GameLogic();
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
                    MessageBox.Show($"Игрок {gameLogic.GetCurrentPlayer()} победил!");
                    ResetGame();
                    return;
                }

                if (gameLogic.IsDraw()) // Проверяем ничью
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
                ResetGame();
                return;
            }

            if (gameLogic.IsDraw()) // Проверяем ничью после хода компьютера
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
