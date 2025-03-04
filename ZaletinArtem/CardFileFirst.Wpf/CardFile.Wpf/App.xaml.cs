using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CardFile.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow?.ViewModel != null)
            {
                mainWindow.ViewModel.SaveToFile("cards.json");
            }
        }
    }
}
