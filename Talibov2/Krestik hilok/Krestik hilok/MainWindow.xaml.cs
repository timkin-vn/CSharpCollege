using System.Windows;
using System.Windows.Controls;

namespace TicTacToeWPF
{
    public partial class MainWindow : Window
    {
        private bool isXTurn = true;
        private string[,] board = new string[3, 3];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (string.IsNullOrEmpty(btn.Content?.ToString()))
            {
                btn.Content = isXTurn ? "X" : "O";
                int row = Grid.GetRow(btn);
                int col = Grid.GetColumn(btn);
                board[row, col] = btn.Content.ToString();
                isXTurn = !isXTurn;
                CheckWinner();
            }
        }

        private void CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && !string.IsNullOrEmpty(board[i, 0]))
                {
                    MessageBox.Show($"{board[i, 0]} победил!");
                    ResetBoard();
                    return;
                }
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && !string.IsNullOrEmpty(board[0, i]))
                {
                    MessageBox.Show($"{board[0, i]} победил!");
                    ResetBoard();
                    return;
                }
            }
        }

        private void ResetBoard()
        {
            board = new string[3, 3];
            foreach (var btn in (this.Content as Grid).Children)
            {
                if (btn is Button) ((Button)btn).Content = "";
            }
            isXTurn = true;
        }
    }
}