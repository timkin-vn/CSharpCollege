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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (ViewModels.MainWindowViewModel)DataContext;
            switch (e.Key)
            {
                case Key.Up:
                case Key.W:
                    vm.MoveUpCommand.Execute(null);
                    e.Handled = true;
                    break;
                case Key.Down:
                case Key.S:
                    vm.MoveDownCommand.Execute(null);
                    e.Handled = true;
                    break;
                case Key.Left:
                case Key.A:
                    vm.MoveLeftCommand.Execute(null);
                    e.Handled = true;
                    break;
                case Key.Right:
                case Key.D:
                    vm.MoveRightCommand.Execute(null);
                    e.Handled = true;
                    break;
            }
        }
    }
}