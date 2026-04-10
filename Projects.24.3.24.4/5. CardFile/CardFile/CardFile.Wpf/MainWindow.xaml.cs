using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using System.Windows;
using System.Windows.Controls;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {

        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateCardButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetNewCard();
            var window = new CardEditWindow();


            var editViewModel = new CardViewModel();
            editViewModel.LoadViewModel(card);

   
            window.DataContext = editViewModel;

            if (window.ShowDialog() == true)
            {
  
                ViewModel.SaveNewCard((CardViewModel)window.DataContext);
            }
        }

        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetSelectedCard();
            if (card == null) return;

            var window = new CardEditWindow();

            var editViewModel = new CardViewModel();
            editViewModel.LoadViewModel(card);

            window.DataContext = editViewModel;

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveEditedCard((CardViewModel)window.DataContext);
            }
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

        
        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e) => Close();
        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e) { ViewModel.Open(); }
        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e) {
            ViewModel.SaveAs();
        }
        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e) { ViewModel.SaveAs(); }
    }
}