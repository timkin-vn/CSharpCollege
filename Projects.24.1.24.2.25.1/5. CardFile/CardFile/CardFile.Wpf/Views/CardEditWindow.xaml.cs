
using System.Windows;
using System.Windows.Input;

namespace CardFile.Wpf.Views
{
    public partial class CardEditWindow : Window
    {
        public ViewModels.CardViewModel ViewModel =>
            (ViewModels.CardViewModel)DataContext;

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
    }
}
