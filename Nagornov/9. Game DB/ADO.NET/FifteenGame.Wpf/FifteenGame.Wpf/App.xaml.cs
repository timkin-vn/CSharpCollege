using System;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                Console.WriteLine("=== ЗАПУСК ПРИЛОЖЕНИЯ ===");

                // Прямо показываем MainWindow, где уже есть панель входа
                var mainWindow = new MainWindow();
                mainWindow.Show();

                Console.WriteLine("MainWindow показан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
                MessageBox.Show($"Ошибка запуска: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}