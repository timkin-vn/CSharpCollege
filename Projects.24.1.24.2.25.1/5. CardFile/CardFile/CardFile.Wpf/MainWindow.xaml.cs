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

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Файлы автосалона (*.json)|*.json|Все файлы (*.*)|*.*",
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
                Filter = "Файлы автосалона (*.json)|*.json",
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
            var car = ViewModel.GetNewCar();
            window.ViewModel.LoadViewModel(car);

            if (window.ShowDialog() != true)
            {
                return;
            }

            ViewModel.SaveNewCar(window.ViewModel);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var car = ViewModel.GetSelectedCar();
            if (car == null)
            {
                return;
            }

            var window = new CardEditWindow();
            window.ViewModel.LoadViewModel(car);

            if (window.ShowDialog() != true)
            {
                return;
            }

            ViewModel.SaveEditedCar(window.ViewModel);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить выбранный автомобиль?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ViewModel.DeleteSelectedCar();
            }
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
            ViewModel.SelectionChanged();
        }
    }
}