using FifteenGame.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FifteenGame.Wpf.Views
{
    /// <summary>
    /// Interaction logic for UserLoginWindow
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel { get; private set; }

        public UserLoginWindow()
        {
            InitializeComponent();
            ViewModel = new UserLoginWindowViewModel();
            DataContext = ViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Сразу создаем или получаем пользователя
            ViewModel.CreateUser();
            ViewModel.SaveUser();
            DialogResult = true;
        }
    }
}
