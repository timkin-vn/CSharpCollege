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
            var editWindow = new EditCardWindow();
            editWindow.ViewModel.LoadFromViewModel(newCard);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.InsertNewCard(editWindow.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetSelectedCard();
            if (card == null)
            {
                return;
            }

            var editWindow = new EditCardWindow();
            editWindow.ViewModel.LoadFromViewModel(card);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.UpdateCard(editWindow.ViewModel);
            }
        }

        private void Cards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt|Двоичные файлы|*.cardbin|Файлы XML|*.cardxml|Файлы JSON|*.cardjson|Файлы ZIP|*.cardzip|Все файлы|*.*";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFile(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSave_Click(object sender, RoutedEventArgs e)
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

        private void FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt|Двоичные файлы|*.cardbin|Файлы XML|*.cardxml|Файлы JSON|*.cardjson|Файлы ZIP|*.cardzip|Все файлы|*.*";
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
