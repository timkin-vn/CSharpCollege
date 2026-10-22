using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as FrameworkElement;
            if (grid?.Tag is CellViewModel cellViewModel)
            {
                var mainVM = DataContext as MainWindowViewModel;
                mainVM?.CellClicked(cellViewModel);
            }
        }
    }
}
