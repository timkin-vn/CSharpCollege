using System.Windows;
using System.Windows.Input;
using FifteenGame.Common.Definitions;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                var result = loginWindow.ViewModel.LoginResult;
                if (result != null && result.User != null)
                {
                    ViewModel.SetUser(result.User.Username, result.User.BestTimeSeconds, result.HasSavedGame, result.User.Id);
                    if (result.HasSavedGame)
                    {
                        MessageBox.Show($"С возвращением, {result.User.Username}!\nМы загрузили вашу прошлую игру.", "Сервер", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        private void EnemyCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CellVM cell)
            {
                if (cell.State == CellState.Empty || cell.State == CellState.Ship)
                {
                    ViewModel.ShootAtEnemy(cell.Column, cell.Row);
                }
            }
        }
    }
}