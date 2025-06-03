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
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();

            //window.ViewModel.LoadViewModel(card);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveNewCard(window.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var card = ViewModel.GetSelectedCard();
            if (card == null)
            {
                return;
            }

            window.ViewModel.LoadViewModel(card);

            if (window.ShowDialog() == true)
            {
                ViewModel.SaveEditedCard(window.ViewModel);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelected();
        }

        private void CardsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt|Двоичные файлы|*.cardbin|Файлы XML|*.cardxml|Файлы JSON|*.cardjson|Файлы ZIP|*.cardzip|Все файлы|*.*";

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
