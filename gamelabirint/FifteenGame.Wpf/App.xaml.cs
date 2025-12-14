using System;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Запускаем окно входа
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске: {ex.Message}\n\nПроверьте App.config!");
                Shutdown();
            }
        }
    }
}