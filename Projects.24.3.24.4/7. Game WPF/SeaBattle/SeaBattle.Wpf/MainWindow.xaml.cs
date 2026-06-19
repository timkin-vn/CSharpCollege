using System.Windows;
using System.Windows.Input;
using SeaBattle.Wpf.ViewModels;

namespace SeaBattle.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Клик по клетке. Реагируем только на поле противника (HideShips = true).
        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;

            var cell = element.DataContext as CellViewModel;
            if (cell == null || !cell.HideShips) return;

            var vm = DataContext as MainWindowViewModel;
            if (vm != null)
                vm.FireAtEnemy(cell.Row, cell.Col);
        }
    }
}
