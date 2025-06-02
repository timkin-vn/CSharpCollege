using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using Microsoft.Win32;
using System.Windows;
using System;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var newCard = ViewModel.GetNewCard();
            var editWindow = new EditCardWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            editWindow.ViewModel.LoadFromViewModel(newCard);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.InsertNewCard(editWindow.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCard == null) return;

            var editWindow = new EditCardWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            editWindow.ViewModel.LoadFromViewModel(ViewModel.SelectedCard);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.UpdateCard(editWindow.ViewModel);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCard == null) return;

            var result = MessageBox.Show(
                $"Удалить технику '{ViewModel.SelectedCard.Name}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ViewModel.DeleteSelectedCard();
            }
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Файлы картотеки|*.cardbin;*.cardxml;*.cardjson|Все файлы|*.*",
                Title = "Открыть картотеку техники"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при открытии файла:\n{ex.Message}",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void FileSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ViewModel.FileName))
                {
                    FileSaveAs_Click(sender, e);
                }
                else
                {
                    ViewModel.SaveToFile(ViewModel.FileName);
                    MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при сохранении:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Двоичный формат (*.cardbin)|*.cardbin|XML формат (*.cardxml)|*.cardxml|JSON формат (*.cardjson)|*.cardjson",
                DefaultExt = ".cardbin",
                Title = "Сохранить картотеку техники"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.SaveToFile(dialog.FileName);
                    MessageBox.Show("Данные успешно сохранены", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при сохранении:\n{ex.Message}",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void FileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ViewModel.Initialized();
        }
    }
}