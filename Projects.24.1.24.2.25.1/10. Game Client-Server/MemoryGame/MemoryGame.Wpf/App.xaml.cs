using MemoryGame.Common.Infrastructure;
using MemoryGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryGame.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherUnhandledException += OnDispatcherUnhandledException;
            CreateNinjectKernel();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var error = e.Exception is AggregateException aggregate ? aggregate.GetBaseException() : e.Exception;
            MessageBox.Show($"Ошибка при обращении к серверу ({ConfigurationManager.AppSettings["ServerConnection"]}):\n{error.Message}\n\nПроверьте, что сервер MemoryGame.WebApi запущен.",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CreateNinjectKernel()
        {
            NinjectKernel.Instance = new StandardKernel(new MemoryGameModule());
        }
    }
}
