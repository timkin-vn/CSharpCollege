using _2048Game.WPF.ViewModels;
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

namespace _2048Game.WPF.Views
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

        private void OkButtom_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.FindUser())
            {
                if (MessageBox.Show("Пользователь не найден. Создать?", "Ошибка",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ViewModel.CreateUser();
                }
                else
                {
                    return;
                }
            }

            ViewModel.SaveUser();
            DialogResult = true;
        }
    }
}
