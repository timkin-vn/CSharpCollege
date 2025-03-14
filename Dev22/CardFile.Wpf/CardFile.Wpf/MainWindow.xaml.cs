using CardFile.Wpf.Infrastructure;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CardFileViewModel ViewModel => (CardFileViewModel)DataContext;

        public MainWindow()
        {
            MapperRegistrator.Register();
            InitializeComponent();
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            var cardViewModel = ViewModel.GetNewCardViewModel();
            editWindow.ViewModel.CopyFrom(cardViewModel);
            if (editWindow.ShowDialog() ?? false)
            {
                ViewModel.Save(editWindow.ViewModel);
            }
        }

        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            var cardViewModel = ViewModel.GetSelectedCardViewModel();
            editWindow.ViewModel.CopyFrom(cardViewModel);
            if (editWindow.ShowDialog() ?? false)
            {
                ViewModel.Save(editWindow.ViewModel);
            }
        }

        private void DeleteCardButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл|*.txt|Двоичный файл|*.cardbin|Файл XML|*.xml|Файлы JSON|*.json|ZIP-архивы|*.zip|Все файлы|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.FileName))
            {
                ViewModel.SaveToFile();
            }
            else
            {
                DoSaveAs();
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoSaveAs();
        }

        private void DoSaveAs()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый файл|*.txt|Двоичный файл|*.cardbin|Файл XML|*.xml|Файлы JSON|*.json|ZIP-архивы|*.zip|Все файлы|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                ViewModel.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ViewModel.Initialized();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
