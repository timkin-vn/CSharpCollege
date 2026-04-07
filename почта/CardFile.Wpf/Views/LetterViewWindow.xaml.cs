using System.Windows;

namespace CardFile.Wpf.Views
{
    public partial class LetterViewWindow : Window
    {
        public LetterViewWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}