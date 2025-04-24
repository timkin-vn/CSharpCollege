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
using Game2048.WPF.ViewModels;

namespace Game2048.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для UserLoginWindow.xaml
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
                if (MessageBox
                    .Show("Указанный пользователь не найден. Создать?", "Ошибка поиска", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }

                ViewModel.CreateUser();
            }

            ViewModel.SaveUser();
            DialogResult = true;
        }
    }
}
