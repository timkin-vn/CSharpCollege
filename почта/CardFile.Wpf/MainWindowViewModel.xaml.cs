using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using System.Windows;
using System.Windows.Controls;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.WindowLoaded();
            }
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.Initialized();
            }
        }

        /// <summary>
        /// Кнопка "Создать" новое письмо
        /// </summary>
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            // Получаем пустую модель письма из ViewModel
            var newLetter = viewModel.GetNewLetter();

            // Создаем окно редактирования и передаем ему новую модель
            var editWindow = new LetterEditWindow();
            editWindow.DataContext = newLetter;

            // Если пользователь нажал OK в окне редактирования
            if (editWindow.ShowDialog() == true)
            {
                // Сохраняем новое письмо через сервис
                viewModel.SaveNewLetter(newLetter);
            }
        }

        /// <summary>
        /// Кнопка "Правка" выбранного письма
        /// </summary>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            // Получаем выбранное письмо
            var selectedLetter = viewModel.GetSelectedLetter();

            if (selectedLetter == null) return;

            // Создаем копию для редактирования, чтобы изменения не применялись сразу при отмене
            var letterCopy = new LetterViewModel();
            letterCopy.LoadViewModel(selectedLetter);

            var editWindow = new LetterEditWindow();
            editWindow.DataContext = letterCopy;

            if (editWindow.ShowDialog() == true)
            {
                // Сохраняем изменения в оригинальный объект и в базу данных
                viewModel.SaveEditedLetter(letterCopy);
            }
        }

        /// <summary>
        /// Кнопка "Удалить" выбранное письмо
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            // Добавляем подтверждение удаления (опционально, но рекомендуется)
            var result = MessageBox.Show("Вы уверены, что хотите удалить это письмо?",
                                         "Подтверждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                viewModel.DeleteSelectedLetter();
            }
        }

        /// <summary>
        /// Обработка выбора строки в таблице для активации/деактивации кнопок
        /// </summary>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            viewModel.SelectionChanged();
        }

        /// <summary>
        /// Двойной клик по таблице открывает письмо для чтения
        /// </summary>
        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var selectedLetter = viewModel.GetSelectedLetter();

            if (selectedLetter == null) return;

            // Открываем окно просмотра
            var viewWindow = new Views.LetterViewWindow();
            viewWindow.DataContext = selectedLetter;

            // Устанавливаем владельца окна (чтобы открывалось поверх главного)
            viewWindow.Owner = this;

            // Показываем как диалог (блокирует главное окно)
            viewWindow.ShowDialog();
        }

        // --- Меню Файл ---

        private void FileOpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Файлы почты (*.mail)|*.mail|Текстовые файлы (*.txt)|*.txt|XML файлы (*.xml)|*.xml|JSON файлы (*.json)|*.json|ZIP архивы (*.zip)|*.zip|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var viewModel = (MainWindowViewModel)DataContext;
                    viewModel.OpenFromFile(dialog.FileName);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;

            if (string.IsNullOrEmpty(viewModel.FileName))
            {
                FileSaveAsMenuItem_Click(sender, e);
            }
            else
            {
                try
                {
                    viewModel.SaveToFile();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileSaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Файлы почты (*.mail)|*.mail|Текстовые файлы (*.txt)|*.txt|XML файлы (*.xml)|*.xml|JSON файлы (*.json)|*.json|ZIP архивы (*.zip)|*.zip|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var viewModel = (MainWindowViewModel)DataContext;
                    viewModel.SaveToFile(dialog.FileName);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}