using System;
using System.Windows;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using CardFile.Business.Infrastructure;   // добавить

namespace CardFile.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Перехват исключений
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show($"Unhandled exception: {args.ExceptionObject}", "Fatal Error");
            };
            DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"Dispatcher exception: {args.Exception.Message}", "Error");
                args.Handled = true;
            };

            // 1. Маппинги WPF (Card <-> CardViewModel)
            MapperInitialization.PreRegister();

            // 2. Маппинги бизнес-слоя и DataStore (CardDto <-> Card, JsonCard, XmlCard и Clone)
            BusinessMapperInitialization.PreRegister();

            // 3. Создание маппера
            Mapping.Initialize();

            base.OnStartup(e);
        }
    }
}