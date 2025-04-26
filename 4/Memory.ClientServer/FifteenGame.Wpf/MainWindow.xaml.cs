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
            this.KeyDown += MainWindow_KeyDown; 
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            int? direction = null;
            if (e.Key == Key.Up) direction = 0;
            if (e.Key == Key.Right) direction = 1;
            if (e.Key == Key.Down) direction = 2;
            if (e.Key == Key.Left) direction = 3;

            if (direction.HasValue)
            {
                ViewModel.MakeMove(direction.Value, GameFinished);
                e.Handled = true;
            }
        }

        private void GameFinished()
        {
            if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loginDialog = new UserLoginWindow();
            loginDialog.ViewModel.MainViewModel = ViewModel;
            loginDialog.ShowDialog();
        }
    }
}
