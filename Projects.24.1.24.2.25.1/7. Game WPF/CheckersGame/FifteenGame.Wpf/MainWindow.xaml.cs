//using FifteenGame.Business.Models;
//using FifteenGame.Wpf.ViewModels;
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
using CheckersGame.Wpf.ViewModels;

namespace CheckersGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var clickedCell = sender as FrameworkElement;
            if (clickedCell?.Tag is CellViewModel cellViewModel)
            {
                var gameViewModel = DataContext as MainWindowViewModel;
                gameViewModel?.CellClicked(cellViewModel);
            }
        }
    }
}