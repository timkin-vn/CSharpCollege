using CardFile.Wpf.View;
using CardFile.Wpf.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        private CardFileViewModel _viewModel;

        public CardFileViewModel ViewModel
        {
            get => _viewModel;
            private set => _viewModel = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new CardFileViewModel();
            DataContext = ViewModel;
            ViewModel.Initialized();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditCardWindow();
            var cardViewModel = ViewModel.GetNewCard();
            window.ViewModel.CopyFrom(cardViewModel);
            if (window.ShowDialog() ?? false)
            {
                if (!ViewModel.Save(window.ViewModel))
                {
                    MessageBox.Show("Не удалось сохранить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var cardViewModel = ViewModel.GetSelectedCard();
            if (cardViewModel == null)
            {
                MessageBox.Show("Выберите запись для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var window = new EditCardWindow();
            window.ViewModel.CopyFrom(cardViewModel);
            if (window.ShowDialog() ?? false)
            {
                if (!ViewModel.Save(window.ViewModel))
                {
                    MessageBox.Show("Не удалось сохранить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.DeleteSelected())
            {
                MessageBox.Show("Не удалось удалить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы|*.txt|Все файлы|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FileName))
            {
                DoSaveAs();
            }
            else
            {
                ViewModel.SaveToFile();
            }
        }

        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы|*.txt|Все файлы|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                ViewModel.SaveToFileAs(saveFileDialog.FileName);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedItem != null)
            {
                ViewModel.SelectedCard = dataGrid.SelectedItem as CardViewModel;
            }
        }
    }
}