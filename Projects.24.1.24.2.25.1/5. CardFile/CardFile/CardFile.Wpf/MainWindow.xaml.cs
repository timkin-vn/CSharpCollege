using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using Microsoft.Win32;
using System;
using System.Windows;

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
                Filter = "Текстовые файлы|*.txt|Файлы CSV|*.csv|Двоичные файлы|*.cardbin" +
                    "|XML-файлы|*.cardxml|JSON-файлы|*.cardjson|ZIP-архив|*.cardzip",
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFromFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FileName))
            {
                DoSaveAs();
            }
            else
            {
                try
                {
                    ViewModel.SaveToFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы|*.txt|Файлы CSV|*.csv|Двоичные файлы|*.cardbin" +
                    "|XML-файлы|*.cardxml|JSON-файлы|*.cardjson|ZIP-архив|*.cardzip",
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.SaveToFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var book = ViewModel.NewBook();
            window.ViewModel.LoadViewModel(book);

            if (window.ShowDialog() != true) return;

            ViewModel.SaveBook(window.ViewModel);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var book = ViewModel.SelectedBook;
            if (book == null) return;

            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(book);

            if (window.ShowDialog() != true) return;

            ViewModel.SaveBook(window.ViewModel);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранную книгу?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ViewModel.DeleteBook();
            }
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RestoreBook();
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IssueBook();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReturnBook();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Библиотечный каталог v1.0\n\nУправление библиотечным фондом",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}