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
using CardFile.WPF.ViewModels;
using CardFile.WPF.Views;
using Microsoft.Win32;

namespace CardFile.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FILE_FILTER = "Текстовые файлы|*.txt|Файлы CSV|*.csv|Двоичный файл картотеки|*.cardbin" +
            "|XML-файлы картотеки|*.cardxml|JSON-файл картотеки|*.cardjson|ZIP-архив картотеки|*.cardzip";

        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(ViewModel.Changed)
            {
                var warningDialog = GetWarningDialog();
                switch(warningDialog)
                {
                    case MessageBoxResult.Yes:
                        SaveFile();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            var dialog = new OpenFileDialog()
            {
                Filter = FILE_FILTER,
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.OpenFormFile(dialog.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
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

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DoSaveAs();
        }
        private void DoSaveAs()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = FILE_FILTER
            };
            if(dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.SaveToFile(dialog.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CardEditWindow();
            var card = ViewModel.GetNewCard();
            window.ViewModel.LoadViewModel(card);
            if (window.ShowDialog() != true)
            {
                return;
            }
            ViewModel.SaveNewCard(window.ViewModel);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            
            var card = ViewModel.GetSelectedCard();
            if(card == null)
            {
                return;
            }
            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(card);
            if(window.ShowDialog() != true)
            {
                return;
            }
            ViewModel.SaveEditedCard(window.ViewModel);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ViewModel.Initialized();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionCard();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(ViewModel.Changed)
            {
                var dialog = GetWarningDialog();
                switch (dialog)
                {
                    case MessageBoxResult.Yes:
                        SaveFile();
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private static MessageBoxResult GetWarningDialog()
        {
            return MessageBox.Show("Сохранить изменения", "Предупреждение", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SearchCard(ViewModel.SearchText);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.Show();
        }
    }
}
