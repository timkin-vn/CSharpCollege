using GameDB.Business.Models;
using GameDB.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace GameDB.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)DataContext; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, что ViewModel не null
            if (ViewModel == null)
            {
                return;
            }

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

            try
            {
                ViewModel.MakeMove(direction.Value, OnGameOver, OnGameWon);
                e.Handled = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnGameWon()
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы собрали плитку 2048! Продолжить игру?",
                "Победа!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                ViewModel.ContinueAfterWin();
            }
            else
            {
                ViewModel.RestartGame();
            }
        }

        private void OnGameOver()
        {
            MessageBoxResult result = MessageBox.Show(
                string.Format("Игра окончена. Счёт: {0}. Начать заново?", ViewModel.Score),
                "Конец игры",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                ViewModel.RestartGame();
            }
        }
    }
}