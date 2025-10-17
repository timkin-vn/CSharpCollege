using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp4.Services;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private readonly GameController _gameController;

        public MainWindow()
        {
            InitializeComponent();

            _gameController = new GameController();

            InitializeGame();
            Focus();
        }

        private void InitializeGame()
        {
            _gameController.InitializeGame();
            UpdateDisplay();
            UpdateStatus();
        }

        private void UpdateDisplay()
        {
            var cellViewModels = _gameController.GetCurrentViewModels();
            GameBoard.ItemsSource = cellViewModels;
            MovesText.Text = _gameController.GetMovesText();
        }

        private void UpdateStatus()
        {
            var statusInfo = _gameController.GetStatusInfo();
            StatusText.Text = statusInfo.Text;
            StatusText.Foreground = statusInfo.Color;
        }

        private void MovePlayer(int deltaRow, int deltaCol)
        {
            var result = _gameController.MovePlayer(deltaRow, deltaCol);

            if (result.IsSuccess)
            {
                UpdateDisplay();
                UpdateStatus();

                if (result.IsGameCompleted)
                {
                    ShowCongratulations();
                }
            }
        }

        private void ShowCongratulations()
        {
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    MovePlayer(-1, 0);
                    break;
                case Key.Down:
                case Key.S:
                    MovePlayer(1, 0);
                    break;
                case Key.Left:
                case Key.A:
                    MovePlayer(0, -1);
                    break;
                case Key.Right:
                case Key.D:
                    MovePlayer(0, 1);
                    break;
                case Key.R:
                    InitializeGame();
                    break;
            }
        }

        private void DirectionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Tag.ToString())
            {
                case "Up":
                    MovePlayer(-1, 0);
                    break;
                case "Down":
                    MovePlayer(1, 0);
                    break;
                case "Left":
                    MovePlayer(0, -1);
                    break;
                case "Right":
                    MovePlayer(0, 1);
                    break;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            Focus();
        }
    }
}