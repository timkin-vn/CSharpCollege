using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            
            if (DataContext == null) DataContext = new MainWindowViewModel();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var company = ViewModel.GetNewCompany();
            window.ViewModel.LoadViewModel(company);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveNewCompany(window.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCompany == null) return;

            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(ViewModel.SelectedCompany);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveEditedCompany(window.ViewModel);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить?", "Вопрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ViewModel.DeleteSelectedCompany();
            }
        }

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "JSON|*.compjson" };
            if (dialog.ShowDialog() == true) ViewModel.OpenFromFile(dialog.FileName);
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FileName)) FileSaveAsMenuItem_Click(sender, e);
            else ViewModel.SaveToFile();
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "JSON|*.compjson" };
            if (dialog.ShowDialog() == true) ViewModel.SaveToFile(dialog.FileName);
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e) => Close();
        private void Window_Loaded(object sender, RoutedEventArgs e) => ViewModel?.WindowLoaded();
        private void Window_Initialized(object sender, EventArgs e) => ViewModel?.Initialized();
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) => ViewModel?.SelectionChanged();
    }
}