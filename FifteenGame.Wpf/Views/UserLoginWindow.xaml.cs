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
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel
        {
            get { return (UserLoginWindowViewModel)DataContext; }
        }

        public UserLoginWindow()
        {
            InitializeComponent();
            DataContext = new UserLoginWindowViewModel();
        }

        public UserLoginWindow(MainWindowViewModel mainViewModel)
            : this()
        {
            ViewModel.MainViewModel = mainViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.UserName))
            {
                MessageBox.Show("Введите имя пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ViewModel.FindUser())
            {
                if (MessageBox.Show("Такого пользователя в системе нет. Создать?", "Ошибка",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ViewModel.CreateUser();
                }
                else
                {
                    return;
                }
            }

            if (!ViewModel.SaveUser())
                return;

            DialogResult = true;
        }
    }
}