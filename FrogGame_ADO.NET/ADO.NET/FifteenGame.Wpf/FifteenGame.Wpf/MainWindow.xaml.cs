using FifteenGame.Business.Services;
using FifteenGame.DataAccess.Repositories;
using FifteenGame.Wpf.ViewModels;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                var userRepository = new UserRepository();
                var gameRepository = new GameRepository();
                var userService = new UserService(userRepository);
                var gameService = new GameService(gameRepository);

                this.DataContext = new MainWindowViewModel(gameService, userService);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}