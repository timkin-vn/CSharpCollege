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
using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Infrastructure;
using FifteenGame.Wpf.Views;
using Ninject;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        public IKernel Kernel { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Kernel = new StandardKernel(new FifteenGameModule());
            NinjectKernel.Instance = Kernel;

            var loginWindow = new UserLoginWindow();
            loginWindow.Show();
        }
    }
}