using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = (ViewModels.MainWindowViewModel)DataContext;
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = vm;
            dialog.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.MainWindowViewModel)DataContext;
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    vm.MakeMove(-1, 0, null);
                    e.Handled = true;
                    break;
                case Key.Down:
                case Key.S:
                    vm.MakeMove(1, 0, null);
                    e.Handled = true;
                    break;
                case Key.Left:
                case Key.A:
                    vm.MakeMove(0, -1, null);
                    e.Handled = true;
                    break;
                case Key.Right:
                case Key.D:
                    vm.MakeMove(0, 1, null);
                    e.Handled = true;
                    break;
            }
        }
    }
}
