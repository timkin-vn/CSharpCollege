using FifteenGame.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace FifteenGame
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;


            Closed += (s, e) => Application.Current.Shutdown();
        }


        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }
    }
}