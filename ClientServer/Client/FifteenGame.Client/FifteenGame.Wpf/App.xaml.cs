using System.Windows;
using System.Net; // <--- Добавь этот using

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        public App()
        {
            // === МАГИЧЕСКАЯ СТРОКА ===
            // Она разрешает подключаться к localhost без проверки сертификата
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
        }
    }
}