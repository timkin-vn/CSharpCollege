
using Calculator.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel =>
            (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string token)
                ViewModel.InputToken(token);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressClear();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PressEqual();
        }

        private void MemoryAddButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MemoryAdd();
        }

        private void MemoryRecallButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MemoryRecall();
        }

        private void MemoryClearButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.MemoryClear();
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearHistory();
        }
    }
}
