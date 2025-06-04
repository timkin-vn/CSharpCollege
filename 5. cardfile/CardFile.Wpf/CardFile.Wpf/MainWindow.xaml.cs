using CardFile.Wpf.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CardFile.Wpf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчики событий
        private void Window_Initialized(object sender, System.EventArgs e)
        {
            ViewModel.Initialized();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded();
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ViewModel.OpenFile(dialog.FileName);
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveToFile();
        }

        private void Cards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.UpdateVisibility();
            }
        }

        private void MenuFileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "JSON (*.cardjson)|*.cardjson|" +
                        "XML (*.cardxml)|*.cardxml|" +
                        "Binary (*.cardbin)|*.cardbin|" +
                        "Text (*.txt)|*.txt",
                DefaultExt = ".cardjson"
            };

            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.SaveToFile(saveDialog.FileName);
                    MessageBox.Show("Файл успешно сохранен", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            if (editWindow.ShowDialog() == true)
            {
                ViewModel.InsertNewCard(editWindow.ViewModel);
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCard != null)
            {
                var editWindow = new EditCardWindow();
                editWindow.DataContext = ViewModel.SelectedCard;
                if (editWindow.ShowDialog() == true)
                {
                    ViewModel.UpdateCard(editWindow.ViewModel);
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.UpdateVisibility();
        }


    }
}