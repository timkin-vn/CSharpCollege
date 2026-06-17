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
using WpfHello.ViewModels;

namespace WpfHello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HelloButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Привет!", "Приветствие", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Add();
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Subtract();
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Multiply();
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Divide();
        }
    }
}
