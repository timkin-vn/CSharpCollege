using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Checkers.ViewModels;

namespace Checkers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel _viewModel;
        public MainWindow(GameViewModel gameViewModel)
        {
            InitializeComponent();
            _viewModel = gameViewModel;
            DataContext = _viewModel;
        }
        private void Restart(object sender, RoutedEventArgs e)
        {
            _viewModel.RestartGame();
        }
    }
}
