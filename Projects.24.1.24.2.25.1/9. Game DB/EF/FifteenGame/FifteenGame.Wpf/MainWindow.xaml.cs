
using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            dialog.ShowDialog();
            Activate();
            Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    MakeMove(MoveDirection.Left);
                    e.Handled = true;
                    break;
                case Key.Right:
                    MakeMove(MoveDirection.Right);
                    e.Handled = true;
                    break;
                case Key.Up:
                    MakeMove(MoveDirection.Up);
                    e.Handled = true;
                    break;
                case Key.Down:
                    MakeMove(MoveDirection.Down);
                    e.Handled = true;
                    break;
            }
        }

        private void MakeMove(MoveDirection direction)
        {
            ViewModel.MakeMove(direction, OnGameFinished);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
            Focus();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            MakeMove(MoveDirection.Up);
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            MakeMove(MoveDirection.Down);
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            MakeMove(MoveDirection.Left);
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            MakeMove(MoveDirection.Right);
        }

        private void OnGameFinished()
        {
            var result = MessageBox.Show(
                "Ходов больше нет. Начать новую игру?",
                "2048",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}
