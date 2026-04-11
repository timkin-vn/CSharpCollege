using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    
    public partial class CardEditWindow : Window
    {
        
        public CardEditViewModel ViewModel => (CardEditViewModel)DataContext;

        public CardEditWindow()
        {
            InitializeComponent();

           
            if (DataContext == null)
            {
                DataContext = new CardEditViewModel();
            }
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