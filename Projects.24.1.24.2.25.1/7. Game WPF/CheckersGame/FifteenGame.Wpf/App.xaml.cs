using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CheckersGame.Business.Models;
using CheckersGame.Business.Services;
using CheckersGame.Wpf.ViewModels;

namespace CheckersGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var model = new GameModel();
            var service = new GameService();
            service.Initialize(model);

            var whiteWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(model, isFlipped: false),
                Title = "Шашки — Белые"
            };
            var blackWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(model, isFlipped: true),
                Title = "Шашки — Чёрные"
            };
            whiteWindow.Show();
            blackWindow.Show();
        }
    }
}
