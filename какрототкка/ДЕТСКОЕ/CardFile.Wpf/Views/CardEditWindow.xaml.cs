using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Interaction logic for EditCardWindow.xaml
    /// </summary>
    public partial class EditCardWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public EditCardWindow()
        {
            InitializeComponent();
        }

        private void IsOrderActive_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsOrderActiveChecked();
        }

        private void IsOrderActive_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsOrderActiveUnchecked();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}