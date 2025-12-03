using System.Windows;

namespace FifteenGame.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => UserNameTextBox.Focus();
        }
    }
}