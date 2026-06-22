using System.Windows;
using System.Windows.Input;
using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;
using Game2048.Wpf.ViewModels;

namespace Game2048.Wpf
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: _viewModel.Move(MoveDirection.Left); e.Handled = true; break;
                case Key.Right: _viewModel.Move(MoveDirection.Right); e.Handled = true; break;
                case Key.Up: _viewModel.Move(MoveDirection.Up); e.Handled = true; break;
                case Key.Down: _viewModel.Move(MoveDirection.Down); e.Handled = true; break;
            }
        }
    }
}
