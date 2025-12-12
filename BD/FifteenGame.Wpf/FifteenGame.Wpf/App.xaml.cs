// App.xaml.cs
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Сначала показываем окно логина
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog(); // ждём, пока закроется
        }
    }
}