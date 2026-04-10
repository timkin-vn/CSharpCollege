using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using Microsoft.Win32;
using System;
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

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                CheckFileExists = true
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            try
            {
                ViewModel.OpenFile(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка открытия", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.SaveFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                AddExtension = true,
                DefaultExt = ".json",
                FileName = "games.json"
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            try
            {
                ViewModel.SaveFileAs(dialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
