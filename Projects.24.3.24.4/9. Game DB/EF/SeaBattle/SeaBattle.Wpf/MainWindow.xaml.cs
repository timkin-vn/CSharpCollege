using System.Windows;
using System.Windows.Input;
using SeaBattle.Common;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;
using SeaBattle.Wpf.ViewModels;

namespace SeaBattle.Wpf
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

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;
            var cell = element.DataContext as CellViewModel;
            if (cell == null || !cell.HideShips) return;
            _viewModel.FireAtEnemy(cell.Row, cell.Col);
        }
    }
}
