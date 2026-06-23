using System;
using System.Windows;
using SeaBattle.Common;
using SeaBattle.DataAccess;

namespace SeaBattle.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                DatabaseInitializer.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться к PostgreSQL или создать базу. Проверь, что сервер запущен.\n\n" + ex.Message,
                    "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

            NinjectKernel.Initialize(new MainModule());

            var login = new LoginWindow();
            login.Show();
        }
    }
}
