using Minesweeper.Common.Dto;
using Minesweeper.WpfClient.Services;
using Minesweeper.WpfClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.WpfClient.Views
{
    public partial class GameView : Window
    {
        private readonly GameViewModel _viewModel;

        public GameView(UserResponse user)
        {
            InitializeComponent();

            var gameService = new GameServiceProxy();
            var userService = new UserServiceProxy();
            _viewModel = new GameViewModel(gameService, userService);
            _viewModel.RequestExit += OnRequestExit;

            DataContext = _viewModel;

            Loaded += async (s, e) => await _viewModel.Initialize(user);
        }

        private void OnRequestExit(object sender, System.EventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            Close();
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is CellViewModel cellViewModel)
            {
                cellViewModel.ClickCommand.Execute(null);
            }
        }

        private void Cell_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is CellViewModel cellViewModel)
            {
                cellViewModel.RightClickCommand.Execute(null);
            }
        }
    }
}