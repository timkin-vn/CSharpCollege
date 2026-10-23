using System.Windows;
using System.Windows.Input;
using Game2048.Business;
using Game2048.Wpf.ViewModels;

namespace Game2048.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            if (vm == null) return;

            switch (e.Key)
            {
                case Key.Left:
                    vm.Move(MoveDirection.Left);
                    e.Handled = true;
                    break;
                case Key.Right:
                    vm.Move(MoveDirection.Right);
                    e.Handled = true;
                    break;
                case Key.Up:
                    vm.Move(MoveDirection.Up);
                    e.Handled = true;
                    break;
                case Key.Down:
                    vm.Move(MoveDirection.Down);
                    e.Handled = true;
                    break;
            }
        }
    }
}
