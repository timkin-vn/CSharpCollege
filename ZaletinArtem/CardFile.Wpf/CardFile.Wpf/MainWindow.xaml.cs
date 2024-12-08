using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using System;
using System.Windows;

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
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            var cardViewModel = ViewModel.GetNewCardViewModel();

            if (cardViewModel == null)
            {
                throw new NullReferenceException("GetNewCardViewModel вернул null.");
            }

            editWindow.DataContext = cardViewModel;

            if (editWindow.ViewModel == null)
            {
                throw new NullReferenceException("ViewModel в EditCardWindow равно null.");
            }

            // Копируем данные
            editWindow.ViewModel.CopyFrom(cardViewModel);

            // Отображаем окно
            if (editWindow.ShowDialog() ?? false)
            {
                ViewModel.Save(editWindow.ViewModel);
            }
        }


        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            var cardViewModel = ViewModel.GetNewCardViewModel();
            editWindow.DataContext = cardViewModel;
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
    }
}
