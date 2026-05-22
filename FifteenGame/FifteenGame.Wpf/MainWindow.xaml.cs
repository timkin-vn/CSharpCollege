using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (_, __) => Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MoveDirection? direction = null;

            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    direction = MoveDirection.Left;
                    break;
                case Key.Right:
                case Key.D:
                    direction = MoveDirection.Right;
                    break;
                case Key.Up:
                case Key.W:
                    direction = MoveDirection.Up;
                    break;
                case Key.Down:
                case Key.S:
                    direction = MoveDirection.Down;
                    break;
            }

            if (direction == null)
            {
                return;
            }

            ViewModel.MakeMove(direction.Value, OnGameOver, OnGameWon);
            e.Handled = true;
        }

        private void OnGameWon()
        {
            if (MessageBox.Show(
                    "Вы собрали плитку 2048! Продолжить игру?",
                    "Победа!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.ContinueAfterWin();
            }
            else
            {
                ViewModel.Initialize();
            }
        }

        private void OnGameOver()
        {
            if (MessageBox.Show(
                    $"Игра окончена. Счёт: {ViewModel.Score}. Начать заново?",
                    "Конец игры",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}
