using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
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

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel; ;
        }

        // Этот обработчик должен быть привязан к кнопке/ячейке (например, Button в XAML)
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cell)
            {
                ViewModel.SelectCell(cell);
            }
        }
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                var grid = (Grid)sender;
                var cell = grid.DataContext as CellViewModel;
                viewModel.SelectCell(cell);
            }
        }
    }
}
