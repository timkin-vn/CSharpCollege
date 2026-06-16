using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views;
public partial class EditCardWindow : Window {
    public CardViewModel ViewModel => (CardViewModel)DataContext;

    public EditCardWindow() {
        InitializeComponent();
    }

    private void WokrsTillNow_Checked(object sender, RoutedEventArgs e) {
        ViewModel.WorksTillNowChecked();
    }

    private void WorksTillNow_Unchecked(object sender, RoutedEventArgs e) {
        ViewModel.WorksTillNowUnchecked();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) {
        DialogResult = false;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e) {
        DialogResult = true;
    }
}