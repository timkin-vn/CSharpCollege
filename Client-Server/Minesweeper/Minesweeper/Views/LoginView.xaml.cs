using Minesweeper.WpfClient.Services;
using Minesweeper.WpfClient.ViewModels;
using System.Windows;

namespace Minesweeper.WpfClient.Views
{
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();

            var userService = new UserServiceProxy();
            _viewModel = new LoginViewModel(userService);
            _viewModel.LoginSucceeded += OnLoginSucceeded;

            DataContext = _viewModel;
        }

        private void OnLoginSucceeded(object sender, Minesweeper.Common.Dto.UserResponse user)
        {
            var gameView = new GameView(user);
            gameView.Show();
            Close();
        }
    }
}