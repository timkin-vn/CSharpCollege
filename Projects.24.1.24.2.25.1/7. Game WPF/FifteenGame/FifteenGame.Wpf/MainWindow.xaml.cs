
using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel =>
            (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) => Keyboard.Focus(this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MoveDirection direction;

            switch (e.Key)
            {
                case Key.Left:
                    direction = MoveDirection.Left;
                    break;

                case Key.Right:
                    direction = MoveDirection.Right;
                    break;

                case Key.Up:
                    direction = MoveDirection.Up;
                    break;

                case Key.Down:
                    direction = MoveDirection.Down;
                    break;

                default:
                    return;
            }

            bool previouslyWon = ViewModel.HasWon;

            ViewModel.MakeMove(direction);

            e.Handled = true;

            if (!previouslyWon && ViewModel.HasWon)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Поздравляем! Ты собрал плитку 2048.\n\n" +
                    "Продолжить игру?",
                    "Победа!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.No)
                    ViewModel.Initialize();

                return;
            }

            if (ViewModel.IsGameOver)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Ходов больше нет.\n" +
                    $"Твой счёт: {ViewModel.Score}\n\n" +
                    "Начать новую игру?",
                    "Игра окончена",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                    ViewModel.Initialize();
            }
        }

        private void NewGameButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ViewModel.Initialize();
            Keyboard.Focus(this);
        }


        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Undo();
            }

            Focus();
        }

    }
}
