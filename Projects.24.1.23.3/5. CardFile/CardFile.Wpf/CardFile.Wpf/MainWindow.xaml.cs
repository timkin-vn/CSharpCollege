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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var card = ViewModel.GetNewCard();
            window.ViewModel.LoadViewModel(card);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveNewCard(window.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var card = ViewModel.GetSelectedCard();
            if (card == null)
            {
                return;
            }

            window.ViewModel.LoadViewModel(card);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveEditedCard(window.ViewModel);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelected();
        }

        private void CardsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }
    }
}
