using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
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

namespace CardFile.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateCardButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetNewCard();
            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(card);
            if (window.ShowDialog() != true)
            {
                return;
            }

            ViewModel.SaveNewCard(window.ViewModel);
        }

        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetSelectedCard();
            if (card == null)
            {
                return;
            }

            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(card);
            if (window.ShowDialog() != true)
            {
                return;
            }

            ViewModel.SaveEditedCard(window.ViewModel);
        }

        private void DeleteCardButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }
    }
}
