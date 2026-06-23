using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Checkers.Common.Infrastructure;
using Checkers.ViewModels;
using Checkers.Views;
using Ninject;
using Ninject.Parameters;

namespace Checkers
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IKernel Kernel { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            string connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
            Kernel = new StandardKernel(new CheckersModule(connectionString));
            var loginWindow = new UserLoginWindow();
            
            loginWindow.LoginSuccessful += async (s, session) =>
            {
                var userSession = loginWindow.UserSession;
                var factory = Kernel.Get<IViewModelFactory>();
                var mainWindow = Kernel.Get<MainWindow>();
                var gameVm = factory.Create(userSession);
                mainWindow.DataContext = gameVm;
                mainWindow.Show();
                var authService = Kernel.Get<AuthService>();
                string result = await authService.RegisterNewUser(loginWindow.UserSession?.Name ?? "");
                MessageBox.Show(result);
            };
            loginWindow.Show();
        }
    }
}
