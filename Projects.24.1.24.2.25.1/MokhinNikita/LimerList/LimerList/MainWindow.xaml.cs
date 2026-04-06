using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LimerList.ViewModels;
using LimerList.Views;
using Microsoft.Win32;

namespace LimerList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FILE_FILTER = "Xml-Limer File|*.liml|Текстовый файл|.*txt|CSV-файл|*.csv|ZIP-файл|*.lzip";
        public MainViewModel ViewModel => (MainViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ViewModel.Initialized();
        }
        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            var limer = ViewModel.GetSelectedLimer();
            if (limer == null)
            {
                return;
            }
            var window = new LimerEditWindow();
            window.ViewModel.LoadViewModel(limer);
            if(window.ShowDialog() != true)
            {
                return;
            }
            ViewModel.SaveEditedLimer(window.ViewModel);
        }
        private void NewButtonClicked(object sender, RoutedEventArgs e)
        {
            var window = new LimerEditWindow();
            var limer = ViewModel.GetNewLimer();
            window.ViewModel.LoadViewModel(limer);
            if (window.ShowDialog() != true)
            {
                return;
            }
            ViewModel.SaveNewLimer(window.ViewModel);
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedLimer();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Selecting();
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
        private void DoSaveAs()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = FILE_FILTER,
            };
            if(dialog.ShowDialog() == true)
            {
                try
                {
                    ViewModel.SaveToFile(dialog.FileName);
                }catch (Exception ex)
                {
                   MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFile();
        }
        private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DoSaveAs();
        }
        private void FunctionCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if(ViewModel.Changed)
            {
                var result = Confirm();
                switch (result)
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
            if(dialog.ShowDialog() == true)
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
        private MessageBoxResult Confirm()
        {
            return MessageBox.Show("Save Changes?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(ViewModel.Changed)
            {
                var result = Confirm();
                switch (result)
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

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }
    }
}
