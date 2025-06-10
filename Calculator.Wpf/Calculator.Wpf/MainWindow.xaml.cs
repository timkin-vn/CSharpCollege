using System.Windows;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm && sender is System.Windows.Controls.Button button)
            {
                vm.PressDigit(button.Content.ToString());
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.PressClear();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm && sender is System.Windows.Controls.Button button)
            {
                string operation = button.Content.ToString();
                if (operation == "=")
                {
                    vm.PressEqual();
                }
                else
                {
                    vm.PressOperation(operation);
                }
            }
        }

        private void DecimalPoint_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.DecimalPoint_Click(sender, e);
            }
        }

        private void PressPi_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.PressPi_Click(sender, e);
            }
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.SquareRoot_Click(sender, e);
            }
        }

        private void IncreaseDecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.IncreaseDecimalPlaces();
            }
        }

        private void DecreaseDecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.DecreaseDecimalPlaces();
            }
        }

        private void RoundButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.MainWindowViewModel vm)
            {
                vm.RoundToSelectedPrecision();
            }
        }
    }
}