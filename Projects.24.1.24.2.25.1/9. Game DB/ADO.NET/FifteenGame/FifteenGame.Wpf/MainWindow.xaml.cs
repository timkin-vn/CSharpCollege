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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginWindow = new UserLoginWindow();
                loginWindow.Owner = this;
                loginWindow.ViewModel.MainViewModel = ViewModel; // ← Важно! Передаём ссылку
                loginWindow.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: ViewModel.MakeMove(MoveDirection.Left, OnGameFinished); break;
                case Key.Right: ViewModel.MakeMove(MoveDirection.Right, OnGameFinished); break;
                case Key.Up: ViewModel.MakeMove(MoveDirection.Up, OnGameFinished); break;
                case Key.Down: ViewModel.MakeMove(MoveDirection.Down, OnGameFinished); break;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }

        private void OnGameFinished()
        {
            // Игра завершена (победа или поражение)
        }
    }
}