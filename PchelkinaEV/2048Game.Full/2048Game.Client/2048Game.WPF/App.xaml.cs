using _2048Game.WPF.ViewModels;
using _2048Game.WPF.Views;
using System.Windows;

namespace _2048Game.WPF
{
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        { 
            var loginWindow = new RegistrationOrEnterWindow();
            var mainWindow = new MainWindow();

            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                var vm = mainWindow.DataContext as MainWindowViewModel;
                vm.SetUser(loginWindow.User);
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }

        }
    }
}
