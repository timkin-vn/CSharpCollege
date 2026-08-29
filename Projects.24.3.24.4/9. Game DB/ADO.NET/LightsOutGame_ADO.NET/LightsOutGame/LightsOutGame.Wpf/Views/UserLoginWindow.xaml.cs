using LightsOutGame.Wpf.ViewModels;
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

namespace LightsOutGame.Wpf.Views
{
    /// <summary>
    /// Interaction logic for UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel => (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.FindUser())
            {
                var dialogResult = MessageBox.Show("Такого пользователя нет в системе. Создать?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    ViewModel.CreateUser();
                }
                else
                {
                    return;
                }
            }

            ViewModel.CommitUser();
            DialogResult = true;
        }
    }
}
