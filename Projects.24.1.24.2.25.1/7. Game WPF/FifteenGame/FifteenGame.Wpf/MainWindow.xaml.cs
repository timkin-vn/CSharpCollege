using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)DataContext; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void Options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            ViewModel.NewGame(DifficultyComboBox.SelectedIndex, FirstMoveComboBox.SelectedIndex != 1);
        }

        private void Cell_LeftClick(object sender, RoutedEventArgs e)
        {
            var cell = ((Button)sender).DataContext as CellViewModel;
            if (cell != null)
            {
                ViewModel.OpenCell(cell.Row, cell.Column);
            }
        }

        private void Cell_RightClick(object sender, MouseButtonEventArgs e)
        {
            var cell = ((Button)sender).DataContext as CellViewModel;
            if (cell != null)
            {
                ViewModel.ToggleFlag(cell.Row, cell.Column);
            }
            e.Handled = true;
        }
    }
}
