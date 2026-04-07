using System;
using System.Windows;
using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            ViewModel.Initialized();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.WindowLoaded();
        }

        // Кнопка "Добавить"
        private void AddCard(object sender, RoutedEventArgs e)
        {
            var newCard = ViewModel.GetNewCard();
            var dialog = new CardEditWindow();
            dialog.ViewModel.LoadViewModel(newCard);
            if (dialog.ShowDialog() == true)
            {
                ViewModel.SaveNewCard(dialog.ViewModel);
            }
        }

        // Кнопка "Изменить"
        private void EditCard(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCard == null) return;
            var dialog = new CardEditWindow();
            dialog.ViewModel.LoadViewModel(ViewModel.SelectedCard);
            if (dialog.ShowDialog() == true)
            {
                ViewModel.SaveEditedCard(dialog.ViewModel);
            }
        }

        // Кнопка "Удалить"
        private void DeleteCard(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedCard == null) return;
            if (MessageBox.Show($"Удалить игрока {ViewModel.SelectedCard.DisplayName}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ViewModel.DeleteSelectedCard();
            }
        }

        // Обработчики меню
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Cards.Clear();
            ViewModel.ClearFileName();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                ViewModel.OpenFromFile(dialog.FileName);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.FileName))
            {
                SaveAsFile_Click(sender, e);
            }
            else
            {
                ViewModel.SaveToFile();
            }
        }

        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|JSON files (*.json)|*.json",
                DefaultExt = "xml"
            };
            if (dialog.ShowDialog() == true)
            {
                ViewModel.SaveToFile(dialog.FileName);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}