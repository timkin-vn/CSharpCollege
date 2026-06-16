using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    public partial class StudentEditWindow : Window
    {
        public StudentViewModel ViewModel => (StudentViewModel)DataContext;
        public StudentEditWindow() => InitializeComponent();
        private void OkButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;
        private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        private void IsStudyingNowCheckBox_Checked(object sender, RoutedEventArgs e) => ViewModel.IsStudyingNowChecked();
        private void IsStudyingNowCheckBox_Unchecked(object sender, RoutedEventArgs e) => ViewModel.IsStudyingNowUnchecked();
    }
}