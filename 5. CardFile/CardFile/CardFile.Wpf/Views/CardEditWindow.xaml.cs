using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    public partial class CardEditWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public CardEditWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void IsWorkingTillNowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsAvailableChecked();
        }

        private void IsWorkingTillNowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsAvailableUnchecked();
        }
    }
}
