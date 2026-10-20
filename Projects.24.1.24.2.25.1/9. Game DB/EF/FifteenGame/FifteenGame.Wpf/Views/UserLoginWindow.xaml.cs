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
            if (string.IsNullOrWhiteSpace(ViewModel.UserName))
            {
                MessageBox.Show(this, "Введите имя игрока.", "Ключи и монстры", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!ViewModel.FindUser())
            {
                if (MessageBox.Show(this, "Игрока с таким именем ещё нет. Создать нового?", "Новый игрок",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
