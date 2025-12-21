using _2048Game.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace _2048Game.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.HandKey(e.Key);
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.RestartLevel();
            }

            // ensure window has keyboard focus after clicking restart
            this.Focus();
            Keyboard.Focus(this);
        }
    }
}
