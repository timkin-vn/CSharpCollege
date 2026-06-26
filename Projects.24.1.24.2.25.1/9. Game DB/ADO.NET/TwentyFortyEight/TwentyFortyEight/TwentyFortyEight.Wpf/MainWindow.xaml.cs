using System.Windows;
using System.Windows.Input;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Wpf.ViewModels;
using TwentyFortyEight.Wpf.Views;

namespace TwentyFortyEight.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel =>
            (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var login = new UserLoginWindow();
            login.ViewModel.MainViewModel = ViewModel;
            login.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    ViewModel.MakeMove(MoveDirection.Left, OnGameFinished, OnGameWon);
                    break;

                case Key.Right:
                    ViewModel.MakeMove(MoveDirection.Right, OnGameFinished, OnGameWon);
                    break;

                case Key.Up:
                    ViewModel.MakeMove(MoveDirection.Up, OnGameFinished, OnGameWon);
                    break;

                case Key.Down:
                    ViewModel.MakeMove(MoveDirection.Down, OnGameFinished, OnGameWon);
                    break;
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Начать новую игру? Текущий прогресс будет потерян.",
                "Новая игра",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                ViewModel.NewGame();
        }

        private void OnGameWon()
        {
            MessageBox.Show(
                "Поздравляем! Вы достигли плитки 2048!\n\nПродолжайте играть, чтобы побить рекорд!",
                "🎉 Победа!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void OnGameFinished()
        {
            var result = MessageBox.Show(
                $"Игра окончена!\n\nВаш счёт: {ViewModel.Score}\nЛучшая плитка: {ViewModel.BestTile}\n\nНачать новую игру?",
                "Конец игры",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
                ViewModel.NewGame();
        }
    }
}
