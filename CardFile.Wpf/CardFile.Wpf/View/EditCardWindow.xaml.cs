using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.View
{
    public partial class EditCardWindow : Window
    {
        public CardViewModel ViewModel { get; set; }

        public EditCardWindow()
        {
            InitializeComponent();
            ViewModel = new CardViewModel();
            DataContext = ViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}