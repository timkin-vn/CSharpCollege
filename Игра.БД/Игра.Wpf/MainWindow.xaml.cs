using System.Windows;
using System.Windows.Controls;
using Игра.Wpf.ViewModels;
using Игра.Wpf.Views;

namespace Игра.Wpf
{
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cellVm = (CellViewModel)button.Tag;
            ViewModel.OnClick(cellVm.Row, cellVm.Column);

            if (ViewModel.IsGameWon)
            {
                if (MessageBox.Show("Игра окончена. Начать заново?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Infrastructure.NinjectSetup.Init();

            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            var res = dialog.ShowDialog();
        }
    }
}