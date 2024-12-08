using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
using System;
using System.Windows;

namespace CardFile.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                Console.WriteLine($"DataContext установлен: {DataContext != null}");
            };
            DataContext = new CardFileViewModel();
        }

        private void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCardWindow();
            var newCardViewModel = new CardViewModel();
            editWindow.DataContext = newCardViewModel;

            if (editWindow.ShowDialog() == true)
            {
                var mainViewModel = DataContext as CardFileViewModel;
                mainViewModel?.AddCard(newCardViewModel);
            }
        }

        private void EditCardButton_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as CardFileViewModel;
            if (mainViewModel?.SelectedCard == null)
                return;

            var editWindow = new EditCardWindow();
            var selectedCardViewModel = mainViewModel.SelectedCard;
            editWindow.DataContext = selectedCardViewModel;

            if (editWindow.ShowDialog() == true)
            {
                mainViewModel.UpdateCard(selectedCardViewModel);
            }
        }

        private void DeleteCardButton_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as CardFileViewModel;
            if (mainViewModel?.SelectedCard == null)
                return;

            mainViewModel.RemoveCard(mainViewModel.SelectedCard);
        }
    }
}
