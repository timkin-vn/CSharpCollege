using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
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
            DataContext = new MainWindowViewModel();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Focus();
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

            if (direction.HasValue)
            {
                ViewModel.MakeMove(direction.Value, OnGameFinished);
                e.Handled = true;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
            Focus();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MakeMove(MoveDirection.Up, OnGameFinished);
            Focus();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MakeMove(MoveDirection.Down, OnGameFinished);
            Focus();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MakeMove(MoveDirection.Left, OnGameFinished);
            Focus();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MakeMove(MoveDirection.Right, OnGameFinished);
            Focus();
        }

        private void OnGameFinished()
        {
            MessageBox.Show(
                "Ходов больше нет. Текущий результат сохранён.",
                "Игра окончена",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;

            bool? result = dialog.ShowDialog();

            if (result != true)
            {
                Close();
            }
        }
    }
}