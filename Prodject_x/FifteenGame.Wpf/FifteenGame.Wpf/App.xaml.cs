using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Reflection;

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
            
            // Запускаем игру Крестики-нолики
            TicTacToeWindow ticTacToeWindow = new TicTacToeWindow();
            ticTacToeWindow.Show();
        }
    }
}
