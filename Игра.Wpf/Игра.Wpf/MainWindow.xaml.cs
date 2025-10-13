using System.Windows;
using System.Windows.Controls;
using Игра.Wpf.ViewModels;

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
        }
    }
}