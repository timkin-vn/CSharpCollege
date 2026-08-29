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
using FifteenGames.Wpf.ViewModels;

namespace FifteenGames.Wpf.Views
{
    /// <summary>
    /// Логика взаимодействия для UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        public UserLoginViewModel ViewModel => (UserLoginViewModel)DataContext;
        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.FindUser())
            {
                if (MessageBox.Show("Пользователь не найден. Создать нового?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
