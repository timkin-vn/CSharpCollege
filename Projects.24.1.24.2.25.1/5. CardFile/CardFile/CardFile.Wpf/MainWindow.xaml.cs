using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using Microsoft.Win32;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).WindowLoaded();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ((MainWindowViewModel)DataContext).InitializeMapper(); // исправлено: было Initialized()
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Привязка SelectedItem уже обновляет SelectedCard, 
            // дополнительная логика не требуется.
            // Метод можно оставить пустым или удалить, 
            // но не вызывайте несуществующий SelectionChanged().
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var newCard = viewModel.GetNewCard();
            var editWindow = new CardEditWindow { DataContext = newCard };
            if (editWindow.ShowDialog() == true)
                viewModel.SaveNewCard(newCard);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var selected = viewModel.GetSelectedCard();
            if (selected == null) return;
            var copy = new CardViewModel();
            copy.LoadViewModel(selected);
            var editWindow = new CardEditWindow { DataContext = copy };
            if (editWindow.ShowDialog() == true)
                viewModel.SaveEditedCard(copy);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).DeleteSelectedCard();
        }

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте загрузку из файла
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте сохранение
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Реализуйте сохранение как
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}