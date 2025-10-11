using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleMinesweeper
{
    public partial class MainWindow : Window
    {
        private int[,] board = new int[5, 5]; // 0 = пусто, 1 = бомба
        private Button[,] buttons = new Button[5, 5];
        private int moves = 0;
        private int bombs = 5; // количество бомб

        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid();
            StartNewGame();
        }

        private void InitializeGrid()
        {
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 5; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button btn = new Button();
                    btn.Click += Cell_Click;
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    GameGrid.Children.Add(btn);
                    buttons[i, j] = btn;
                }
            }
        }

        private void StartNewGame()
        {
            moves = 0;
            StatusText.Text = "Ходов: 0";

            // Очистка доски
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    board[i, j] = 0;

            // Расставляем бомбы
            Random rnd = new Random();
            int placed = 0;
            while (placed < bombs)
            {
                int r = rnd.Next(5);
                int c = rnd.Next(5);
                if (board[r, c] == 0)
                {
                    board[r, c] = 1; // бомба
                    placed++;
                }
            }

            // Сброс кнопок
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    buttons[i, j].Content = "";
                    buttons[i, j].IsEnabled = true;
                    buttons[i, j].Background = System.Windows.Media.Brushes.LightGray; // сброс цвета
                }
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);

            moves++;
            StatusText.Text = $"Ходов: {moves}";

            if (board[row, col] == 1)
            {
                clickedButton.Content = "💣";
                clickedButton.Background = System.Windows.Media.Brushes.Red;
                MessageBox.Show("Вы наткнулись на бомбу! Игра окончена.");
                RevealBombs();
                DisableAllButtons();
            }
            else
            {
                int count = CountAdjacentBombs(row, col);
                clickedButton.Content = count.ToString();
                clickedButton.IsEnabled = false;

                if (CheckWin())
                {
                    MessageBox.Show($"Поздравляю! Вы выиграли за {moves} ходов!");
                    DisableAllButtons();
                }
            }
        }

        private int CountAdjacentBombs(int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < 5 && j >= 0 && j < 5 && board[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (board[i, j] == 0 && buttons[i, j].IsEnabled)
                        return false; // остались закрытые безопасные клетки
            return true;
        }

        private void RevealBombs()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (board[i, j] == 1)
                        buttons[i, j].Content = "💣";
        }

        private void DisableAllButtons()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    buttons[i, j].IsEnabled = false;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}