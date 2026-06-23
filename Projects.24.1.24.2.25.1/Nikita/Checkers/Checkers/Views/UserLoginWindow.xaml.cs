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
using Checkers.Business.Models;
using Checkers.Models;

namespace Checkers.Views
{
    /// <summary>
    /// Логика взаимодействия для UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        public event EventHandler<UserSession> LoginSuccessful;
        private UserSession _userSession;
        public UserSession UserSession
        {
            get => _userSession;
            set => _userSession = value;
        }
        public UserLoginWindow()
        {
            InitializeComponent();
            UserSession = null;
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            string userName = userNameTextBox?.Text ?? "";
            if(!string.IsNullOrWhiteSpace(userName))
            {
                UserSession userSession = new UserSession
                {
                    Id = 1,
                    Name = userName,
                };
                LoginSuccessful?.Invoke(this, userSession);
                UserSession = userSession;
                Close();
            }else
            {
                MessageBox.Show("Пожалуйста, введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
