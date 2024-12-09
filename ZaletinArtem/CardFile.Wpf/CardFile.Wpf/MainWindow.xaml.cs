using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
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
            InitializeComponent();

            var viewModel = new CardFileViewModel();
            DataContext = viewModel;
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
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = ".json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                ViewModel.SaveToFile(saveFileDialog.FileName);
                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                DefaultExt = ".json"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ViewModel.LoadFromFile(openFileDialog.FileName);
                MessageBox.Show("Данные успешно загружены!", "Загрузка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
