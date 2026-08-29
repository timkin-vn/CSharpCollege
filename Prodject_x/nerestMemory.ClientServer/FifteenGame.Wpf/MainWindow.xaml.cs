using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (int[])((FrameworkElement)sender).Tag;
            ViewModel.MakeMove(tag, GameFinished);
        }

        private void GameFinished()
        {
            if (MessageBox.Show("Заезд завершен! Начать новый заезд?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information) == 
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
        
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Спрашиваем пользователя, действительно ли он хочет начать новую игру
            if (MessageBox.Show("Вы уверены, что хотите начать новый заезд?", "Подтверждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // Запускаем новую игру с обновленными названиями
                ViewModel.StartNewGame();
            }
        }
    }
}
