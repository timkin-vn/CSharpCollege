using Pacman.Client.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Pacman.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.Hide();

            var loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                
                var loginVm = (LoginViewModel)loginWindow.DataContext;
                var user = loginVm.LoggedUser;

                
                var mainVm = (MainViewModel)this.DataContext;
                mainVm.StartGame(user);

                
                this.Show();
                this.Activate(); 
            }
            else
            {
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.HandleInput(e.Key);
        }
    }
}