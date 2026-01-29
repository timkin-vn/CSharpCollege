using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CreateNinjectKernel();
            
            // Создаем главное окно и показываем окно входа
            var mainWindow = new MainWindow();
            var loginWindow = new Views.UserLoginWindow();
            loginWindow.ViewModel.MainViewModel = mainWindow.ViewModel;
            
            if (loginWindow.ShowDialog() == true)
            {
                mainWindow.Show();
            }
        }

        private void CreateNinjectKernel()
        {
            NinjectKernel.Instance = new StandardKernel(new FifteenGameModule());
        }
    }
}
