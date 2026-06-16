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
        public MainWindow() => InitializeComponent();

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "Файлы картотеки|*.txt;*.csv;*.cardbin;*.cardxml;*.cardjson;*.cardzip" };
            if (dialog.ShowDialog() != true) return;
            try { ViewModel.OpenFromFile(dialog.FileName); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FileName)) DoSaveAs();
            else try { ViewModel.SaveToFile(); } catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e) => DoSaveAs();

        private void DoSaveAs()
        {
            var dialog = new SaveFileDialog { Filter = "Файлы картотеки|*.txt;*.csv;*.cardbin;*.cardxml;*.cardjson;*.cardzip" };
            if (dialog.ShowDialog() != true) return;
            try { ViewModel.SaveToFile(dialog.FileName); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e) => Close();

        private void CreateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var student = ViewModel.GetNewStudent();
            var window = new StudentEditWindow();
            window.ViewModel.LoadViewModel(student);
            if (window.ShowDialog() == true) ViewModel.SaveNewStudent(window.ViewModel);
        }

        private void EditStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var student = ViewModel.GetSelectedStudent();
            if (student == null) return;
            var window = new StudentEditWindow();
            window.ViewModel.LoadViewModel(student);
            if (window.ShowDialog() == true) ViewModel.SaveEditedStudent(window.ViewModel);
        }

        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e) => ViewModel.DeleteSelectedStudent();
        private void Window_Loaded(object sender, RoutedEventArgs e) => ViewModel.WindowLoaded();
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) => ViewModel.SelectionChanged();
    }
}