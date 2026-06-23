using System.Windows;
using Minesweeper.Common;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;
using Minesweeper.Client.ViewModels;

namespace Minesweeper.Client
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(User user)
        {
            InitializeComponent();
            var gameService = NinjectKernel.Get<IGameService>();
            _viewModel = new MainViewModel(gameService, user);
            DataContext = _viewModel;
        }
    }
}
