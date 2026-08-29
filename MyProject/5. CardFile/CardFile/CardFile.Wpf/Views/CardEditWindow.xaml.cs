using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    public partial class CardEditWindow : Window
    {
        public CardEditWindow()
        {
            InitializeComponent();
        }

        public CardViewModel GetViewModel()
        {
            return (CardViewModel)DataContext;
        }

        public void LoadViewModel(CardViewModel card)
        {
            GetViewModel().LoadViewModel(card);
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